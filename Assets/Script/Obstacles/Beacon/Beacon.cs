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

    // Death
    Transform death_pos;
    [SerializeField]
    ParticleSystem death_part;

	// Use this for initialization
	void Start ()
    {
        health = max_health;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if (is_invincible)
            IsInvincible();
	}

    // Recibir dano
    public void GetDamage(float damage)
    {
        // Comprobar estado de invencibilidad
        if(!is_invincible)
        {
            //Debug.Log("I got hit. Missing " + health);
            health -= damage;
            invincible_c = invincible_dur;
            is_invincible = true;

            if (health < 0)
                IsDead();
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
        {
            //Debug.Log("Finish invincibility");
            is_invincible = false;
        }
    }

    void IsDead()
    {
        death_pos = transform.parent.Find("DieEffectPos");
        Instantiate(death_part, death_pos.position, Quaternion.identity);
        Destroy(death_pos.gameObject);
        Destroy(this.gameObject);
    }
}
