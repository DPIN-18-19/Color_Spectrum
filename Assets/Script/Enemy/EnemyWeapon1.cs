using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon1 : MonoBehaviour
{

    ////////////////////////////////////////////////
    // Datos de bala
    [Header ("Informacion de Bala")]
    public GameObject bullet;               // Objeto bala
    public float bullet_speed = 10;         // Velocidad de bala
    public float bullet_dmg = 5;            // Dano de bala
    public float bullet_range = 20;         // Rango de bala
    public bool bullet_friend = false;      // Bando de bala

    public Transform weapon;            // Arma de enemigo
    Transform fire_pos;                 // Posicion de salida de bala
    Transform effect_pos;               // Posicion de efecto disparo
    Transform shell_pos;                // Posicion de salida casquillo

    private float shot_c;                   // Contador tiempo entre disparos
    [SerializeField]
    private float shot_dur;                 // Duracion normal entre disparos
    [SerializeField]
    private float shot_dur_max;             // Duracion maxima entre disparos

    [HideInInspector]
    public bool random = false;         // Tipo de disparo aleatorio

    [HideInInspector]
    public bool is_shooting;            // Estado "Disparando"

    ////////////////////////////////////////////////
    // Color
    int bullet_color;

    // Material de balas
    [Header("Elementos de Color")]
    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    // Efecto de disparo
    public ParticleSystem shot_effect_y;
    public ParticleSystem shot_effect_c;
    public ParticleSystem shot_effect_m;

    // Casquillo de bala
    public ParticleSystem shell_efect_y;
    public ParticleSystem shell_efect_c;
    public ParticleSystem shell_efect_m;

    ////////////////////////////////////////////////
    // Audio
    private AudioSource a_source;
    [Header("Efectos de Sonido")]
    public AudioClip FXShotEnemy;

    ///////////////////////////////////////////////
    //Ralentizar
    Slow_Motion Ralentizar;
    private float RalentizarDisparos;
    [Header("Habilidad Ralentizar")]
    public float TiempoRalentizado;

    //////////////////////////////////////////////
    
    void Start()
    {
        a_source = GetComponent<AudioSource>();

        fire_pos = weapon.Find("FirePos");
        effect_pos = weapon.Find("EffectPos");
        shell_pos = weapon.Find("ShellPos");
        
        shot_c = shot_dur;
        bullet_color = gameObject.GetComponent<Enemy>().GetColor();

        RalentizarDisparos = 1f;
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();

    }

    // Update is called once per frame
    void Update()
    {
        if (Ralentizar.ActivateAbility == true)
            a_source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
        if (Ralentizar.ActivateAbility == false)
            a_source.pitch = 1;

        shot_c -= Time.deltaTime;

        // Realizar disparo
        if (shot_c < 0 && is_shooting == true)
        {
            AddaptColor();
            a_source.PlayOneShot(FXShotEnemy);

            GameObject bullet_shot = Instantiate(bullet, fire_pos.position, fire_pos.rotation);
            Vector3 bullet_dir = transform.forward;
            bullet_shot.GetComponent<BulletController>().
                        AddBulletInfo(bullet_color, -bullet_speed,
                        bullet_dir, bullet_dmg, bullet_range, bullet_friend);   //- Create Gun Variables

            // Calculo de tiempo siguiente disparo
            shot_c = NextShotTime() * RalentizarDisparos;
        }

        if(Ralentizar.ActivateAbility == true)
            RalentizarDisparos = Ability_Time_Manager.Instance.Slow_Enemy_Shoot;
        if(Ralentizar.ActivateAbility == false)
            RalentizarDisparos = 1f;
    }

    // Clase de tiempo entre disparos
    float NextShotTime()
    {
        if (!random)
            return shot_dur;
        else
            return Random.Range(shot_dur, shot_dur_max);
    }

    // Change colors of bullet
    void AddaptColor()
    {
        //bullet_color = gameObject.GetComponent<EnemyController>().GetColor();

        switch (bullet_color)
        {
            // Amarillo
            case 0:
                if (bullet.GetComponent<Renderer>() != null)
                    bullet.GetComponent<Renderer>().material = yellow_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)
                    bullet.GetComponent<TrailRenderer>().material = yellow_mat;

                // Crear efectos adicionales de disparo
                Instantiate(shot_effect_y.gameObject, effect_pos.position, effect_pos.rotation);
                Instantiate(shell_efect_y.gameObject, shell_pos.position, shell_pos.rotation);
                break;
            // Cyan
            case 1:
                if (bullet.GetComponent<Renderer>() != null)
                    bullet.GetComponent<Renderer>().material = cyan_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)
                    bullet.GetComponent<TrailRenderer>().material = cyan_mat;

                // Crear efectos adicionales de disparo
                Instantiate(shot_effect_c.gameObject, effect_pos.position, effect_pos.rotation);
                Instantiate(shell_efect_c.gameObject, shell_pos.position, shell_pos.rotation);
                break;
            // Magenta
            case 2:
                if (bullet.GetComponent<Renderer>() != null)
                    bullet.GetComponent<Renderer>().material = magenta_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)
                    bullet.GetComponent<TrailRenderer>().material = magenta_mat;

                // Crear efectos adicionales de disparo
                Instantiate(shot_effect_m.gameObject, effect_pos.position, effect_pos.rotation);
                Instantiate(shell_efect_m.gameObject, shell_pos.position, shell_pos.rotation);
                break;
            default:
                break;
        }
    }
}
