using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PatrolController : MonoBehaviour {

    public Transform[] patrol_points;
    int first_patrol = 0;
    float cur_patrol;

    public float wait_time = 0;

    public float patrol_speed = 5;

    public bool is_patrol = true;


    private NavMeshAgent nav_agent;             // Navmesh object


    // Use this for initialization
    void Start()
    {
        nav_agent = GetComponent<NavMeshAgent>();

        if (is_patrol)
            ResetPatrol();
    }

    // Update is called once per frame
    void Update()
    {
        if (is_patrol)
        {
            CheckArrived();
        }

    }

    public void ResetPatrol()
    {
        if (patrol_points.Length != 0)
        {
            cur_patrol = first_patrol;
            nav_agent.SetDestination(patrol_points[(int)cur_patrol].position);
            //nav_agent.speed = patrol_speed;
        }
    }


    void CheckArrived()
    {
        float dist = nav_agent.remainingDistance;
        if (dist != Mathf.Infinity && nav_agent.pathStatus == NavMeshPathStatus.PathComplete && nav_agent.remainingDistance == 0)
        {
            Debug.Log("Destination reached: " + cur_patrol);
            Invoke("ChooseNextPatrol", wait_time);
        }
    }
    void ChooseNextPatrol()
    {
        if (patrol_points.Length != 0)
        {
            float length = patrol_points.Length;
            cur_patrol = Mathf.Repeat(++cur_patrol, length);

            nav_agent.SetDestination(patrol_points[(int)cur_patrol].position);
        }
    }
}
