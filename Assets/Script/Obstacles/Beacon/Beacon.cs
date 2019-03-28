using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Beacon : MonoBehaviour
{
    [Header("Datos baliza")]
    [SerializeField]
    string beacon_name;             // Nombre para imprimir en barra de vida

    // Health
    public float max_health;        // Vida de la baliza
    float health;


    // Invincivility
    [SerializeField]
    float invincible_dur;           // Duracion de invencibilidad
    float invincible_c;             // Contador de invencibilidad
    bool is_invincible;             // Comprobador de invencibilidad

	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (is_invincible)
            IsInvincible();
	}

    // Recibir dano
    void GetDamage(float damage)
    {
        // Comprobar estado de invencibilidad
        if(!is_invincible)
        {
            health -= damage;
            invincible_c = invincible_dur;
            is_invincible = true;
        }
    }

    void UpdateHealthBar()
    {
        float bar_val = health / max_health;
    }

    void IsInvincible()
    {
        invincible_c -= Time.deltaTime;

        if (invincible_c < 0)
            is_invincible = false;
    }
}
