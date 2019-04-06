using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCall : MonoBehaviour
{
    Enemy enemy;
    EnemyBehaviour behaviour;
    ShieldDetect detect;
    SphereCollider s_col;

    //Transform shield_pos;
    float call_distance;

    bool is_calling;
    float call_c, call_dur = 4.0f;
    
	// Use this for initialization
	void Start ()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        behaviour = transform.parent.GetComponent<EnemyBehaviour>();
        detect = transform.parent.GetComponent<ShieldDetect>();
        s_col = GetComponent<SphereCollider>();

        call_distance = transform.parent.GetComponent<EnemyBehaviour>().alert_distance;
        is_calling = true;
        call_c = call_dur;
	}

    private void Update()
    {
        if (is_calling)
            CheckCalling();
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
                    Debug.Log("Enemy type " + other_enemy.enemy_type);
                    if (other_enemy.enemy_type <= Enemy.EnemyType.Shotgun)
                    {
                        Debug.Log("Enemy is of valid type");
                        if (!CheckOccupees(other.transform))
                        {
                            Debug.Log("Enemy is not already in shield");
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
            Debug.Log("L was occupied, go for R");
            r_shield.Occupy(enemy);
            DeactivateSearch();
            return;
        }

        // Escudo derecho esta ocupado
        if(r_shield.occupied)
        {
            Debug.Log("R was occupied, go for L");
            l_shield.Occupy(enemy);
            DeactivateSearch();
            return;
        }

        Debug.Log("Inserting enemy");
        // Comprobar que lado se sitúa más cerca.
        float l_dist = Vector3.Distance(l_shield.transform.position, enemy.position);
        float r_dist = Vector3.Distance(r_shield.transform.position, enemy.position);

        // Lado izquierdo cerca
        if(l_dist <= r_dist)
        {
            Debug.Log("L is near");
            l_shield.Occupy(enemy);
        }
        // Lado derecho cerca
        else
        {
            Debug.Log("R is near");
            r_shield.Occupy(enemy);
        }
    }

    // Desactivar trigger
    public void ActivateSearch()
    {
        is_calling = true;
    }

    // Desactivar trigger
    void DeactivateSearch()
    {
        is_calling = false;
        s_col.enabled = false;
    }

    // Activar y deactivar colision para reducir numero de comprobaciones
    void CheckCalling()
    {
        // Apagar colision en el proximo frame
        if (s_col.enabled)
        {
            s_col.enabled = false;
            call_c = call_dur;
        }

        call_c -= Time.deltaTime;

        // Activar colision durante un frame
        if(call_c < 0)
        {
            Debug.Log("Check shield");
            s_col.enabled = true;
        }
    }
}
