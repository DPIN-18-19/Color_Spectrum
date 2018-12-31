using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthController : MonoBehaviour
{
    //public Text vida;             // Player's health in UI
    public float health;
    float newHealth;                // Player's current health
    public float max_health;        // Player's maximum health
    public ColorChangingController cambioColor;
    float armor;                    // Player's current armor
    public float max_armor;         // Player's maximum armor

    // Dead variables
    public ParticleSystem [] die_effect;    // Effect particle array
    int player_color;                       // Player's color
    public AudioClip FxDie;
    private AudioSource source;

    public ParticleSystem HealthYellow;
    public ParticleSystem HealthBlue;
    public ParticleSystem HealthPink;

    public PlayerController MaterialsPlayer;

    public float TimeDamageMat;
    private float MaxTimeDamageMat;
    public bool Daño;

    public float TimeHealtheMat;
    public float MaxTimeHealhthMat;
    public bool curar;

    public float TimeGlitchtheMat;
    public float MaxGlitchthMat;
    public bool ParedNopasar;
   
    public ColorChangingController BlackGlitch;

    public PlayerJaneMoveController MovePlayer;


    //////////////////////////////////////////////////////////////////////////////
    void Awake()
    {
        source = GetComponent<AudioSource>();

    }
    // Use this for initialization
    void Start ()
    {
       // TimeGlitchtheMat = MaxGlitchthMat;
        MaxTimeDamageMat = TimeDamageMat;
        //  MaxTimeHealhthMat = TimeHealtheMat;
        // Subscribe to event
        ColorChangingController.Instance.ToYellow += ChangeToYellow;
        ColorChangingController.Instance.ToCyan += ChangeToCyan;
        ColorChangingController.Instance.ToMagenta += ChangeToMagenta;
        if(GameplayManager.GetInstance() != null)
        GameplayManager.GetInstance().max_health = max_health;


        health = max_health;
        newHealth = health;
        armor = max_armor;
    }

    // Update is called once per frame
    void Update()
    {
       // Debug.Log(health);
        IsDead();
        GameplayManager.GetInstance().health = health;
        health = Mathf.Lerp(health, newHealth, Time.deltaTime * 17);
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

        if (Daño == false && TimeDamageMat < 0)
        {
            if (ParedNopasar == false)
            {

                if (cambioColor.GetColor() == 0)
                {
                    MaterialsPlayer.RestoreChangeToYellow();
                }
                if (cambioColor.GetColor() == 1)
                {
                    MaterialsPlayer.RestoreChangeToCyan();
                }
                if (cambioColor.GetColor() == 2)
                {
                    MaterialsPlayer.RestoreChangeToMagenta();
                }
                TimeDamageMat = MaxTimeDamageMat;
            }
            if(ParedNopasar == true)
            {
                if (cambioColor.GetColor() == 0)
                {
                    MaterialsPlayer.ChangeToBlackYellow();
                }
                if (cambioColor.GetColor() == 1)
                {
                    MaterialsPlayer.ChangeToBlackBlue();
                }
                if (cambioColor.GetColor() == 2)
                {
                    MaterialsPlayer.ChangeToBlackPink();
                }
                TimeDamageMat = MaxTimeDamageMat;
            }
        }

        if( TimeDamageMat < 0)
        {
            Daño = false;
        }
       
        if (curar == true)
        {
            TimeHealtheMat -= Time.deltaTime;
        }
        if (curar == false && TimeHealtheMat < 0)
        {
            if (cambioColor.GetColor() == 0)
            {
                MaterialsPlayer.RestoreChangeToYellow();
            }
            if (cambioColor.GetColor() == 1)
            {
                MaterialsPlayer.RestoreChangeToCyan();
            }
            if (cambioColor.GetColor() == 2)
            {
                MaterialsPlayer.RestoreChangeToMagenta();
            }
            TimeHealtheMat = MaxTimeHealhthMat;

        }
        if (TimeHealtheMat < 0)
        {
            curar = false;
        }
        
        if(cambioColor.ParedCambioNo == true)
        {
            ParedNopasar = true;
            TimeGlitchtheMat += Time.deltaTime;
            if (TimeGlitchtheMat < MaxGlitchthMat)
            {
                if (cambioColor.GetColor() == 0)
                {
                    MaterialsPlayer.ChangeToBlackGlitchYellow();
                }
                if (cambioColor.GetColor() == 1)
                {
                    MaterialsPlayer.ChangeToBlackGlitchBlue();
                }
                if (cambioColor.GetColor() == 2)
                {
                    MaterialsPlayer.ChangeToBlackGlitchPink();
                }
            }
            if (TimeGlitchtheMat > MaxGlitchthMat && Daño == false )
            {
                if (cambioColor.GetColor() == 0)
                {
                    MaterialsPlayer.ChangeToBlackYellow();
                }
                if (cambioColor.GetColor() == 1)
                {
                    MaterialsPlayer.ChangeToBlackBlue();
                }
                if (cambioColor.GetColor() == 2)
                {
                    MaterialsPlayer.ChangeToBlackPink();
                }
            }
            if (TimeGlitchtheMat > MaxGlitchthMat && Daño == true)
            {

                if (cambioColor.GetColor() == 0)
                {
                    MaterialsPlayer.ChangeToDamageYellow();
                }
                if (cambioColor.GetColor() == 1)
                {
                    MaterialsPlayer.ChangeToDamageBlue();
                }
                if (cambioColor.GetColor() == 2)
                {
                    MaterialsPlayer.ChangeToDamagePink();
                }

               // TimeDamageMat -= Time.deltaTime;

            }


            if (TimeGlitchtheMat > cambioColor.MaxDuracion)
            {
                if (cambioColor.GetColor() == 0)
                {
                    MaterialsPlayer.RestoreChangeToYellow();
                }
                if (cambioColor.GetColor() == 1)
                {
                    MaterialsPlayer.RestoreChangeToCyan();
                }
                if (cambioColor.GetColor() == 2)
                {
                    MaterialsPlayer.RestoreChangeToMagenta();
                }
                TimeGlitchtheMat = 0;
                ParedNopasar = false;
            }
        }
    }

    // Check if dead
    void IsDead()
    {
        if (health <= 0.4)
        {
            AudioSource.PlayClipAtPoint(FxDie, transform.position);
            if (player_color < die_effect.Length)
            {
                //- Search a way to destroy die effect after finishing

                Instantiate(die_effect[player_color].gameObject, transform.position, Quaternion.identity);

                GameObject.Find("GameManager").GetComponent<SceneMan>().Invoke("ToMenu", 2);

                gameObject.SetActive(false);

                MovePlayer.CanMove = false;

               // Destroy(gameObject);
            }
        }
    }

    // Substract health
    public void GetDamage(float damage)
    {
        if (armor > 0)
            GetArmorDamage(damage);
        else
        {
            newHealth = health - damage;

            if (cambioColor.GetColor() == 0)
            {
                MaterialsPlayer.ChangeToDamageYellow();

            }
            if (cambioColor.GetColor() == 1)
            {
                MaterialsPlayer.ChangeToDamageBlue();
            }
            if (cambioColor.GetColor() == 2)
            {
                MaterialsPlayer.ChangeToDamagePink();
            }

            ScoreManager.Instance.CountDamage(damage);
            Daño = true;
        }
    }




    // Get back health
    public void RestoreHealth(float cure)
    {
        newHealth = health + cure;
        //health += cure;
        if (cambioColor.GetColor() == 0)
        {
            MaterialsPlayer.ChangeToHealthYellow();
            Instantiate(HealthYellow.gameObject, transform.position, Quaternion.identity);
        }
        if (cambioColor.GetColor() == 1)
        {
            MaterialsPlayer.ChangeToHealthBlue();
            Instantiate(HealthBlue.gameObject, transform.position, Quaternion.identity);
        }
        if (cambioColor.GetColor() == 2)
        {
            MaterialsPlayer.ChangeToHealthPink();
            Instantiate(HealthPink.gameObject, transform.position, Quaternion.identity);
        }
        curar = true;
        if (health > max_health)
        {
            health = max_health;
          
        }
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
