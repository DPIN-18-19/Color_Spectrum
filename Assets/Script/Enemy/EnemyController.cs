using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyController : MonoBehaviour
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

    public bool AttackMovePlayer;
    public bool AttackStopPlayer;
    Animator anim;

    // Navmesh variables

    private NavMeshAgent nav_agent;             // Navmesh object
    private GameObject target;                  // Move towards objective
    public Transform[] points;                  // Patrol points (unfinished)
    public GameObject home;                     // Enemy return point

    // Moving variables

    public float chase_speed = 7;

    // Target Detection
    //public float home_distance;                 // Distance the enemy can be from home
    public float alert_distance;                // Distance at which the enemy detects the player by getting near
    public float sight_distance;                // Distance at which the enemy detects the player by sight
    //public float sight_angle;                   // Enemy field of view
    //float listen_distance;                      // Distance at which the enemy detects the player by noise
    //bool chase_by_near;
    //bool chase_on_sight;

    DetectionController detect;

    // Chasing
    public float safe_distance;                 // Distance the enemy can be from the player


    // Shooting
    ShotEnemy shot;                      // Enemy's gun
    public ParticleSystem DieEffect;            // Die particles
    //public bool Stop;                         // (Unused)
    bool friendly_fire;                         // Friendly fire



    // AI variables

    bool is_chasing;                        // Is enemy moving towards objective
    bool look_target = false;               // Rotate enemy when going after player
    bool back_home = false;                 // Is enemy heading back home
    bool in_home = false;
    PatrolController patrol;

    // Sound

    public AudioClip FxDie;
    private AudioSource source;

    // private int despoint;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        source = GetComponent<AudioSource>();

        target = GameObject.FindWithTag("Player");
        nav_agent = GetComponent<NavMeshAgent>();

        detect = GetComponent<DetectionController>();
        shot = GetComponent<ShotEnemy>();

        if(patrol != null)
            patrol = GetComponent<PatrolController>();

        //DieEffect.Stop();
        EnemyColorData();
    }

    void EnemyColorData()
    {
        if (cur_color == Colors.Yellow)
        {
            GetComponent<EnemyHealthController>().ChangeToYellow();
        }
        else if (cur_color == Colors.Magenta)
        {
            GetComponent<EnemyHealthController>().ChangeToMagenta();
        }
        else if (cur_color == Colors.Cyan)
        {
            GetComponent<EnemyHealthController>().ChangeToCyan();
        }
    }

    // Update is called once per frame
    void Update()
    {
        DetectPlayer();
        KeepDistance();
        ShootTarget();
        IsInHome();

        // Go after player
        if (is_chasing == true)
        {
            nav_agent.SetDestination(target.transform.position);
            //nav_agent.speed = chase_speed;

            AttackMovePlayer = true;
            anim.SetBool("Attack", AttackMovePlayer);
            AttackStopPlayer = false;
            anim.SetBool("AttackStop", AttackStopPlayer);

            nav_agent.isStopped = false;

            if (patrol != null)
                patrol.is_patrol = false;

        }
        // Go back home
        if (back_home == true)
        {
            nav_agent.SetDestination(home.transform.position);
            shot.isShooting = false;
        }

        // Rotate towards player instantly
        if (look_target == true)
        {
            //transform.LookAt(target.transform.position);

            float look = LookAtAxis(target.transform.position);

            look = Mathf.LerpAngle(0, look, Time.deltaTime * 15.5f);
            //transform.rotation = Quaternion.LookRotation((target.transform.position - transform.position).normalized, Vector3.up);
            transform.Rotate(0, look, 0);

            AttackStopPlayer = true;
            anim.SetBool("AttackStop", AttackStopPlayer);

            nav_agent.isStopped = true;
        }

        if(in_home)
        {
            AttackMovePlayer = false;
            anim.SetBool("Attack", AttackMovePlayer);
        }
        // Freeze in navmesh
        //if (Stop == true)
        //{
        //    nav_agent.isStopped = true;
        //}

    }

    private void OnTriggerEnter(Collider col)
    {
        ////  Keep distance from player
        //if (col.gameObject.tag == "LimitEnemigo")
        //{
        //    look_target = true;
        //    //Stop = true;
        //    shot.isShooting = true;

        //}
        //// Start attacking player
        //if (col.gameObject.tag == "AttackPlayer")
        //{
        //    // target = GameObject.Find("Player");
        //    is_chasing = true;
        //    //Stop = false;
        //}
    }


    // Player Detection
    private void DetectPlayer()
    {
        if (is_chasing && !detect.IsPlayerNear(sight_distance))
        {
            is_chasing = false;
            back_home = true;
        }

        if (detect.IsPlayerInFront() && detect.IsPlayerOnSight(sight_distance))
        {
            //Debug.Log("I see you");
            is_chasing = true;
            in_home = false;
        }
        else if (detect.IsPlayerNear(alert_distance) && detect.IsPlayerOnSight(sight_distance))
        {
            //Debug.Log("You are near me");
            is_chasing = true;
            in_home = false;
        }
    }

    private void KeepDistance()
    {
        if (detect.IsPlayerNear(safe_distance))
        {
            look_target = true;
        }
        else
            look_target = false;
    }

    private void ShootTarget()
    {
        if (detect.IsPlayerInFront() && detect.IsPlayerOnSight(sight_distance))
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
            Debug.Log("here");
            float dist = nav_agent.remainingDistance;
            if (dist != Mathf.Infinity /*&& nav_agent.pathStatus == NavMeshPathStatus.PathComplete*/ && nav_agent.remainingDistance == 0)
            {
                Debug.Log("Home reached");
                back_home = false;
                in_home = true;

                if (patrol != null)
                {
                    patrol.is_patrol = true;
                    patrol.ResetPatrol();
                }
            }
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        // Collided with player's bullet
        //if ((col.gameObject.tag == damaging_tag1 && col.gameObject.layer == damaging_layer1) || (col.gameObject.tag == damaging_tag2 && col.gameObject.layer == damaging_layer2))
        //{
        //    // Maybe you should code something at bullet controller

        //    Instantiate(DieEffect.gameObject, transform.position, Quaternion.identity);

        //    // Destroy the whole enemy and its positions
        //    Destroy(transform.parent.gameObject);
            
        //    if (col.gameObject.layer == 16 && friendly_fire == true)
        //    {
        //        Destroy(col.gameObject);
        //    }

        //}
        //if(GetComponent<EnemyHealthController>().IsWeak(col.gameObject.tag, col.gameObject.layer))
        //{
        //    GetComponent<EnemyHealthController>().Get
        //}
    }
    //private void OnTriggerExit(Collider col)
    //{
    //    // Keep walking towards player
    //    if (col.gameObject.tag == "LimitEnemigo")
    //    {
    //        is_chasing = true;
    //        //Stop = false;
    //        //look_target = false;
    //        transform.LookAt(target.transform.position);
    //    }

    //    // Go back home
    //    if (col.gameObject.tag == "AttackPlayer")
    //    {
    //        is_chasing = false;
    //        shot.isShooting = false;
    //    }
    //}


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

        return Vector3.SignedAngle(transform.forward, projection, Vector3.up) - 180;
    }
}
