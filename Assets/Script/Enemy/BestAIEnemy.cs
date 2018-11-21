using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BestAIEnemy : MonoBehaviour
{
    //- Make Universal color system

    // Colors
    public enum Colors
    {
        Yellow,         // Yellow = 0
        Cyan,           // Cyan = 1
        Magenta         // Magenta = 2
    };
    
    public Colors cur_color = Colors.Yellow;                    // Current selected color

    string damaging_tag1;
    string damaging_tag2;
    int damaging_layer1;
    int damaging_layer2;


    /////////////////////////////////////////////////////

    // Navmesh variables

    private NavMeshAgent nav_agent;             // Navmesh object
    private GameObject target;                  // Move towards objective
    public Transform[] points;                  // Patrol points (unfinished)
    public GameObject home;                     // Enemy return point

    // Moving variables

    public float chase_speed = 7;

    // Target Detection
    public float home_distance;                 // Distance the enemy can be from home
    public float alert_distance;                // Distance at which the enemy detects the player by getting near
    public float sight_distance;                // Distance at which the enemy detects the player by sight
    public float sight_angle;                   // Enemy field of view
    float listen_distance;                      // Distance at which the enemy detects the player by noise
    bool chase_by_near;
    bool chase_on_sight;

    // Chasing
    public float safe_distance;                 // Distance the enemy can be from the player


    // Shooting
    public ShotEnemy shot;                      // Enemy's gun
    public ParticleSystem DieEffect;            // Die particles
    //public bool Stop;                         // (Unused)
    bool friendly_fire;                         // Friendly fire



    // AI variables

    bool is_chasing;                        // Is enemy moving towards objective
    bool look_target = false;               // Rotate enemy when going after player
    bool back_home = false;                 // Is enemy heading back home
    PatrolController patrol;

    // private int despoint;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.Find("Player");
        nav_agent = GetComponent<NavMeshAgent>();
        patrol = GetComponent<PatrolController>();

        //DieEffect.Stop();
        EnemyColorData();
    }
	
    void EnemyColorData()
    {
        if(cur_color == Colors.Yellow)
        {
            damaging_tag1 = "Blue";
            damaging_tag2 = "Pink";
            damaging_layer1 = 12;
            damaging_layer2 = 13;
        }
        else if (cur_color == Colors.Magenta)
        {
            damaging_tag1 = "Blue";
            damaging_tag2 = "Yellow";
            damaging_layer1 = 12;
            damaging_layer2 = 11;
        }
        else if (cur_color == Colors.Cyan)
        {
            damaging_tag1 = "Pink";
            damaging_tag2 = "Yellow";
            damaging_layer1 = 13;
            damaging_layer2 = 11;
        }
    }

	// Update is called once per frame
	void Update ()
    {
        DetectPlayer();
        KeepDistance();
        ShootTarget();
        IsInHome();

        // Go after player
        if (is_chasing == true )
        {
            nav_agent.SetDestination(target.transform.position);
            //nav_agent.speed = chase_speed;
            nav_agent.isStopped = false;
            patrol.is_patrol = false;
            
        }
        // Go back home
        if (back_home == true)
        {
            nav_agent.SetDestination(home.transform.position);
            shot.isShooting = false;
        }

        // Rotate towards player instantly
        if(look_target == true)
        {
            //transform.LookAt(target.transform.position);

            float look = LookAtAxis(target.transform.position);

            look = Mathf.LerpAngle(0, look, Time.deltaTime);
            transform.Rotate(0, look, 0);
            nav_agent.isStopped = true;
        }

        // Freeze in navmesh
        //if (Stop == true)
        //{
        //    nav_agent.isStopped = true;
        //}

    }

    private void OnTriggerEnter(Collider col)
    {
        //  Keep distance from player
        if (col.gameObject.tag == "LimitEnemigo")
        {
            look_target = true;
            //Stop = true;
            shot.isShooting = true;

        }
        // Start attacking player
        if (col.gameObject.tag == "AttackPlayer")
        {

            // target = GameObject.Find("Player");
            is_chasing = true;
            //Stop = false;


        }
    }


    // Player Detection
    private void DetectPlayer()
    {
        if(is_chasing && !IsPlayerNear(sight_distance))
        {
            is_chasing = false;
            back_home = true;
        }

        if (IsPlayerInFront() && IsPlayerOnSight())
        {
            //Debug.Log("I see you");
            is_chasing = true;
        }
        else if (IsPlayerNear(alert_distance) && IsPlayerOnSight())
        {
            //Debug.Log("You are near me");
            is_chasing = true;
        }
    }

    private void KeepDistance()
    {
        if (IsPlayerNear(safe_distance))
        {
            look_target = true;
        }
        else
            look_target = false;
    }

    private void ShootTarget()
    {
        if (IsPlayerInFront() && IsPlayerOnSight())
        {
            shot.isShooting = true;
            shot.random = false;
        }
        else
        {
            //shot.isShooting = false;
            shot.random = true;
        }
    }

    private bool IsPlayerNear(float distance)
    {
        Vector3 target_distance = target.transform.position - transform.position;

        if(target_distance.magnitude <= distance)
        {
            //is_chasing = true;          //- Move is_chasing somewhere else
            return true;
        }
        else
        {
            //is_chasing = false;         //- Move is_chasing somewhere else
            return false;
        }
    }

    // Detect player if on range of sight, obstacles between enemy and target are not considered
    private bool IsPlayerInFront()
    {
        Vector3 target_dir = target.transform.position - transform.position;
        target_dir.Normalize();

        float target_angle = Vector3.Angle(transform.forward, target_dir);

        if(sight_angle/2 > Mathf.Abs(target_angle))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    // Detect if player is in the enemy line of sight. Obstacles between enemy and target are considered.
    // Enemies cannot see through same layer obstacles
    private bool IsPlayerOnSight()
    {
        RaycastHit hit;

        //int layer_mask = 1 << 8;

        Vector3 target_dir = target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, target_dir.normalized, out hit, sight_distance))
        {
            if (hit.transform.gameObject.tag == target.tag)
            {
                return true;
            }
            
            // See through same colored objects
            //if(hit.transform.gameObject.layer == this.gameObject.layer)
            //{
            //    float dist_left = distance - (hit.point -see_from).magnitude;

            //    if (IsPlayerOnSight(hit.point, dist_left))
            //        return true;
            //    else
            //        return false;
            //}
        }

        return false;
    }

    private void IsHomeFar()
    {
        Vector3 home_distance = transform.position - home.transform.position;

        if (home_distance.magnitude <= sight_distance)
        {
            is_chasing = true;
        }
        else
        {
            is_chasing = false;
        }
    }

    private void IsInHome()
    {
        if (back_home)
        {
            float dist = nav_agent.remainingDistance;
            if (dist != Mathf.Infinity && nav_agent.pathStatus == NavMeshPathStatus.PathComplete && nav_agent.remainingDistance == 0)
            {
                Debug.Log("Home reached");
                back_home = false;
                patrol.is_patrol = true;
                patrol.ResetPatrol();
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        // Collided with player's bullet
        if((col.gameObject.tag == damaging_tag1 && col.gameObject.layer == damaging_layer1) || (col.gameObject.tag == damaging_tag2 && col.gameObject.layer == damaging_layer2))
        {
            Instantiate(DieEffect.gameObject,transform.position, Quaternion.identity);
            Debug.Log("hOLA");
            Destroy(this.gameObject);
            //- Has to destroy enemy position too


            if (col.gameObject.layer == 16 && friendly_fire == true)
            {
                Destroy(col.gameObject);
            }

        }
    }
    private void OnTriggerExit(Collider col)
    {
        // Keep walking towards player
        if (col.gameObject.tag == "LimitEnemigo")
        {
            is_chasing = true;
            //Stop = false;
            //look_target = false;
            transform.LookAt(target.transform.position);

        }

        // Go back home
        if (col.gameObject.tag == "AttackPlayer")
        {
            is_chasing = false;
            shot.isShooting = false;
            

        }
    }


    //////////////////////////////////////////

    // Get Enemy Color
    public int GetColor()
    {
        return (int)cur_color;
    }


    //- Make a utilities script
    public float LookAtAxis(Vector3 look_at)
    {
        // Calculate point to look at
        Vector3 projection = Vector3.ProjectOnPlane(transform.position - look_at, transform.up);

        // Calculate Angle between current transform.front and object to look at

        return Vector3.Angle(transform.forward, projection) - 180;
    }
}
