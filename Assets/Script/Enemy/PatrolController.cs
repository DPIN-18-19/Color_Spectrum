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
    List<Transform> patrol_points_l;       // Puntos de patrulla
    float cur_patrol;                       // Actual punto de patrulla
    [HideInInspector]
    public bool is_patrolling = true;       // El enemigo está en el estado "Patrulla"

    [Header("Datos de patrulla")]
    public float patrol_speed = 5;          // Velocidad de patrulla
    float wait_c;                       // Contador de espera
    public float wait_dur;              // Duracion de espera
    bool is_waiting;                        // Estado "Vigilando"

    ///////////////////////////////////////////////////////////////
    void Start()
    {
        // Inicializar componentes
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
    }

    // Buscar todos los puntos de patrulla
    void SearchPatrolPoints()
    {
        patrol_points_l = new List<Transform>();
        Transform patrol_p = transform.parent.Find("PatrolPoints");

        for(int i = 0; i < patrol_p.childCount; ++i)
            patrol_points_l.Add(patrol_p.GetChild(i));
    }

    // Reiniciar patrulla
    public void ResetPatrol()
    {
        if (patrol_points_l.Count != 0)
        {
            cur_patrol = 0;
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
}