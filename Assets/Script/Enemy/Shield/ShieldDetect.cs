using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldDetect : DetectionController
{
    // Comprobar si el target esta en rango de vision.
    // Se tienen en cuenta obstaculos entre usuario y objetivo.
    public bool IsOnSight(float distance, Transform n_target)
    {
        RaycastHit[] hits;
        Vector3 target_dir = n_target.position - transform.position;
        hits = Physics.RaycastAll(transform.position, target_dir.normalized, distance);

        int my_target = 0;


        Debug.Log("Magnitude is " + Vector3.Magnitude(target_dir));
        Debug.Log("Distance is " + distance);

        for (int i = 0; i < hits.Length; ++i)
        {
            // Buscar al objetivo
            if (hits[i].transform.gameObject.tag == n_target.tag)
                // Comprobar que se trate del objetivo buscado, en caso que haya varios posibles en línea.
                if (Vector3.Distance(hits[i].transform.position, n_target.position) < 0.1f)
                    my_target = i;
        }

        // El usuario no ha detectado al objetivo
        if (hits.Length == 0 || my_target == hits.Length)
        {
            Debug.Log("Here " + my_target + " " + hits.Length);
            return false;
        }

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
                    // Comprobar si el enemigo no es otro enemigo
                    if(hits[i].transform.GetComponent<Enemy>() == null)
                        // Comprobar si el obstáculo es de diferente color
                        if (GetComponent<Enemy>().cur_color.ToString() != hits[i].transform.gameObject.tag)
                        {
                            Debug.Log("afa");
                            return false;
                        }
            }
        }

        return true;
    }
}
