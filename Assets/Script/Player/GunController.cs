using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public bool is_firing;   // Player is firing gun

    public GameObject bullet;    // Yellow Bullet
                                 // public GameObject cyan_bullet;      // Blue Bullet
                                 // public GameObject magenta_bullet;   // Pink Bullet

    // private GameObject cur_bullet;      // Current color bullet

    public Transform bullet_spawn;     // Bullet spawn position

    // Materials
    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    // Small flash at the beginning
    //public GameObject EffectYellow;     
    //public GameObject EffectPink;
    //public GameObject EffectBlue;

    public GameObject flash_effect;
    public float flash_time;         // Flash duration

    // Bullet shells
    //public Transform ShellYellow;
    //public Transform ShellBlue;
    //public Transform ShellPink;

    public Transform shell;
    public Transform shell_spawn; // Shell starting position

    // PLayer's bullet color checker
    //public bool BulletYellow;
    //public bool BulletBlue;
    //public bool BulletPink;
    //public float bulletSpeed;       // Bullet speed

    public float timeBetweenShorts;     // Firing cooldown
    private float shotCounter;          // Firing cooldown counter

    // Sometimes there is no bullet fired

    public AudioClip FXShotPlayer;
    private AudioSource source;
    

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

	// Use this for initialization
	void Start () {
        
        // Subscribe to Event
        ColorChangingController.Instance.ToYellow += BulletToYellow;
        ColorChangingController.Instance.ToCyan += BulletToCyan;
        ColorChangingController.Instance.ToMagenta += BulletToMagenta;

        //BulletYellow = true;
        //BulletBlue = false;
        //BulletPink = false;


    }
	
	// Update is called once per frame
	void Update () {

       // if (Input.GetKeyDown("q") && BulletYellow == true)
       // {
       //     BulletYellow = false;
       //     BulletBlue = false;
       //     BulletPink = true;
       // }
       //else if (Input.GetKeyDown("e") && BulletYellow == true)
       // {
       //     BulletYellow = false;
       //     BulletBlue = true;
       //     BulletPink = false;
       // }
       // else if  (Input.GetKeyDown("q") && BulletBlue == true)
       // {
       //     BulletYellow = true;
       //     BulletBlue = false;
       //     BulletPink = false;
            
       // }
       // else if (Input.GetKeyDown("e") && BulletBlue == true)
       // {
       //     BulletYellow = false;
       //     BulletBlue = false;
       //     BulletPink = true;
       // }
       // else if (Input.GetKeyDown("q") && BulletPink == true)
       // {
       //     BulletYellow = false;
       //     BulletBlue = true;
       //     BulletPink = false;
            
       // }
       // else if (Input.GetKeyDown("e") && BulletPink == true)
       // {
       //     BulletYellow = true;
       //     BulletBlue = false;
       //     BulletPink = false;

       // }

        if(is_firing)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = timeBetweenShorts;
                Instantiate(bullet, bullet_spawn.position, bullet_spawn.rotation);
                //Instantiate(flash_effect, bullet_spawn.position, bullet_spawn.rotation);
                source.PlayOneShot(FXShotPlayer);
                //effect.SetActive(true);
                //EffectYellow.SetActive(true);
                Instantiate(shell, shell_spawn.position, shell_spawn.rotation);
                //Invoke("QuitarEfectoYellow", FlashTime);

            }
        }

        //if (isFiring && BulletYellow == true && BulletBlue == false && BulletPink == false )
        //{
        //    shotCounter -= Time.deltaTime;
        //    if(shotCounter <= 0)
        //    {
        //        shotCounter = timeBetweenShorts;
        //        BulletController newBullet = Instantiate(bullet, FirePoint.position, FirePoint.rotation) as BulletController;
        //        source.PlayOneShot(FXShotPlayer);
        //        newBullet.speed = bulletSpeed;
        //        EffectYellow.SetActive(true);
        //        Instantiate(ShellYellow, ShellEjection.position, ShellEjection.rotation);
        //        Invoke("QuitarEfectoYellow", FlashTime);

        //    }
        //}

        //if (isFiring && BulletYellow == false && BulletBlue == true && BulletPink == false)
        //{
        //    shotCounter -= Time.deltaTime;
        //    if (shotCounter <= 0)
        //    {
        //        source.PlayOneShot(FXShotPlayer);
        //        shotCounter = timeBetweenShorts;
        //        Instantiate(bulletBlue , FirePoint.position, FirePoint.rotation) ;
        //        EffectBlue.SetActive(true);
        //        Instantiate(ShellBlue, ShellEjection.position, ShellEjection.rotation);
        //        Invoke("QuitarEfectoBlue", FlashTime);
        //    }
        //}

        //if (isFiring && BulletYellow == false && BulletBlue == false && BulletPink == true)
        //{
        //    shotCounter -= Time.deltaTime;
        //    if (shotCounter <= 0)
        //    {
        //        source.PlayOneShot(FXShotPlayer);
        //        shotCounter = timeBetweenShorts;
        //        Instantiate(bulletPink , FirePoint.position, FirePoint.rotation) ;
        //        EffectPink.SetActive(true);
        //        Instantiate(ShellPink, ShellEjection.position, ShellEjection.rotation);
        //        Invoke("QuitarEfectoPink", FlashTime);
        //    }
        //}
    }
   //void QuitarEfectoYellow()
   // {
   //     EffectYellow.SetActive(false);
   // }
   // void QuitarEfectoBlue()
   // {
   //     EffectBlue.SetActive(false);
   // }
   // void QuitarEfectoPink()
   // {
   //     EffectPink.SetActive(false);
   // }

    // Color dependent functions
    void BulletToYellow()
    {
        //cur_bullet = yellow_bullet;
        bullet.GetComponent<Renderer>().material = yellow_mat;
        flash_effect.GetComponent<SpriteRenderer>().color = Color.yellow;
        flash_effect.GetComponentInChildren<Light>().color = Color.yellow;
        shell.GetComponent<MeshRenderer>().material = yellow_mat;
        Debug.Log("Change to yellow");
    }

    void BulletToCyan()
    {
        //cur_bullet = cyan_bullet;
        bullet.GetComponent<Renderer>().material = cyan_mat;
        flash_effect.GetComponent<SpriteRenderer>().color = Color.cyan;
        flash_effect.GetComponentInChildren<Light>().color = Color.cyan;
        shell.GetComponent<MeshRenderer>().material = cyan_mat;
        Debug.Log("Change to cyan");
    }

    void BulletToMagenta()
    {
        //cur_bullet = magenta_bullet;
        bullet.GetComponent<Renderer>().material = magenta_mat;
        flash_effect.GetComponent<SpriteRenderer>().color = Color.magenta;
        flash_effect.GetComponentInChildren<Light>().color = Color.magenta;
        shell.GetComponent<MeshRenderer>().material = magenta_mat;
        Debug.Log("Change to magenta");
    }

}
