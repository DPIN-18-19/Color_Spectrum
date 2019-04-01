using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetect : DetectionController
{
    // Comprobar si el jugador esta en rango de vision.
    // Se tienen en cuenta obstaculos entre usuario y objetivo.
    public bool IsOnSight(float distance, Transform n_target)
    {
        RaycastHit[] hits;
        Vector3 target_dir = n_target.position - transform.position;
        hits = Physics.RaycastAll(transform.position, target_dir.normalized, distance);

        int my_target = 0;

        for (int i = 0; i < hits.Length; ++i)
        {
            // Buscar al jugador
            if (hits[i].transform.gameObject.tag == n_target.tag)
                my_target = i;
        }

        // El usuario no ha detectado al objetivo
        if (hits.Length == 0 || my_target == hits.Length)
            return false;

        // Comprobar si existe un objeto entre jugador y enemigo
        for (int i = 0; i < hits.Length; ++i)
        {
            // No comprobar objetivo de nuevo
            if (i == my_target)
                continue;
            else
            {

                float obstacle_dist = Vector3.Distance(transform.position, hits[i].point);

                // Se puede ver a través del obstáculo
                if (obstacle_dist < target_dir.magnitude)
                    if (GetComponent<Enemy>().cur_color.ToString() != hits[i].transform.gameObject.tag)
                    {
                        return false;
                    }
            }
        }

        return true;
    }
}
