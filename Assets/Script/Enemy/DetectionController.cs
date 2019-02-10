using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    GameObject target;


    public float home_distance;                 // Distancia a la que el enemigo puede estar de casa
    //public float alert_distance;                // Distancia con la que el enemigo detecta al jugador al acercarse
    //public float sight_distance;                // Distancia a la que el enemigo puede ver
    public float sight_angle;                   // Campo de visión del enemigo
    float listen_distance;                      // Distancia a la que el enemigo puede detectar algo
    bool chase_by_near;
    bool chase_on_sight;

    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public bool IsPlayerNear(float distance)
    {
        Vector3 target_distance = target.transform.position - transform.position;

        if (target_distance.magnitude <= distance)
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
    public bool IsPlayerInFront()
    {
        Vector3 target_dir = target.transform.position - transform.position;
        target_dir.Normalize();

        float target_angle = Vector3.Angle(transform.forward, target_dir);

        if (sight_angle / 2 > Mathf.Abs(target_angle))
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
    public bool IsPlayerOnSight(float distance)
    {
        RaycastHit hit;

        //int layer_mask = 1 << 8;

        Vector3 target_dir = target.transform.position - transform.position;

        if (Physics.Raycast(transform.position, target_dir.normalized, out hit, distance))
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
}
