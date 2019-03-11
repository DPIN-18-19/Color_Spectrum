using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotTurretEnemy : MonoBehaviour
{
    int bullet_color;

    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    //public ParticleSystem Shot_effectYellow;
    //public ParticleSystem Shot_effectBlue;
    //public ParticleSystem Shot_effectPink;
    public GameObject bullet;
    public Transform FirePos_1;
    public Transform FirePos_2;
    //public Transform EffectShot;
    public float timeBetweenShots_1 = 3;
    public float TimeShots_1;
    private float TimeShotsMax_1;
    public float timeBetweenShots_2 = 3;
    public float TimeShots_2;
    private float TimeShotsMax_2;
    public bool isShooting;

    public AudioClip FXShotEnemy;
    private AudioSource source;

    Slow_Motion Ralentizar;
    private float RalentizarDisparos;

    public float bullet_speed = 10;
    public float bullet_dmg = 5;
    public float bullet_range = 20;
    public bool bullet_friend = false;

    public bool random = false;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start()
    {
        RalentizarDisparos = 1f;
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        TimeShotsMax_1 = TimeShots_1;
        timeBetweenShots_1 = TimeShots_1;
        TimeShotsMax_2 = TimeShots_2;
        timeBetweenShots_2 = TimeShots_2;

    }
    void Update()
    {
        if (Ralentizar.ActivateAbility == true)
        {
            source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
        }
        if (Ralentizar.ActivateAbility == false)
        {
            source.pitch = 1;
        }

        timeBetweenShots_1 = timeBetweenShots_1 - Time.deltaTime;
        timeBetweenShots_2 = timeBetweenShots_2 - Time.deltaTime;

        if (timeBetweenShots_1 < 0 && isShooting == true)
        {
            AddaptColor();
            source.PlayOneShot(FXShotEnemy);
            // EffectShot.SetActive(true);
            GameObject bullet_shot = Instantiate(bullet, FirePos_1.position, FirePos_1.rotation);
            //bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color,10, 5,20,false);   //- Create Gun Variables
            Vector3 bullet_dir = transform.right;

            bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color, -bullet_speed, bullet_dir, bullet_dmg, bullet_range, bullet_friend);   //- Create Gun Variables
            // Instantiate(Shell, ShellEjection.position, ShellEjection.rotation);
            timeBetweenShots_1 = TimeShots_1 * RalentizarDisparos;
            // Invoke("QuitarEfecto", FlashTime);
        }
        if (timeBetweenShots_2 < 0 && isShooting == true)
        {
            AddaptColor();
            source.PlayOneShot(FXShotEnemy);
            // EffectShot.SetActive(true);
            GameObject bullet_shot = Instantiate(bullet, FirePos_2.position, FirePos_2.rotation);
            //bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color,10, 5,20,false);   //- Create Gun Variables
            Vector3 bullet_dir = transform.right;

            bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color, -bullet_speed, bullet_dir, bullet_dmg, bullet_range, bullet_friend);   //- Create Gun Variables
            // Instantiate(Shell, ShellEjection.position, ShellEjection.rotation);
            timeBetweenShots_2 = TimeShots_2 * RalentizarDisparos;
            // Invoke("QuitarEfecto", FlashTime);
        }
        if (Ralentizar.ActivateAbility == true)
        {
            RalentizarDisparos = Ability_Time_Manager.Instance.Slow_Enemy_Shoot;
        }
        if (Ralentizar.ActivateAbility == false)
        {
            RalentizarDisparos = 1f;
        }
    }
    void AddaptColor()
    {
        bullet_color = gameObject.GetComponent<TurretController>().GetColor();

        switch (bullet_color)
        {
            case 0:
                if (bullet.GetComponent<Renderer>() != null)

                    bullet.GetComponent<Renderer>().material = yellow_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)

                    bullet.GetComponent<TrailRenderer>().material = yellow_mat;

                //Instantiate(Shot_effectYellow.gameObject, EffectShot.position, EffectShot.rotation);

                //Instantiate(ShellEfectYellow.gameObject, ShellEjection.position, ShellEjection.rotation);
                break;
            case 1:
                if (bullet.GetComponent<Renderer>() != null)

                    bullet.GetComponent<Renderer>().material = cyan_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)

                    bullet.GetComponent<TrailRenderer>().material = cyan_mat;

                //Instantiate(Shot_effectBlue.gameObject, EffectShot.position, EffectShot.rotation);

                //Instantiate(ShellEfectBlue.gameObject, ShellEjection.position, ShellEjection.rotation);
                break;
            case 2:
                if (bullet.GetComponent<Renderer>() != null)

                    bullet.GetComponent<Renderer>().material = magenta_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)

                    bullet.GetComponent<TrailRenderer>().material = magenta_mat;

                //Instantiate(Shot_effectPink.gameObject, EffectShot.position, EffectShot.rotation);

                //Instantiate(ShellEfectPink.gameObject, ShellEjection.position, ShellEjection.rotation);

                break;
            default:
                Debug.Log("Hello");
                break;
        }
    }

}
