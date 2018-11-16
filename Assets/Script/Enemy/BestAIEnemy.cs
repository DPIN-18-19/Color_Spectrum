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
    public GameObject home;               // Enemy return point

    // Moving variables

    float patrol_speed;
    float chase_speed;

    // Target Detection
    public float home_distance;                 // Distance the enemy can be from home
    public float sight_distance;                // Distance at which the enemy detects the player by sight
    public float sight_angle;
    float listen_distance;                      // Distance at which the enemy detects the player by noise
    bool chase_by_near;
    bool chase_on_sight;

    // Shooting
    public ShotEnemy shot;                      // Enemy's gun
    public ParticleSystem DieEffect;            // Die particles
    //public bool Stop;                         // (Unused)
    public bool friendly_fire;                  // Friendly fire



    // AI variables

    public bool is_chasing;                     // Is enemy moving towards objective
    public bool look_target = false;            // Rotate enemy when going after player

    // private int despoint;

    // Use this for initialization
    void Start () {
        target = GameObject.Find("Player");
        nav_agent = GetComponent<NavMeshAgent>();
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

        // Go after player
        if (is_chasing == true )
        {
            nav_agent.SetDestination(target.transform.position);
            shot.isShooting = true;
            nav_agent.isStopped = false;
            
        }
        // Go back home
        if (is_chasing == false)
        {
            nav_agent.SetDestination(home.transform.position);
        }

        // Rotate towards player instantly
        if(look_target == true)
        {
            transform.LookAt(target.transform.position);
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

    private void DetectPlayer()
    {
        if (IsPlayerNear() && IsPlayerOnSight())
            is_chasing = true;
        else
            is_chasing = false;
    }

    private bool IsPlayerNear()
    {
        Vector3 target_distance = target.transform.position - transform.position;

        if(target_distance.magnitude <= sight_distance)
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
    private bool IsPlayerOnSight()
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
}
