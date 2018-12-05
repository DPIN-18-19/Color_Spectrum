using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealthController : MonoBehaviour
{
    string damaging_tag1;
    string damaging_tag2;
    int damaging_layer1;
    int damaging_layer2;
    
    float health;
    float newHealth;                // Player's current health
    public float max_health;        // Player's maximum health

    // Dead variables
    public ParticleSystem[] die_effect;     // Effect particle array
    int enemy_color;                        // Enemy's color
    public AudioClip FxDie;
    private AudioSource source;

    //////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        source = GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start()
    {
        health = max_health;
        newHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        IsDead();

        health = Mathf.Lerp(health, newHealth, Time.deltaTime * 17);
        if (health > max_health)
            health = max_health;
        if (health < 0)
        {
            health = 0;
        }
    }

    // Check if dead
    void IsDead()
    {
        if (health <= 0.4)
        {
            //AudioSource.PlayClipAtPoint(FxDie, transform.position);
            if (enemy_color < die_effect.Length)
            {
                Instantiate(die_effect[enemy_color].gameObject, transform.position, Quaternion.identity);
                Destroy(transform.parent.gameObject);
            }
        }
    }

    // Substract health
    public void GetDamage(float damage)
    {
        newHealth = health - damage;
    }

    // Get back health
    public void RestoreHealth(float cure)
    {
        newHealth = health + cure;

        if (health > max_health)
            health = max_health;
    }

    public bool IsWeak(string other_tag, int other_layer)
    {

        if ((other_tag == damaging_tag1 && other_layer == damaging_layer1) || (other_tag == damaging_tag2 && other_layer == damaging_layer2))
            return true;
        else
            return false;
    }
    
    // Color dependent functions

    public void ChangeToYellow()
    {
        damaging_tag1 = "Blue";
        damaging_tag2 = "Pink";
        damaging_layer1 = 12;
        damaging_layer2 = 13;
        enemy_color = 0;
    }

    public void ChangeToCyan()
    {
        damaging_tag1 = "Pink";
        damaging_tag2 = "Yellow";
        damaging_layer1 = 13;
        damaging_layer2 = 11;
        enemy_color = 1;
    }

    public void ChangeToMagenta()
    {
        damaging_tag1 = "Blue";
        damaging_tag2 = "Yellow";
        damaging_layer1 = 12;
        damaging_layer2 = 11;
        enemy_color = 2;
    }
}