using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyBehaviour : MonoBehaviour
{
    //////////////////////////////////////////////////
    // Variables de componentes
    protected NavMeshAgent nav_agent;               // Objeto navmesh
    protected DetectionController detect;           // Deteccion
    EnemyWeapon shot;                                 // Pistola
    PatrolController patrol;                        // Patrulla
    Animator anim;                                  // Animacion
    protected AudioSource a_source;                 // Audio
    
    //////////////////////////////////////////////////
    // Variables de informacion
    GameObject target;              // Objetivo de enemigo
    Transform home;                  // Punto de regreso de enemigo
    
    public float alert_distance;                // Distancia que el enemigo detecta por acercarse
    public float sight_distance;                // Distancia que el enemigo detecta por visión
    public float safe_distance;                 // Distancia que el enemigo mantiene del jugador

    //////////////////////////////////////////////////
    // Variables de comportamiento
    bool is_chasing;                        // El enemigo está en el estado "Perseguir"
    bool is_retreat = false;                // El enemigo está en el estado "Regresar"

    bool in_home = false;                       // El enemigo está en el estado "Casa"
    protected bool is_looking = false;          // El enmigo está en el estado "Mirar"
    
    /////////////////////////////////////////////////////
    // Variables de animacion
    bool attack_moving;             // Pose de atacar desplazandose
    bool attack_in_place;           // Pose de atacar quieto
    float correct_look;             // Aplicar animacion de rotacion

    private void Start()
    {
        // Inicializar variables componentes
        nav_agent = GetComponent<NavMeshAgent>();
        detect = GetComponent<DetectionController>();
        shot = GetComponent<EnemyWeapon>();
        patrol = GetComponent<PatrolController>();
        anim = GetComponent<Animator>();
        a_source = GetComponent<AudioSource>();

        // Inicializar objetos objetivo
        target = GameObject.FindWithTag("Player");
        home = transform.parent.Find("EnemyHome");
    }

    private void Update()
    {
        // Cambiadores de estado
        DetectPlayer();
        KeepDistance();
        ShootTarget();

        // Estados de enemigo
        // Realizar "Perseguir"
        if (is_chasing)
            IsChasing();
        // Realizar "Regresar"
        if (is_retreat)
            IsRetreating();
        // Realizar "Patrullar"
        if(patrol != null && patrol.is_patrol)
            IsPatrolling();
        // Realizar "Mirar"
        if(is_looking)
            IsLooking();
        // Realizar "EnCasa"
        if (in_home)
            IsInHome();

        UpdateAnimState();
    }

    ////////////////////////////////////////////////////////////////////
    // Funciones cambiadores de estado
    // Deteccion de jugador
    private void DetectPlayer()
    {
        // En estado "Perseguir", no se ve al jugador
        if (is_chasing && !detect.IsPlayerNear(sight_distance))
        {
            is_chasing = false;
            is_retreat = true;
        }
        // Comprobar si enemigo ve a jugador
        if (detect.IsPlayerInFront() && detect.IsPlayerNear(sight_distance)) // && detect.IsPlayerOnSight(sight_distance))
        {
            is_chasing = true;
            in_home = false;
        }
        // Comprobar si enemigo detecta jugador cerca. No detecta si hay obstáculo en medio.
        else if (detect.IsPlayerNear(alert_distance) && detect.IsPlayerOnSight(sight_distance))
        {
            is_chasing = true;
            in_home = false;
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
    void ShootTarget()
    {
        // Realizar disparos continuos
        if (detect.IsPlayerInFront() && detect.IsPlayerOnSight(sight_distance))
        {
            shot.is_shooting = true;
            shot.random = false;
        }
        // Realizar disparos aleatorios
        else
        {
            //shot.isShooting = false;
            shot.random = true;
        }
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

    ////////////////////////////////////////////////////////////////////
    // Funciones de estado
    // Estado "Perseguir"
    void IsChasing()
    {
        nav_agent.SetDestination(target.transform.position);
        //nav_agent.speed = chase_speed;

        attack_moving = true;
        attack_in_place = false;

        nav_agent.isStopped = false;

        if (patrol != null)
            patrol.is_patrol = false;
    }

    // Estado "Regresar"
    void IsRetreating()
    {
        nav_agent.SetDestination(home.transform.position);
        shot.is_shooting = false;

        float dist = nav_agent.remainingDistance; // Distancia restante hasta objetivo
        if (dist != Mathf.Infinity && nav_agent.remainingDistance <= 5.0f /*&& nav_agent.pathStatus == NavMeshPathStatus.PathComplete*/)
        {
            is_retreat = false;
            in_home = true;
                
            // Retornar a patrulla si debe
            if (patrol != null)
            {
                patrol.is_patrol = true;
                patrol.ResetPatrol();
            }
        }
    }

    // Estado "Patrullar"
    void IsPatrolling()
    {
        attack_moving = true;
    }

    // Estado "Mirar"
    void IsLooking()
    {
        correct_look = LookAtAxis(target.transform.position);
        correct_look = Mathf.LerpAngle(0, correct_look, Time.deltaTime);// / Slow_Rotation * 15.5f);
        transform.Rotate(0, correct_look, 0);

        attack_in_place = true;

        nav_agent.velocity = Vector3.zero;
        nav_agent.isStopped = true; // Se para el enemigo
    }

    // Estado "EnCasa"
    void IsInHome()
    {
        if (patrol != null && !patrol.is_patrol)
            attack_moving = false;
    }
    
    // Actualizar estado animacion
    void UpdateAnimState()
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
