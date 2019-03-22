using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    GameObject target;                  // Objeto a detectar

    // Use this for initialization
    void Start ()
    {
        target = GameObject.FindGameObjectWithTag("Player");
	}

    // Comprobar si el objetivo se encuentra cerca
    public bool IsPlayerNear(float distance)
    {
        // Distancia de usuario a objetivo
        Vector3 target_distance = target.transform.position - transform.position;

        if (target_distance.magnitude <= distance)
            return true;
        else
            return false;
    }
    
    // Comprobar si el objetivo se encuentra en el angulo de vision.
    // Se tienen en cuenta obstaculos entre usuario y objetivo.
    public bool IsPlayerInFront(float sight_angle)
    {
        // Calcular vector director a objetivo
        Vector3 target_dir = target.transform.position - transform.position;

        target_dir.Normalize();
        // Calcular angulo de direccion
        float target_angle = Vector3.Angle(transform.forward, target_dir);

        if (sight_angle / 2 > Mathf.Abs(target_angle))
            return true;
        else
            return false;
    }
    
    // Comprobar si el jugador esta en rango de vision.
    // Se tienen en cuenta obstaculos entre usuario y objetivo.
    public bool IsPlayerOnSight(float distance)
    {
        RaycastHit[] hits;
        Vector3 target_dir = target.transform.position - transform.position;
        hits = Physics.RaycastAll(transform.position, target_dir.normalized, distance);

        int my_target = 0;

        for(int i = 0; i < hits.Length; ++i)
        {
            // Buscar al jugador
            if (hits[i].transform.gameObject.tag == target.tag)
                my_target = i;
        }

        // El usuario no ha detectado al objetivo
        if (hits.Length == 0 || my_target == hits.Length)
            return false;
        
        // Comprobar si existe un objeto entre jugador y enemigo
        for (int i = 0; i < hits.Length; ++i)
        {
            // No comprobar objetivo de nuevo
            if (i != my_target)
            {
                float obstacle_dist = Vector3.Distance(transform.position, hits[i].point);

                // Se puede ver a través del obstáculo
                if (obstacle_dist < target_dir.magnitude)
                    if (GetComponent<Enemy>().cur_color.ToString() != hits[i].transform.gameObject.tag)
                    {
                        Debug.Log("Obstacle " + hits[i].transform.name + " is in front");
                        return false;
                    }
                    else
                        Debug.Log("Obstacle " + hits[i].transform.name + " is not in front");
            }
            Debug.Log("Obstacle " + hits[i].transform.name + " is checked");
        }

        return true;
    }
}
