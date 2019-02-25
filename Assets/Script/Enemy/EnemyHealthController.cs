using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthController : MonoBehaviour
{
    string damaging_tag1;
    string damaging_tag2;
    int damaging_layer1;
    int damaging_layer2;
    public Transform PosDeadParticle;
    ///////////////////////////////////////////////////////////////

    // Health variables
    float health;
    float newHealth;                // Player's current health
    public float max_health;        // Player's maximum health

    ///////////////////////////////////////////////////////////////
    // Canvas

    public Image health_bar;

    ///////////////////////////////////////////////////////////////

    // Dead variables
    public ParticleSystem[] die_effect;     // Effect particle array
    int enemy_color;                        // Enemy's color
    public AudioClip FxDie;
    private AudioSource source;

    public EnemyController EnemyMaterials;
    public float TimeDamageMat;
    private float MaxTimeDamageMat;
    public bool Daño;

    public int score = 100;

    //////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        source = GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start()
    {
        MaxTimeDamageMat = TimeDamageMat;
        health = max_health;
        newHealth = health;
    }

    // Update is called once per frame
    void Update()
    {
        
        IsDead();
        HealthBar();

        //health = Mathf.Lerp(health, newHealth, Time.deltaTime * 17);
        if (health > max_health)
            health = max_health;
        if (health < 0)
        {
            health = 0;
        }


        if(Daño == true)
        {
            TimeDamageMat -= Time.deltaTime;
        }
        if (Daño == false && TimeDamageMat <= 0)
        {
            if (EnemyMaterials.GetColor() == 0)
            {
                EnemyMaterials.RestoreChangeToYellow();
            }
            if (EnemyMaterials.GetColor() == 1)
            {
                EnemyMaterials.RestoreChangeToCyan();
            }
            if (EnemyMaterials.GetColor() == 2)
            {
                EnemyMaterials.RestoreChangeToMagenta();
            }

            TimeDamageMat = MaxTimeDamageMat;
        }
        if (TimeDamageMat <= 0)
        {
            Daño = false;
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
                Instantiate(die_effect[enemy_color].gameObject, PosDeadParticle.transform.position, Quaternion.identity);
                ScoreManager.Instance.CountEnemy(score);
                Destroy(transform.parent.gameObject);
            }
        }
    }

    // Substract health
    public void GetDamage(float damage)
    {
        //Debug.Log("dMAA");
        health = health - damage;
        health_bar.fillAmount = health / max_health;


        if (EnemyMaterials.GetColor() == 0)
        {
            EnemyMaterials.ChangeToDamageYellow();
        }
        if (EnemyMaterials.GetColor() == 1)
        {
            EnemyMaterials.ChangeToDamageBlue();
        }
        if (EnemyMaterials.GetColor() == 2)
        {
            EnemyMaterials.ChangeToDamagePink();
            Debug.Log("DañoRosa");
        }
        Daño = true;
        Debug.Log("Daño");
    }

    // Get back health
    //public void RestoreHealth(float cure)
    //{
    //    health = health + cure;

    //    if (health > max_health)
    //        health = max_health;
    //}

    void HealthBar()
    {
        if (health < max_health && health > max_health * 0.5f)
            health_bar.color = Color.green;
        else if (health < max_health * 0.5f)
            health_bar.color = Color.red;
    }

    public bool IsWeak(string other_tag, int other_layer)
    {
        //Debug.Log(other_tag);
        //Debug.Log(other_layer);
        //Debug.Log("Damage Layer1" + damaging_layer1 + " Damage Layer2" + damaging_layer2);
        //Debug.Log("Damage tag1" + damaging_tag1 + " Damage tag2" + damaging_tag2);
        if ((other_tag == damaging_tag1 && other_layer == damaging_layer1) || (other_tag == damaging_tag2 && other_layer == damaging_layer2))
        {
            Debug.Log("Hola1");
            return true;

        }
        else
        {
            Debug.Log("Hola1");
            return false;
        }
    }

    // Color dependent functions

    public void ChangeToYellow()
    {
        damaging_tag1 = "Blue";
        damaging_tag2 = "Pink";
        damaging_layer1 = 9;
        damaging_layer2 = 10;
        enemy_color = 0;
    }

    public void ChangeToCyan()
    {
        damaging_tag1 = "Pink";
        damaging_tag2 = "Yellow";
        damaging_layer1 = 10;
        damaging_layer2 = 8;
        enemy_color = 1;
    }

    public void ChangeToMagenta()
    {
        damaging_tag1 = "Blue";
        damaging_tag2 = "Yellow";
        damaging_layer1 = 9;
        damaging_layer2 = 8;
        enemy_color = 2;
    }
}