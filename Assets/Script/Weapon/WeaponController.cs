using System.Collections;
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
    private weapon_List gun_list;            // Lista de armas;
    private GunData cur_weapon;           // Datos del arma equipada
    public GameObject gun;
    public int activated_weapon = 0;        // Número de arma activada
    public GameObject weapon_pos;                     // Posición del arma

    /////////////////////////////////////////////////////////
    // Bullet

    //public GameObject bullet;                   // Bullet to shoot

    /////////////////////////////////////////////////////////
    // Logic
   // public PlayerController player;
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

        GetNewWeapon(activated_weapon);
    }

    // Update is called once per frame
    void Update()
    {
        shotCounter -= Time.deltaTime;
        if (is_firing)
        {
            contShoot = cur_weapon.num_disparos;
           

            if (shotCounter <= 0)
            {
                Transform spawn = gun.transform.Find("FirePos");
                Transform shell = gun.transform.Find("SpawnShell");

                do
                {
                    shotCounter = cur_weapon.cadency;
                    GameObject bullet_shot = Instantiate(cur_weapon.bullet, spawn.position, spawn.rotation);
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

                //// Change this
                if (cur_color == 0)
                {
                    Instantiate(Shot_effectYellow.gameObject, spawn.position, spawn.rotation);
                    Instantiate(ShellEfectYellow.gameObject, shell.position, shell.rotation);
                }

                if (cur_color == 1)
                {
                    Instantiate(Shot_effectBlue.gameObject, spawn.position, spawn.rotation);
                    Instantiate(ShellEfectBlue.gameObject, shell.position, shell.rotation);
                }

                if (cur_color == 2)
                {
                    Instantiate(Shot_effectPink.gameObject, spawn.position, spawn.rotation);
                    Instantiate(ShellEfectPink.gameObject, shell.position, shell.rotation);
                }
            }
            // True  automatica, False manual 
            is_firing = true;
        }
    }

    Vector3 CalculateBulletDirection()
    {
        RaycastHit hit;
        Transform player = GetComponentInParent<PlayerController>().transform;

        // Raycast desde el jugador al infinito
        if (Physics.Raycast(player.position, player.forward, out hit, Mathf.Infinity))
        {
            return (player.position - hit.point).normalized;
        }

        // Disparo al infinito
        Debug.Log("Error");
        return Vector3.zero;
    }
    
    public void GetNewWeapon(int id)
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

        // Incluir datos de materiales
        SearchRenderers();

        // Activar habilidad
        GetComponent<AbilityController>().ActivateAbility(cur_weapon.ability);
    }

    void SearchRenderers()
    {
        render_children.Clear();
        
        // Buscar todos los componentes de un arma que tengan "Renderer"
        if (gun.GetComponentsInChildren<Renderer>().Length != 0)
            render_children.AddRange(gun.GetComponentsInChildren<Renderer>());

        // Incluir los renderers en los objetos actualizados con colores
        GetComponentInParent<PlayerController>().renderersToChangeColor.AddRange(render_children);
        // Pintar el arma con el correspondiente material
        GetComponentInParent<PlayerController>().UpdateColor();
    }

    void TakeOutRenderers()
    {
        // Eliminar los renderers del jugador
        if(render_children.Count != 0)
            foreach (Renderer r in render_children)
                GetComponentInParent<PlayerController>().renderersToChangeColor.Remove(r);

        render_children.Clear();
    }

    // Color dependent functions

    // Changing to yellow
    void BulletToYellow()
    {
        cur_color = 0;
        if(cur_weapon.bullet.GetComponent<Renderer>() != null)
            cur_weapon.bullet.GetComponent<Renderer>().material = yellow_mat;
        if(cur_weapon.bullet.GetComponent<TrailRenderer>() != null)
            cur_weapon.bullet.GetComponent<TrailRenderer>().material = yellow_mat;
    }

    // Changing to cyan
    void BulletToCyan()
    {
        cur_color = 1;
        if (cur_weapon.bullet.GetComponent<Renderer>() != null)
            cur_weapon.bullet.GetComponent<Renderer>().material = cyan_mat;
        if (cur_weapon.bullet.GetComponent<TrailRenderer>() != null)
            cur_weapon.bullet.GetComponent<TrailRenderer>().material = cyan_mat;
    }

    // Changing to magenta
    void BulletToMagenta()
    {
        cur_color = 2;
        if (cur_weapon.bullet.GetComponent<Renderer>() != null)
            cur_weapon.bullet.GetComponent<Renderer>().sharedMaterial = magenta_mat;
        if (cur_weapon.bullet.GetComponent<TrailRenderer>() != null)
            cur_weapon.bullet.GetComponent<TrailRenderer>().sharedMaterial = magenta_mat;
    }
}
