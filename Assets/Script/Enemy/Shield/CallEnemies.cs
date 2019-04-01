using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CallEnemies : MonoBehaviour
{
    Enemy enemy;

    Transform shield_pos;

    bool l_fill, r_fill;
    
	// Use this for initialization
	void Start ()
    {
        enemy = GetComponent<Enemy>();
        shield_pos = transform.Find("ShieldPos");	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        // Comprobar si se detecto enemigo del mismo color
        if(other.transform.name.Contains("Enemy"))
        {
            if(other.GetComponent<Enemy>().GetColor() == enemy.GetColor())
            {
                // Comprobar validez del enemigo respecto posicion


                // Elegir punto a ocupar
                other.GetComponent<EnemyBehaviour>().IsProtected(ChooseSide(other.transform));
            }
        }
    }

    Transform ChooseSide(Transform enemy)
    {
        Transform l_shield = shield_pos.GetChild(0);
        Transform r_shield = shield_pos.GetChild(1);

        // Escudo izquierdo esta ocupado
        if (l_fill)
        {
            r_fill = true;
            return r_shield;
        }

        // Escudo derecho esta ocupado
        if(r_fill)
        {
            l_fill = true;
            return l_shield;
        }

        // Comprobar que lado se sitúa más cerca.
        float l_dist = Vector3.Distance(l_shield.position, enemy.position);
        float r_dist = Vector3.Distance(r_shield.position, enemy.position);

        // Lado izquierdo cerca
        if(l_dist <= r_dist)
        {
            l_fill = true;
            return l_shield;
        }
        // Lado derecho cerca
        else
        {
            r_fill = true;
            return r_shield;
        }
    }
}
