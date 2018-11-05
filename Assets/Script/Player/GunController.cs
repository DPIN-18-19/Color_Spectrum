using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public bool is_firing;   // Player is firing gun

    public GameObject bullet;    // Yellow Bullet
    // public GameObject cyan_bullet;      // Blue Bullet
    // public GameObject magenta_bullet;   // Pink Bullet

    // private GameObject cur_bullet;      // Current color bullet


    // Materials
    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;

    // Small flash at the beginning
    //public GameObject EffectYellow;     
    //public GameObject EffectPink;
    //public GameObject EffectBlue;

    public GameObject effect;

    public float FlashTime;         // Flash duration

    // Bullet shells
    public Transform ShellYellow;
    public Transform ShellBlue;
    public Transform ShellPink;
    public Transform ShellEjection; // Shell starting position

    public Transform shell;

    // PLayer's bullet color checker
    //public bool BulletYellow;
    //public bool BulletBlue;
    //public bool BulletPink;
    public float bulletSpeed;       // Bullet speed

    public float timeBetweenShorts; // Firing cooldown
    private float shotCounter;      // Firing cooldown counter

    // Sometimes there is no bullet fired

    public AudioClip FXShotPlayer;
    private AudioSource source;


    public Transform FirePoint;     // Bullet spawn position


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
                Instantiate(bullet, FirePoint.position, FirePoint.rotation);
                //Instantiate(effect, FirePoint.position, FirePoint.rotation);
                source.PlayOneShot(FXShotPlayer);
                //effect.SetActive(true);
                //EffectYellow.SetActive(true);
                Instantiate(shell, ShellEjection.position, ShellEjection.rotation);
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

    void BulletToYellow()
    {
        //cur_bullet = yellow_bullet;
        bullet.GetComponent<Renderer>().material = Yellow_Material;
        effect.GetComponent<SpriteRenderer>().color = Color.yellow;
        effect.GetComponentInChildren<Light>().color = Color.yellow;
        shell.GetComponent<MeshRenderer>().material = Yellow_Material;
        Debug.Log("Change to yellow");
    }

    void BulletToCyan()
    {
        //cur_bullet = cyan_bullet;
        bullet.GetComponent<Renderer>().material = Blue_Material;
        effect.GetComponent<SpriteRenderer>().color = Color.cyan;
        effect.GetComponentInChildren<Light>().color = Color.cyan;
        shell.GetComponent<MeshRenderer>().material = Blue_Material;
        Debug.Log("Change to cyan");
    }

    void BulletToMagenta()
    {
        //cur_bullet = magenta_bullet;
        bullet.GetComponent<Renderer>().material = Pink_Material;
        effect.GetComponent<SpriteRenderer>().color = Color.magenta;
        effect.GetComponentInChildren<Light>().color = Color.magenta;
        shell.GetComponent<MeshRenderer>().material = Pink_Material;
        Debug.Log("Change to magenta");
    }

}
