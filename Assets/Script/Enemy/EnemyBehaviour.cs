using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Variables de componentes
    protected NavMeshAgent nav_agent;               // Objeto navmesh
    protected EnemyHealth health;                   // Salud
    protected DetectionController detect;           // Deteccion
    protected EnemyWeapon shot;                     // Pistola
    protected PatrolController patrol;              // Patrulla
    protected Animator anim;                        // Animacion
    protected AudioSource a_source;                 // Audio

    //////////////////////////////////////////////////
    // Variables de customizacion
    [Header("Customizacion de estados")]
    [SerializeField]
    protected bool can_detect_near = true;               // Puede detectar por cercanía
    [SerializeField]
    protected bool can_see = true;                       // Puede detectar por vista                
    [SerializeField]
    protected bool can_shoot = true;                     // Puede disparar
    [SerializeField]
    protected bool can_move = true;                      // Puede moverse
    [SerializeField]
    protected bool can_rotate = true;
    [SerializeField]
    public bool can_patrol = true;
    [SerializeField]
    protected bool can_change_target = false;            // Puede cambiar de target al jugador

    //////////////////////////////////////////////////
    // Variables de informacion
    [Header("Objetivo")]
    public Transform init_target;               // Objetivo de inicio
    protected Transform target;                 // Objetivo de enemigo
    protected Transform home;                   // Punto de regreso de enemigo
    [Header("Metricas de deteccion")]
    public float alert_distance;                // Distancia que el enemigo detecta por acercarse
    public float sight_distance;                // Distancia que el enemigo detecta por vision
    public float safe_distance;                 // Distancia que el enemigo mantiene del jugador
    public float sight_angle;                   // Angulo de vision del enemigo
    public float lost_distance;                 // Distancia que tras alejarse, el enemigo pierde interes
    int stuck_phase = 2;                            // Fase de "Atascado". 0:Disparo 1:Quieto 2:Fuera
    float stuck_shot;                           // Si se atasca, tiempo que sigue disparando en la dirección que mira
    float stuck_hold;                           // Si se atasca, tiempo que permanece quieto antes de regresar

    //////////////////////////////////////////////////
    // Variables de comportamiento
    public bool is_chasing;                          // El enemigo está en el estado "Perseguir"
    protected bool is_retreat = false;                  // El enemigo está en el estado "Regresar"
    protected bool in_home = false;                     // El enemigo está en el estado "Casa"
    public bool is_looking = false;                  // El enmigo está en el estado "Mirar"
    protected bool is_protected = false;
    protected bool is_stuck = false;                // El enemigo está en el eestado "Atascado"

    float checker_c;
    Transform prev_position;

    /////////////////////////////////////////////////////
    // Variables de animacion
    [HideInInspector]
    public bool attack_moving;             // Pose de atacar desplazandose
    [HideInInspector]
    public bool attack_in_place;           // Pose de atacar quieto
    protected float correct_look;             // Aplicar animacion de rotacion

    ///////////////////////////////////////////////////////
    //RalentizarMovimiento
    protected Slow_Motion Ralentizar;

    protected float MaxSpeedSlow;
    protected float MaxAnimSlow;
    protected float Slow_Rotation;
    protected float MaxRalentizarRotar;

    private void Start()
    {
        // Inicializar variables componentes
        nav_agent = GetComponent<NavMeshAgent>();
        health = GetComponent<EnemyHealth>();
        detect = GetComponent<DetectionController>();
        shot = GetComponent<EnemyWeapon>();
        patrol = GetComponent<PatrolController>();
        anim = GetComponent<Animator>();
        a_source = GetComponent<AudioSource>();
        
        checker_c = 0.5f;
        prev_position = transform;

        // Inicializar objetos objetivo
        //target = GameObject.Find(target_name).transform;
        if (init_target == null)
            init_target = GameObject.Find("Player_Naomi").transform;

        target = init_target;
        home = transform.parent.Find("EnemyHome");
        detect.SetTarget(target);

        if (!can_move)
            nav_agent.speed = 0;

        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        // Habilidad ralentizar
        MaxRalentizarRotar = 1;
        //target = GameObject.FindWithTag("Player");
        MaxSpeedSlow = nav_agent.speed;
        MaxAnimSlow = anim.speed;
    }

    private void Update()
    {
        // Cambiadores de estado
        DetectPlayer();
        if (!is_protected)
            KeepDistance();
        if (can_shoot)
            ShootTarget();

        //if (!is_stuck && !is_retreat && checker_c <= 0)
        //    CheckIfStuck();
        //else
        //    checker_c -= Time.deltaTime;

        if (can_change_target)
            ChangeTarget();
        CheckBeacon();

        // Estados de enemigo
        // Realizar "Perseguir"
        if (is_chasing)
            IsChasing();
        // Realizar "Regresar"
        if (is_retreat)
            IsRetreating();
        // Realizar "Patrullar"
        if (can_patrol && patrol != null && patrol.is_patrolling)
            IsPatrolling();
        // Realizar "Mirar"
        if (can_rotate && is_looking)
            IsLooking();
        // Realizar "EnCasa"
        if (in_home)
            IsInHome();
        // Realizar "Atascado"
        if (is_stuck)
            IsStuck();

        UpdateAnimState();

        // Habilidad ralentizar
        if (Ralentizar.ActivateAbility == true)
        {
            a_source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
            nav_agent.speed = Ability_Time_Manager.Instance.Slow_Enemy_Speed;
            anim.speed = Ability_Time_Manager.Instance.Slow_Enemy_Animation;
            Slow_Rotation = Ability_Time_Manager.Instance.Slow_Enemy_Rotation;
        }
        if (Ralentizar.ActivateAbility == false)
        {
            a_source.pitch = 1;
            nav_agent.speed = MaxSpeedSlow;
            anim.speed = MaxAnimSlow;
            Slow_Rotation = MaxRalentizarRotar;
        }
    }

    ////////////////////////////////////////////////////////////////////
    // Funciones cambiadores de estado
    // Deteccion de jugador
    protected virtual void DetectPlayer()
    {
        // En estado "Perseguir", no se ve al jugador
        if (is_chasing && !detect.IsPlayerNear(lost_distance))
        {
            is_chasing = false;
            is_retreat = true;
            Debug.Log("Retreat2");
        }

        if (!is_protected || !is_chasing)
        {
            if (can_see)
                // Comprobar si enemigo ve a jugador
                if (detect.IsPlayerInFront(sight_angle) && detect.IsPlayerNear(sight_distance) && detect.IsPlayerOnSight(sight_distance))
                {
                    Debug.Log("I see");
                    is_chasing = true;
                    in_home = false;
                }

            if (can_detect_near)
                // Comprobar si enemigo detecta jugador cerca. No detecta si hay obstáculo en medio.
                if (detect.IsPlayerNear(alert_distance) && detect.IsPlayerOnSight(sight_distance))
                {
                    is_chasing = true;
                    in_home = false;
                }
        }
    }

    // El enemigo se encuentra en la distancia de seguridad
    protected virtual void KeepDistance()
    {
        if (detect.IsPlayerNear(safe_distance))
            is_looking = true;
        else
            is_looking = false;
    }

    // Realizar disparos 
    protected virtual void ShootTarget()
    {
        // Realizar disparos continuos
        if (detect.IsPlayerInFront(sight_angle) && detect.IsPlayerOnSight(sight_distance))
        {
            shot.random = false;
        }
        // Realizar disparos aleatorios
        else
            shot.random = true;
    }

    // El enemigo está lejos de su punto de retorno
    private void IsHomeFar()
    {
        Vector3 home_distance = transform.position - home.transform.position;

        if (home_distance.magnitude <= sight_distance)
            is_chasing = true;
        else
            is_chasing = false;
    }



    // Cambiar objetivo de disparo
    void ChangeTarget()
    {
        if (health.GetInDamage())
        {
            target = GameObject.Find("Player_Naomi").transform;
            detect.SetTarget(target);
            // Cuando se cambia de objetivo, forzar a entrar en persecución con nuevo objetivo
        }
    }

    void CheckBeacon()
    {
        // Tratar de buscar otra forma de detectar la eliminación de un target.
        if (target.childCount == 0)
        {
            target = GameObject.Find("Player_Naomi").transform;
            detect.SetTarget(target);
        }
    }

    void CheckIfStuck()
    {
        if (Vector3.Distance(transform.position, prev_position.position) < 0.05f &&
            Vector3.Angle(transform.forward, prev_position.forward) < 1)
        {
            if (stuck_phase != 0)
            {
                Debug.Log("Stuck hold");
                is_looking = true;
                is_stuck = true;
                is_chasing = false;
                stuck_phase = 0;
                attack_in_place = true;
                stuck_shot = 2;
            }
        }
        else
            prev_position = transform;

        checker_c = 0.5f;
    }


    private void OnCollisionStay(Collision other)
    {
        // Collision with a colored wall
        if (other.transform.name.Contains("Pared"))
        {
            if (!is_stuck && !is_retreat && checker_c <= 0 && Vector3.Angle(other.contacts[0].normal, -transform.forward) < 20)
                CheckIfStuck();
            else
                checker_c -= Time.deltaTime;

            //transform.position += (other.contacts[0].point - transform.position).normalized;
        }
    }

    ////////////////////////////////////////////////////////////////////
    // Funciones de estado
    // Estado "Perseguir"
    protected virtual void IsChasing()
    {
        nav_agent.SetDestination(target.position);
        //nav_agent.speed = chase_speed;

        if (can_shoot)
            shot.is_shooting = true;

        attack_moving = true;
        attack_in_place = false;
        is_retreat = false;

        nav_agent.isStopped = false;

        if (can_patrol && patrol != null)
            patrol.is_patrolling = false;
    }

    // Estado "Regresar"
    protected virtual void IsRetreating()
    {
        nav_agent.SetDestination(home.transform.position);

        if (can_shoot)
            shot.is_shooting = false;

        // Recuperar anterior objetivo
        if (can_change_target)
        {
            target = init_target;
            detect.SetTarget(target);
        }

        float dist = nav_agent.remainingDistance; // Distancia restante hasta objetivo
        if (dist != Mathf.Infinity && nav_agent.remainingDistance <= 0.1f /*&& nav_agent.pathStatus == NavMeshPathStatus.PathComplete*/)
        {
            is_retreat = false;
            in_home = true;

            // Retornar a patrulla si debe
            if (can_patrol && patrol != null)
            {
                patrol.is_patrolling = true;
                patrol.ResetPatrol();
            }
        }
    }

    public void IsShooting(bool to_shot)
    {
        if (can_shoot)
            shot.is_shooting = to_shot;
    }

    // Estado "Patrullar"
    protected void IsPatrolling()
    {
        attack_moving = true;
    }

    // Estado "Mirar"
    protected virtual void IsLooking()
    {
        correct_look = LookAtAxis(target.position);
        correct_look = Mathf.LerpAngle(0, correct_look, Time.deltaTime / Slow_Rotation * 15.5f);
        transform.Rotate(0, correct_look, 0);

        if (!is_protected)
        {
            attack_in_place = true;
            nav_agent.velocity = Vector3.zero;
            nav_agent.isStopped = true; // Se para el enemigo
        }
    }

    // Estado "EnCasa"
    protected void IsInHome()
    {
        if (can_patrol && patrol != null && !patrol.is_patrolling)
            attack_moving = false;

        in_home = false;
    }


    void IsStuck()
    {
        // Wait to stop shooting
        if (stuck_phase == 0)
        {
            stuck_shot -= Time.deltaTime;

            if (stuck_shot <= 0)
            {
                Debug.Log("Stuck hold");
                stuck_phase = 1;
                is_chasing = false;
                shot.is_shooting = false;
                stuck_hold = 3;
            }
        }
        // Wait to return home
        else if (stuck_phase == 1)
        {
            stuck_hold -= Time.deltaTime;

            if (stuck_hold <= 0)
            {
                Debug.Log("Go back");
                is_looking = false;
                is_retreat = true;
                is_stuck = false;
                stuck_phase = 2;
                attack_in_place = false;
                attack_moving = true;
                nav_agent.isStopped = false;
            }
        }
    }

    // Estado "Protegido". El enemigo entra en el estado desde que comienza a acercarse a un punto de protección
    public void IsProtected(Transform shield_pos)
    {
        // Dejar de patrullar
        if (can_patrol && patrol != null)
            patrol.is_patrolling = false;

        is_chasing = false;
        is_looking = false;
        is_protected = true;
        nav_agent.isStopped = false;
        nav_agent.SetDestination(shield_pos.position);
    }

    // Actualizar estado animacion
    protected virtual void UpdateAnimState()
    {
        anim.SetBool("Attack", attack_moving);
        anim.SetBool("AttackStop", attack_in_place);

        if (is_looking)
            anim.SetFloat("EnemyTurn", correct_look);
    }

    /////////////////////////////////////////////////////////////
    //- Make a utilities script
    public float LookAtAxis(Vector3 look_at)
    {
        // Calculate point to look at
        Vector3 projection = Vector3.ProjectOnPlane(transform.position - look_at, transform.up);

        // Calculate Angle between current transform.front and object to look at
        return Vector3.SignedAngle(transform.forward, projection, Vector3.up) - 180;
    }
}
