﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    /////////////////////////////////////////////////////////
    // Components

    private AudioSource source;
    public CameraController cam;
    
    /////////////////////////////////////////////////////////
    // Guns

    [SerializeField]
    private WeaponList gun_list;            // Lista de armas;
    private GunData cur_weapon;           // Datos del arma equipada
    GameObject gun;
    public int activated_weapon = 0;        // Número de arma activada
    public GameObject weapon_pos;                     // Posición del arma

    /////////////////////////////////////////////////////////
    // Bullet

    public GameObject bullet;                   // Bullet to shoot
    
    /////////////////////////////////////////////////////////
    // Logic

    public bool is_firing;          // Player is firing gun
    private float contShoot;        // Number of bullets to spawn at the same time
    private float shotCounter;      // Firing cadency counter

    /////////////////////////////////////////////////////////
    // Colors Dependent

    List<Renderer> render_children;
    
    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    int cur_color;

    /////////////////////////////////////////////////////////
    // Effects

    public ParticleSystem Shot_effectYellow;
    public ParticleSystem Shot_effectBlue;
    public ParticleSystem Shot_effectPink;

    public ParticleSystem ShellEfectYellow;
    public ParticleSystem ShellEfectBlue;
    public ParticleSystem ShellEfectPink;
    
    //////////////////////////////////////////////////////////////////////////////

    void Awake()
    {
        source = GetComponent<AudioSource>();
        render_children = new List<Renderer>();
        //contShoot = gunlist.weaponList[2].num_disparos;
        //pistola = true;
    }

    // Use this for initialization
    void Start()
    {
        // Subscribe to Event
        ColorChangingController.Instance.ToYellow += BulletToYellow;
        ColorChangingController.Instance.ToCyan += BulletToCyan;
        ColorChangingController.Instance.ToMagenta += BulletToMagenta;

        GetNewWeapon(0);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(cur_weapon.gun.name);
        //Debug.Log(cur_weapon.gun.transform.parent.name);
        Debug.Log("Gun pos: " + cur_weapon.gun.transform.position);

        if (is_firing)
        {
            contShoot = cur_weapon.num_disparos;
            shotCounter -= Time.deltaTime;

            if (shotCounter <= 0)
            {
                Transform spawn = gun.transform.Find("FirePos");
                Vector3 spawn_pos = spawn.TransformDirection(spawn.position);
                Debug.Log("Fire pos: " + spawn.position);
                Debug.Log("Fire pos: " + spawn_pos);

                do
                {
                    Debug.Log("MakeShoot");


                    if (spawn != null)
                    {
                        Debug.Log(spawn.name);
                    }

                    //spawn.position = Vector3.one * 100;

                    shotCounter = cur_weapon.cadency;
                    GameObject bullet_shot = Instantiate(bullet, spawn.position, spawn.rotation);
                    source.PlayOneShot(cur_weapon.fx_shot);
                    //Debug.Break();

                    Vector3 bullet_dir = CalculateBulletDirection();
                    //bullet_dir = bullet_spawn.transform.TransformDirection(bullet_dir.normalized);

                    //Random Spray
                    float randspray = Random.Range(cur_weapon.spray, -cur_weapon.spray);
                    Quaternion angulo = Quaternion.Euler(0f, randspray, 0f);
                    bullet_dir = angulo * bullet_dir;

                    //Debug.DrawLine(cam.GetMousePosInPlane(bullet_spawn_pistola.position), bullet_spawn_pistola.position, Color.cyan);
                    bullet_shot.GetComponent<BulletController>().AddBulletInfo(cur_color, cur_weapon.speed, bullet_dir, cur_weapon.damage, cur_weapon.range, true);
                    

                    --contShoot;

                } while (contShoot > 0);

                if (cur_color == 0)
                {
                    Instantiate(Shot_effectYellow.gameObject, cur_weapon.gun.transform.Find("FirePos").position, cur_weapon.gun.transform.Find("FirePos").rotation);
                    Instantiate(ShellEfectYellow.gameObject, cur_weapon.gun.transform.Find("SpawnShell").position, cur_weapon.gun.transform.Find("SpawnShell").rotation);
                }

                if (cur_color == 1)
                {
                    Instantiate(Shot_effectBlue.gameObject, cur_weapon.gun.transform.Find("FirePos").position, cur_weapon.gun.transform.Find("FirePos").rotation);
                    Instantiate(ShellEfectBlue.gameObject, cur_weapon.gun.transform.Find("SpawnShell").position, cur_weapon.gun.transform.Find("SpawnShell").rotation);
                }

                if (cur_color == 2)
                {
                    Instantiate(Shot_effectPink.gameObject, cur_weapon.gun.transform.Find("FirePos").position, cur_weapon.gun.transform.Find("FirePos").rotation);
                    Instantiate(ShellEfectPink.gameObject, cur_weapon.gun.transform.Find("SpawnShell").position, cur_weapon.gun.transform.Find("SpawnShell").rotation);
                }
            }
            
            //Quaternion spawn_rot = bullet_spawn_sniper.rotation;
            //Quaternion spawn_rot1 = bullet_spawn_escopeta.rotation;
            //Quaternion spawn_rot2 = bullet_spawn_pistola.rotation;

            //Debug.Log("Rotation: X:" + spawn_rot.x + " Y:" + spawn_rot.y + " Z:" + spawn_rot.z);
            //spawn_rot *= Quaternion.Euler(new Vector3(90, -90, 0));
            //spawn_rot1 *= Quaternion.Euler(new Vector3(90, -90, 0));
            //spawn_rot2 *= Quaternion.Euler(new Vector3(90, -90, 0));
            
            //source.PlayOneShot(FXShotPlayer);
            //Invoke("QuitarEfectoYellow", FlashTime);
        }
    }

    Vector3 CalculateBulletDirection()
    {
        RaycastHit hit;
        Transform player = GetComponentInParent<PlayerController>().transform;

        if (Physics.Raycast(player.position, player.forward, out hit, Mathf.Infinity))
        {
            //Debug.Log("Collision with: " + hit.transform.gameObject.name);
            return (player.position - hit.point).normalized;
        }

        Debug.Log("Error");
        return Vector3.zero;
    }
    
    void GetNewWeapon(int id)
    {
        // Eliminar datos de materiales
        TakeOutRenderers();

        // Destruir el arma actualmente equipada
        if (gun != null)
            Destroy(gun);
        
        // Actualizar datos
        activated_weapon = id;
        cur_weapon = gun_list.weapon_list[id];


        // Crear arma nueva como hijo
        gun = Instantiate(cur_weapon.gun, weapon_pos.transform);
        gun.transform.parent = weapon_pos.transform;

        Debug.Log("Parent: " + gun.transform.parent.name);

        // Incluir datos de materiales
        SearchRenderers();
        GetComponentInParent<PlayerController>().renderersToChangeColor.AddRange(render_children);
    }

    void SearchRenderers()
    {
        render_children.Clear();
        
        if (gun.GetComponentsInChildren<Renderer>().Length != 0)
        {
            render_children.AddRange(gun.GetComponentsInChildren<Renderer>());
        }

        //if (gun.GetComponent<Renderer>() != null)
        //    render_children.Add(gun.GetComponent<Renderer>());
    }

    void TakeOutRenderers()
    {
        if(render_children.Count != 0)
            foreach (Renderer r in render_children)
                GetComponentInParent<PlayerController>().renderersToChangeColor.Remove(r);

        render_children.Clear();
    }

    // Color dependent functions

    // Changing to yellow
    void BulletToYellow()
    {
        //cur_bullet = yellow_bullet;
        cur_color = 0;
        bullet.GetComponent<Renderer>().material = yellow_mat;
        bullet.GetComponent<TrailRenderer>().material = yellow_mat;

    }

    // Changing to cyan
    void BulletToCyan()
    {
        //cur_bullet = cyan_bullet;
        cur_color = 1;
        bullet.GetComponent<Renderer>().material = cyan_mat;
        bullet.GetComponent<TrailRenderer>().material = cyan_mat;
    }

    // Changing to magenta
    void BulletToMagenta()
    {
        //cur_bullet = magenta_bullet;
        cur_color = 2;
        bullet.GetComponent<Renderer>().material = magenta_mat;
        bullet.GetComponent<TrailRenderer>().material = magenta_mat;
    }
}
