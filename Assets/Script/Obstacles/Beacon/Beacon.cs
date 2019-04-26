using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Beacon : MonoBehaviour
{
    [Header("Datos baliza")]
    [SerializeField]
    string beacon_name;             // Nombre para imprimir en barra de vida

    // Health
    public float max_health;        // Vida de la baliza
    float health;

    public Image healthbar;
    bool bar_active;
    float bar_val;

    // Invincivility
    [SerializeField]
    float invincible_dur;           // Duracion de invencibilidad
    float invincible_c;             // Contador de invencibilidad
    bool is_invincible;             // Comprobador de invencibilidad

    public bool isDamage;

    // Death
    Transform death_pos;
    [SerializeField]
    ParticleSystem death_part;

    // Use this for initialization
    void Start()
    {
        health = max_health;
    }

    // Update is called once per frame
    void Update()
    {
        if (is_invincible)
            IsInvincible();
    }

    // Recibir dano
    public void GetDamage(float damage)
    {
        // Comprobar estado de invencibilidad
        if (!is_invincible)
        {
            Debug.Log("I got hit. Missing " + health);
            isDamage = true;
            health -= damage;
            invincible_c = invincible_dur;
            is_invincible = true;

            //if (bar_active)
            //    UpdateHealthBar();

            if (health < 0)
                IsDead();
        }
    }

    void UpdateHealthBar()
    {
        bar_val = health / max_health;

        healthbar.fillAmount = health / max_health;
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
        DeactivateBeacon();
        Destroy(death_pos.gameObject);
        Destroy(this.gameObject);
    }

    public void ActivateBeacon()
    {
        healthbar.transform.parent.gameObject.SetActive(true);
        bar_active = true;
        healthbar.transform.parent.Find("Name").GetComponent<TextMeshProUGUI>().text = beacon_name;
    }

    public void DeactivateBeacon()
    {
        healthbar.transform.parent.gameObject.SetActive(false);
        bar_active = false;
    }
}