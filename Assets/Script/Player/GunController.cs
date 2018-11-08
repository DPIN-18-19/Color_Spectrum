using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public bool isFiring;
    public BulletController bullet;
    public GameObject bulletBlue;
    public GameObject bulletPink;

    public GameObject EffectYellow;
    public GameObject EffectPink;
    public GameObject EffectBlue;
    public float FlashTime;

    public Transform ShellYellow;
    public Transform ShellBlue;
    public Transform ShellPink;
    public Transform ShellEjection;

    public bool BulletYellow;
    public bool BulletBlue;
    public bool BulletPink;
    public float bulletSpeed;

    public float timeBetweenShorts;
    private float shotCounter;

    public AudioClip FXShotPlayer;
    private AudioSource source;


    public Transform FirePoint;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
	// Use this for initialization
	void Start () {
        BulletYellow = true;
        BulletBlue = false;
        BulletPink = false;
    }
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown("q") && BulletYellow == true)
        {
            BulletYellow = false;
            BulletBlue = false;
            BulletPink = true;
        }
       else if (Input.GetKeyDown("e") && BulletYellow == true)
        {
            BulletYellow = false;
            BulletBlue = true;
            BulletPink = false;
        }
        else if  (Input.GetKeyDown("q") && BulletBlue == true)
        {
            BulletYellow = true;
            BulletBlue = false;
            BulletPink = false;
            
        }
        else if (Input.GetKeyDown("e") && BulletBlue == true)
        {
            BulletYellow = false;
            BulletBlue = false;
            BulletPink = true;
        }
        else if (Input.GetKeyDown("q") && BulletPink == true)
        {
            BulletYellow = false;
            BulletBlue = true;
            BulletPink = false;
            
        }
        else if (Input.GetKeyDown("e") && BulletPink == true)
        {
            BulletYellow = true;
            BulletBlue = false;
            BulletPink = false;

        }


        if (isFiring && BulletYellow == true && BulletBlue == false && BulletPink == false )
        {
            shotCounter -= Time.deltaTime;
            if(shotCounter <= 0)
            {
                shotCounter = timeBetweenShorts;
                BulletController newBullet = Instantiate(bullet, FirePoint.position, FirePoint.rotation) as BulletController;
                source.PlayOneShot(FXShotPlayer);
                newBullet.speed = bulletSpeed;
                EffectYellow.SetActive(true);
                Instantiate(ShellYellow, ShellEjection.position, ShellEjection.rotation);
                Invoke("QuitarEfectoYellow", FlashTime);
                
            }
        }

        if (isFiring && BulletYellow == false && BulletBlue == true && BulletPink == false)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                source.PlayOneShot(FXShotPlayer);
                shotCounter = timeBetweenShorts;
                Instantiate(bulletBlue , FirePoint.position, FirePoint.rotation) ;
                EffectBlue.SetActive(true);
                Instantiate(ShellBlue, ShellEjection.position, ShellEjection.rotation);
                Invoke("QuitarEfectoBlue", FlashTime);
            }
        }

        if (isFiring && BulletYellow == false && BulletBlue == false && BulletPink == true)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                source.PlayOneShot(FXShotPlayer);
                shotCounter = timeBetweenShorts;
                Instantiate(bulletPink , FirePoint.position, FirePoint.rotation) ;
                EffectPink.SetActive(true);
                Instantiate(ShellPink, ShellEjection.position, ShellEjection.rotation);
                Invoke("QuitarEfectoPink", FlashTime);
            }
        }
    }
   void QuitarEfectoYellow()
    {
        EffectYellow.SetActive(false);
    }
    void QuitarEfectoBlue()
    {
        EffectBlue.SetActive(false);
    }
    void QuitarEfectoPink()
    {
        EffectPink.SetActive(false);
    }
   
}
