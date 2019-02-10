using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    // Color
    int bullet_color;

    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;


    public ParticleSystem Shot_effectYellow;
    public ParticleSystem Shot_effectBlue;
    public ParticleSystem Shot_effectPink;

    public ParticleSystem ShellEfectYellow;
    public ParticleSystem ShellEfectBlue;
    public ParticleSystem ShellEfectPink;


    ////////////////////////////////////////////////

    public GameObject bullet;
    public Transform FirePos;
    public Transform EffectShot;
    public float timeBetweenShorts = 3;
    public float TimeShots;
    public float TimeShotsMax = 1.5f;
    public bool isShooting;
    //public GameObject EffectShot;
    //public float FlashTime;
    // public Transform Shell;
    public Transform ShellEjection;
    public AudioClip FXShotEnemy;
    private AudioSource source;

    ///////////////////////////////////////////////

    public float bullet_speed = 10;
    public float bullet_dmg = 5;
    public float bullet_range = 20;
    public bool bullet_friend = false;

    //////////////////////////////////////////////

    public bool random = false;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {
        timeBetweenShorts = TimeShots;
    }

    // Update is called once per frame
    void Update()
    {
        timeBetweenShorts = timeBetweenShorts - Time.deltaTime;

        if (timeBetweenShorts < 0 && isShooting == true)
        {
            AddaptColor();
            source.PlayOneShot(FXShotEnemy);
            // EffectShot.SetActive(true);
            GameObject bullet_shot = Instantiate(bullet, FirePos.position, FirePos.rotation);
            //bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color,10, 5,20,false);   //- Create Gun Variables
            Vector3 bullet_dir = transform.forward;

            bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color, -bullet_speed, bullet_dir, bullet_dmg, bullet_range, bullet_friend);   //- Create Gun Variables
            // Instantiate(Shell, ShellEjection.position, ShellEjection.rotation);
            timeBetweenShorts = TimeShots;
            // Invoke("QuitarEfecto", FlashTime);
        }
    }
    void QuitarEfecto()
    {
        // EffectShot.SetActive(false);
    }

    // Change colors of bullet
    void AddaptColor()
    {
        bullet_color = gameObject.GetComponent<EnemyController>().GetColor();

        switch (bullet_color)
        {
            case 0:
                if (bullet.GetComponent<Renderer>() != null)

                bullet.GetComponent<Renderer>().material = yellow_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)

                    bullet.GetComponent<TrailRenderer>().material = yellow_mat;

                Instantiate(Shot_effectYellow.gameObject, EffectShot.position, EffectShot.rotation);

                Instantiate(ShellEfectYellow.gameObject, ShellEjection.position, ShellEjection.rotation);
                break;
            case 1:
                if (bullet.GetComponent<Renderer>() != null)

                    bullet.GetComponent<Renderer>().material = cyan_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)

                    bullet.GetComponent<TrailRenderer>().material = cyan_mat;

                Instantiate(Shot_effectBlue.gameObject, EffectShot.position, EffectShot.rotation);

                Instantiate(ShellEfectBlue.gameObject, ShellEjection.position, ShellEjection.rotation);
                break;
            case 2:
                if (bullet.GetComponent<Renderer>() != null)

                bullet.GetComponent<Renderer>().material = magenta_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)

                    bullet.GetComponent<TrailRenderer>().material = magenta_mat;

                Instantiate(Shot_effectPink.gameObject, EffectShot.position, EffectShot.rotation);

                Instantiate(ShellEfectPink.gameObject, ShellEjection.position, ShellEjection.rotation);

                break;
            default:
                Debug.Log("Hello");
                break;
        }
    }

    void NextShotTime()
    {
        if (!random)
        {
            timeBetweenShorts = TimeShots;
        }
        else
        {
            timeBetweenShorts = Random.Range(TimeShots, TimeShotsMax);
        }
    }
}
