using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectionController : MonoBehaviour
{
    GameObject target;                  // Objeto a detectar
    
    //public float home_distance;                 // Distancia a la que el enemigo puede estar de casa
    ////public float sight_angle;                   // Campo de visión del enemigo
    //float listen_distance;                      // Distancia a la que el enemigo puede detectar algo
    //bool chase_by_near;
    //bool chase_on_sight;

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
    // Los enemigos no pueden ver a traves de obstaculos de la misma capa
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
