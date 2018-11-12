using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour {

    public bool is_firing;          // Player is firing gun

    public GameObject bullet;       // Bullet to shoot
    // public GameObject cyan_bullet;      // Blue Bullet
    // public GameObject magenta_bullet;   // Pink Bullet

    // private GameObject cur_bullet;      // Current color bullet

    public Transform bullet_spawn;     // Bullet spawn position

    // Materials
    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    int cur_color;
    public float speed = 0.2f;
    public float damage = 5;
    public float range = 5;

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

    public CameraController cam;

    //////////////////////////////////////////////////////////////////////////////

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
                GameObject bullet_shot = Instantiate(bullet, bullet_spawn.position, bullet_spawn.rotation);

                Vector3 bullet_dir = bullet_spawn.position - cam.GetMousePosInPlane(bullet_spawn.position);
                bullet_dir = bullet_spawn.transform.TransformDirection(bullet_dir.normalized);
   
                Debug.DrawLine(cam.GetMousePosInPlane(bullet_spawn.position), bullet_spawn.position, Color.cyan);
                bullet_shot.GetComponent<BulletController>().AddBulletInfo(cur_color, speed, bullet_dir,damage, range, true);
                //Debug.Break();

                // Flash effect
                Quaternion spawn_rot = bullet_spawn.rotation;
                //Debug.Log("Rotation: X:" + spawn_rot.x + " Y:" + spawn_rot.y + " Z:" + spawn_rot.z);
                spawn_rot *= Quaternion.Euler(new Vector3(90, -90, 0));
                Instantiate(flash_effect, bullet_spawn.position, spawn_rot);

                source.PlayOneShot(FXShotPlayer);

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

    // Changing to yellow
    void BulletToYellow()
    {
        //cur_bullet = yellow_bullet;
        cur_color = 0;
        bullet.GetComponent<Renderer>().material = yellow_mat;
        bullet.GetComponent<TrailRenderer>().material = yellow_mat;
        flash_effect.GetComponent<SpriteRenderer>().color = Color.yellow;
        flash_effect.GetComponentInChildren<Light>().color = Color.yellow;
        shell.GetComponent<MeshRenderer>().material = yellow_mat;
    }

    // Changing to cyan
    void BulletToCyan()
    {
        //cur_bullet = cyan_bullet;
        cur_color = 1;
        bullet.GetComponent<Renderer>().material = cyan_mat;
        bullet.GetComponent<TrailRenderer>().material = cyan_mat;
        flash_effect.GetComponent<SpriteRenderer>().color = Color.cyan;
        flash_effect.GetComponentInChildren<Light>().color = Color.cyan;
        shell.GetComponent<MeshRenderer>().material = cyan_mat;
    }

    // Changing to magenta
    void BulletToMagenta()
    {
        //cur_bullet = magenta_bullet;
        cur_color = 2;
        bullet.GetComponent<Renderer>().material = magenta_mat;
        bullet.GetComponent<TrailRenderer>().material = magenta_mat;
        flash_effect.GetComponent<SpriteRenderer>().color = Color.magenta;
        flash_effect.GetComponentInChildren<Light>().color = Color.magenta;
        shell.GetComponent<MeshRenderer>().material = magenta_mat;
    }

}
