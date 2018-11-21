using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sniper : MonoBehaviour {

    public bool is_firing;          // Player is firing gun

    public GameObject bullet;       // Bullet to shoot


    public Transform bullet_spawn;     // Bullet spawn position

    // Materials
    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    int cur_color;
    /*public float speed = 20f;
    public float damage = 5;
    public float range = 5;
    public float spray = 5;
    public float cadency;               // Firing cadency
    */
    private float shotCounter;          // Firing cadency counter




    public GameObject flash_effect;
    public float flash_time;         // Flash duration



    public Transform shell;
    public Transform shell_spawn; // Shell starting position

    [SerializeField]
    private weapon_List gunlist;



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
    void Start()
    {

        // Subscribe to Event
        ColorChangingController.Instance.ToYellow += BulletToYellow;
        ColorChangingController.Instance.ToCyan += BulletToCyan;
        ColorChangingController.Instance.ToMagenta += BulletToMagenta;




    }

    // Update is called once per frame
    void Update()
    {


        if (is_firing)
        {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0)
            {
                shotCounter = gunlist.weaponList[1].cadency;
                GameObject bullet_shot = Instantiate(bullet, bullet_spawn.position, bullet_spawn.rotation);

                Vector3 bullet_dir = bullet_spawn.position - cam.GetMousePosInPlane(bullet_spawn.position);
                bullet_dir = bullet_spawn.transform.TransformDirection(bullet_dir.normalized);

                //Random Spray
                float randspray = Random.Range(gunlist.weaponList[1].spray, -gunlist.weaponList[1].spray);
                Quaternion angulo = Quaternion.Euler(0f, randspray, 0f);
                bullet_dir = angulo * bullet_dir;

                Debug.DrawLine(cam.GetMousePosInPlane(bullet_spawn.position), bullet_spawn.position, Color.cyan);
                bullet_shot.GetComponent<BulletController>().AddBulletInfo(cur_color, gunlist.weaponList[1].speed, bullet_dir, gunlist.weaponList[1].damage, gunlist.weaponList[1].range, true);
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


    }




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



