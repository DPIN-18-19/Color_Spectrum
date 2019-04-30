using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour
{
    ///////////////////////////////////////////////////////////////
    // Variables de componentes
    private NavMeshAgent nav_agent;             // Objeto navmesh
    Animator anim;
    
    ///////////////////////////////////////////////////////////////
    // Estado de patrulla
    List<Transform> patrol_points_l;        // Puntos de patrulla
    float cur_patrol;                       // Actual punto de patrulla
    [HideInInspector]
    public bool is_patrolling = true;       // El enemigo está en el estado "Patrulla"

    [Header("Datos de patrulla")]
    public float patrol_speed = 5;          // Velocidad de patrulla
    float wait_c;                           // Contador de espera
    public float wait_dur;                  // Duracion de espera
    bool is_waiting;                        // Estado "Vigilando"

    ///////////////////////////////////////////////////////////////
    // Comportamiento spawn
    bool is_spawning;                       // Estado "Spawneando"
    float spawn_c;                          // Contador de spawn
    float spawn_dur;                        // Duracion de spawn

    ///////////////////////////////////////////////////////////////
    void Start()
    {
        // Inicializar componentes
        if (nav_agent == null)
            nav_agent = GetComponent<NavMeshAgent>();
        anim = gameObject.GetComponent<Animator>();
        
        // Inicializar datos
        SearchPatrolPoints();
        ResetPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_patrolling)
            CheckArrived();

        if (is_waiting)
            IsWaiting();

        if (is_spawning)
            IsSpawning();
    }

    // Buscar todos los puntos de patrulla
    void SearchPatrolPoints()
    {
        // Inicializar lista 
        if (patrol_points_l == null)
            patrol_points_l = new List<Transform>();
        // Vaciar lista ya existente
        else
            patrol_points_l.Clear();

        Transform patrol_p = transform.parent.Find("PatrolPoints");

        if(patrol_p != null)
            for(int i = 0; i < patrol_p.childCount; ++i)
                if(patrol_p.GetChild(i) != null)
                    patrol_points_l.Add(patrol_p.GetChild(i));
    }

    // Reiniciar patrulla
    public void ResetPatrol()
    {
        if (patrol_points_l.Count != 0)
        {
            cur_patrol = 0;
            nav_agent.isStopped = false;
            nav_agent.SetDestination(patrol_points_l[(int)cur_patrol].position);
            wait_c = wait_dur;
            //nav_agent.speed = patrol_speed;
        }
        else
            is_patrolling = false;
    }
    
    // Comprobar si se ha alcanzado el siguiente punto
    void CheckArrived()
    {
        if (!is_waiting)
        {
            float dist = nav_agent.remainingDistance;
            if (dist != Mathf.Infinity && nav_agent.remainingDistance <= 0.5f /*&& nav_agent.pathStatus == NavMeshPathStatus.PathComplete*/ )
            {
                is_waiting = true;
            }
        }
    }
    
    // Estado de "Vigilar"
    void IsWaiting()
    {
        wait_c -= Time.deltaTime;

        if (wait_c <= 0.1f)
        {
            wait_c = wait_dur;
            is_waiting = false;
            ChooseNextPatrol();
        }
    }

    // Seleccionar siguiente punto de patrulla
    void ChooseNextPatrol()
    {
        if (patrol_points_l.Count != 0)
        {
            float length = patrol_points_l.Count;
            cur_patrol = Mathf.Repeat(++cur_patrol, length);
            nav_agent.SetDestination(patrol_points_l[(int)cur_patrol].position);
        }
    }

    // Pausar patrulla por spawn
    public void PausePatrolBySpawn(float n_spawn_dur)
    {
        is_spawning = true;
        is_patrolling = false;
        spawn_dur = n_spawn_dur;
        spawn_c = spawn_dur;

        if(nav_agent == null)
            nav_agent = GetComponent<NavMeshAgent>();
        // Parar en el sitio
        nav_agent.velocity = Vector3.zero;
        nav_agent.isStopped = true;
    }

    // Estado "Spawn"
    void IsSpawning()
    {
        spawn_c -= Time.deltaTime;

        if (spawn_c < 0.1f)
        {
            // Inicializar patrulla con nuevos puntos
            SearchPatrolPoints();
            ResetPatrol();
            is_spawning = false;
            is_patrolling = true;
        }
    }
}