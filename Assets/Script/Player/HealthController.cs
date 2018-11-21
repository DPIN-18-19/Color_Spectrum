using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    //public Text vida;             // Player's health in UI
    float health;                   // Player's current health
    public float max_health;        // Player's maximum health

    float armor;                    // Player's current armor
    public float max_armor;         // Player's maximum armor

    // Dead variables
    public ParticleSystem [] die_effect;    // Effect particle array
    int player_color;                       // Player's color

    //////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start ()
    {
        // Subscribe to event
        ColorChangingController.Instance.ToYellow += ChangeToYellow;
        ColorChangingController.Instance.ToCyan += ChangeToCyan;
        ColorChangingController.Instance.ToMagenta += ChangeToMagenta;

        health = max_health;
        armor = max_armor;
    }
	
	// Update is called once per frame
	void Update ()
    {
        IsDead();

        GameplayManager.GetInstance().health = health;
    }

    // Check if dead
    void IsDead()
    {
        if(health <= 0)
        {
            if(player_color < die_effect.Length)
            {
                //- Search a way to destroy die effect after finishing

                Instantiate(die_effect[player_color].gameObject, transform.position, Quaternion.identity);
                Destroy(gameObject);
            }
        }
    }

    // Substract health
    public void GetDamage(float damage)
    {
        if (armor > 0)
            GetArmorDamage(damage);
        else
            health -= damage;

        Debug.Log("Damaged : " + health);
    }

    // Get back health
    public void RestoreHealth(float cure)
    {
        health += cure;

        if(health > max_health)
            health = max_health;

        Debug.Log("Restored : " + health);
    }

    public void GetArmorDamage(float damage)
    {
        armor -= damage;

        // If armor is destroyed, get out health?
        if (armor < 0)
            GetDamage(Mathf.Abs(armor));
    }

    public void RestoreArmor(float cure)
    {
        armor += cure;

        if (armor > max_armor)
            armor = max_armor;
    }

    // Color dependent functions

    void ChangeToYellow()
    {
        player_color = 0;
    }

    void ChangeToCyan()
    {
        player_color = 1;
    }

    void ChangeToMagenta()
    {
        player_color = 2;
    }
}


// Adeslas
