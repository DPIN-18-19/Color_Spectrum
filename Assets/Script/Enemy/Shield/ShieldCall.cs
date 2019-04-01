using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldCall : MonoBehaviour
{
    Enemy enemy;
    ShieldDetect detect;

    //Transform shield_pos;

    bool l_fill, r_fill;
    
	// Use this for initialization
	void Start ()
    {
        enemy = transform.parent.GetComponent<Enemy>();
        detect = transform.parent.GetComponent<ShieldDetect>();

        //shield_pos = transform.Find("ShieldPos");	
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Detected " + other.transform.name);
        // Comprobar si se detecto enemigo del mismo color
        if(other.transform.name.Contains("Enemy"))
        {
            Debug.Log("Enemy was found");
            if(other.GetComponent<Enemy>().GetColor() == enemy.GetColor())
            {
                Debug.Log("Enemy has same color");
                // Comprobar validez del enemigo respecto posicion
                if (detect.IsOnSight(GetComponent<SphereCollider>().radius, other.transform))
                {
                    Debug.Log("Enemy can reach");
                    // Elegir punto a ocupar
                    other.GetComponent<EnemyBehaviour>().IsProtected(ChooseSide(other.transform));
                }
            }
        }
    }

    Transform ChooseSide(Transform enemy)
    {
        Transform l_shield = transform.GetChild(0);
        Transform r_shield = transform.GetChild(1);

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

        Debug.Log("Inserting enemy");
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

    // Desactivar trigger
    void DeactivateSearch()
    {
        GetComponent<SphereCollider>().enabled = false;
    }
}
