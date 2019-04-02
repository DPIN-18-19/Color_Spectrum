using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCall : MonoBehaviour
{
    Enemy enemy;
    EnemyBehaviour behaviour;
    ShieldDetect detect;
    
    //Transform shield_pos;
    float call_distance;
    
	// Use this for initialization
	void Start ()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        behaviour = transform.parent.GetComponent<EnemyBehaviour>();
        detect = transform.parent.GetComponent<ShieldDetect>();

        call_distance = transform.parent.GetComponent<EnemyBehaviour>().alert_distance;
        //shield_pos = transform.Find("ShieldPos");	
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected " + other.transform.name);
        if (behaviour.is_chasing)
        {
            // Comprobar si se detecto enemigo del mismo color
            if (other.transform.name.Contains("Enemy"))
            {
                Debug.Log("Enemy was found");
                Enemy other_enemy = other.GetComponent<Enemy>();
                if (other_enemy != null && other_enemy.GetColor() == enemy.GetColor())
                {
                    Debug.Log("Enemy has same color");
                    if (other_enemy.enemy_type <= Enemy.EnemyType.Shotgun)
                    {
                        if(!CheckOccupees(other.transform))
                        Debug.Log("Enemy is of valid type");
                        // Comprobar validez del enemigo respecto posicion
                        if (detect.IsOnSight(call_distance, other.transform))
                        {
                            Debug.Log("Enemy can reach");
                            // Elegir punto a ocupar
                            ChooseSide(other.transform);
                        }
                    }
                }
            }
        }
    }

    bool CheckOccupees(Transform n_occupee)
    {
        if (transform.GetChild(0).GetComponent<ShieldPos>().SameOccupee(n_occupee) || transform.GetChild(1).GetComponent<ShieldPos>().SameOccupee(n_occupee))
            return true;
        else
            return false;
    }

    void ChooseSide(Transform enemy)
    {
        ShieldPos l_shield = transform.GetChild(0).GetComponent<ShieldPos>();
        ShieldPos r_shield = transform.GetChild(1).GetComponent<ShieldPos>();

        // Escudo izquierdo esta ocupado, ocupar derecha
        if (l_shield.occupied)
        {
            r_shield.Occupy(enemy);
        }

        // Escudo derecho esta ocupado
        if(r_shield.occupied)
        {
            l_shield.Occupy(enemy);
        }

        Debug.Log("Inserting enemy");
        // Comprobar que lado se sitúa más cerca.
        float l_dist = Vector3.Distance(l_shield.transform.position, enemy.position);
        float r_dist = Vector3.Distance(r_shield.transform.position, enemy.position);

        // Lado izquierdo cerca
        if(l_dist <= r_dist)
        {
            l_shield.Occupy(enemy);
        }
        // Lado derecho cerca
        else
        {
            r_shield.Occupy(enemy);
        }
    }

    // Desactivar trigger
    void DeactivateSearch()
    {
        GetComponent<SphereCollider>().enabled = false;
    }
}
