using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{
    /////////////////////////////////////////////////////////
    // Components

    private AudioSource source;
    
    /////////////////////////////////////////////////////////
    // Guns

    [SerializeField]
   // private weapon_List gun_list;            // Lista de armas;
    public IWeaponChipList eq_weapons;
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
        

    /////////////////////////////////////////////////////////
    // Colors Dependent

    List<Renderer> render_children;
    

    int cur_color;

    /////////////////////////////////////////////////////////
    // Effects


    
    //////////////////////////////////////////////////////////////////////////////

    void Awake()
    {
       
        source = GetComponent<AudioSource>();
        render_children = new List<Renderer>();
        //GetNewWeapon(activated_weapon);
        //contShoot = gunlist.weaponList[2].num_disparos;
        //pistola = true;
    }

    // Use this for initialization
    void Start()
    {
        // Subscribe to Event
        //ColorChangingController.Instance.ToYellow += BulletToYellow;
        //ColorChangingController.Instance.ToCyan += BulletToCyan;
        //ColorChangingController.Instance.ToMagenta += BulletToMagenta;

       
    }

    // Update is called once per frame
    void Update()
    {
        if (is_firing)
        {
            //Debug.Log(gun.GetComponent<GunController>().GetType().Name);
            gun.GetComponent<GunController>().FireBullet();
            //FireWeapon();
            // True  automatica, False manual 
            //is_firing = true;
        }
        else
            gun.GetComponent<GunController>().TriggerRelease();

        gun.GetComponent<GunController>().UpdateCadence();
    }

    //void FireWeapon()
    //{
    //    contShoot = cur_weapon.num_disparos;


    //    if (shotCounter <= 0)
    //    {
    //        Transform spawn = gun.transform.Find("FirePos");
    //        Transform shell = gun.transform.Find("SpawnShell");

    //        do
    //        {
    //            shotCounter = cur_weapon.cadency;
    //            GameObject bullet_shot = Instantiate(cur_weapon.bullet, spawn.position, spawn.rotation);
    //            source.PlayOneShot(cur_weapon.fx_shot);
    //            //Debug.Break();

    //            Vector3 bullet_dir = CalculateBulletDirection();
    //            //bullet_dir = bullet_spawn.transform.TransformDirection(bullet_dir.normalized);

    //            //Random Spray
    //            float randspray = Random.Range(cur_weapon.spray, -cur_weapon.spray);
    //            Quaternion angulo = Quaternion.Euler(0f, randspray, 0f);
    //            bullet_dir = angulo * bullet_dir;

    //            //Debug.DrawLine(cam.GetMousePosInPlane(bullet_spawn_pistola.position), bullet_spawn_pistola.position, Color.cyan);
    //            bullet_shot.GetComponent<BulletController>().AddBulletInfo(cur_color, cur_weapon.speed, bullet_dir, cur_weapon.damage, cur_weapon.range, true);

    //            --contShoot;

    //        } while (contShoot > 0);

    //        //// Change this
    //        if (cur_color == 0)
    //        {
    //            Instantiate(Shot_effectYellow.gameObject, spawn.position, spawn.rotation);
    //            Instantiate(ShellEfectYellow.gameObject, shell.position, shell.rotation);
    //        }

    //        if (cur_color == 1)
    //        {
    //            Instantiate(Shot_effectBlue.gameObject, spawn.position, spawn.rotation);
    //            Instantiate(ShellEfectBlue.gameObject, shell.position, shell.rotation);
    //        }

    //        if (cur_color == 2)
    //        {
    //            Instantiate(Shot_effectPink.gameObject, spawn.position, spawn.rotation);
    //            Instantiate(ShellEfectPink.gameObject, shell.position, shell.rotation);
    //        }
    //    }
    //}

    //Vector3 CalculateBulletDirection()
    //{
    //    RaycastHit hit;
    //    Transform player = GetComponentInParent<PlayerController>().transform;

    //    // Raycast desde el jugador al infinito
    //    if (Physics.Raycast(player.position, player.forward, out hit, Mathf.Infinity))
    //    {
    //        return (player.position - hit.point).normalized;
    //    }

    //    // Disparo al infinito
    //    Debug.Log("Error");
    //    return Vector3.zero;
    //}
    
    public void GetNewWeapon(int id)
    {
        // Eliminar datos de materiales
        TakeOutRenderers();

        // Destruir el arma actualmente equipada
        if (gun != null)
            Destroy(gun);
        
        // Actualizar datos
        activated_weapon = id;
       // Debug.Log("MadeonGun: " + id);
        cur_weapon = eq_weapons.i_weapon_chips[id].base_gun;

        // Crear arma nueva como hijo
        gun = Instantiate(cur_weapon.gun, weapon_pos.transform);
        gun.transform.parent = weapon_pos.transform;
        gun.GetComponent<GunController>().AddGunInfo(cur_weapon);
        //gun.GetComponent<GunController>().UpdateColor(GetComponent<PlayerRenderer>().cur_color);

        // Incluir datos de materiales
        SearchRenderers();
        gun.GetComponent<GunController>().UpdateColor(GetComponentInParent<PlayerRenderer>().cur_color);

        // Actualizar plano de rotacion
        transform.parent.GetComponent<PlayerRotation>().SetNewGunHeight(gun.transform.Find("FirePos"));
    }

    void SearchRenderers()
    {
        render_children.Clear();
        
        // Buscar todos los componentes de un arma que tengan "Renderer"
        if (gun.GetComponentsInChildren<Renderer>().Length != 0)
            render_children.AddRange(gun.GetComponentsInChildren<Renderer>());

        // Incluir los renderers en los objetos actualizados con colores
        GetComponentInParent<PlayerRenderer>().renderersToChangeColor.AddRange(render_children);
        // Pintar el arma con el correspondiente material
        GetComponentInParent<PlayerRenderer>().UpdateColor();
    }

    void TakeOutRenderers()
    {
        // Eliminar los renderers del jugador
        if(render_children.Count != 0)
            foreach (Renderer r in render_children)
                GetComponentInParent<PlayerRenderer>().renderersToChangeColor.Remove(r);

        render_children.Clear();
    }

    // Color dependent functions

    // Changing to yellow
    //protected void BulletToYellow()
    //{
    //    cur_color = 0;
    //    if(cur_weapon.bullet.GetComponent<Renderer>() != null)
    //        cur_weapon.bullet.GetComponent<Renderer>().material = yellow_mat;
    //    if(cur_weapon.bullet.GetComponent<TrailRenderer>() != null)
    //        cur_weapon.bullet.GetComponent<TrailRenderer>().material = yellow_mat;
    //}

    //// Changing to cyan
    //protected void BulletToCyan()
    //{
    //    cur_color = 1;
    //    if (cur_weapon.bullet.GetComponent<Renderer>() != null)
    //        cur_weapon.bullet.GetComponent<Renderer>().material = cyan_mat;
    //    if (cur_weapon.bullet.GetComponent<TrailRenderer>() != null)
    //        cur_weapon.bullet.GetComponent<TrailRenderer>().material = cyan_mat;
    //}

    //// Changing to magenta
    //protected void BulletToMagenta()
    //{
    //    cur_color = 2;
    //    if (cur_weapon.bullet.GetComponent<Renderer>() != null)
    //        cur_weapon.bullet.GetComponent<Renderer>().sharedMaterial = magenta_mat;
    //    if (cur_weapon.bullet.GetComponent<TrailRenderer>() != null)
    //        cur_weapon.bullet.GetComponent<TrailRenderer>().sharedMaterial = magenta_mat;
    //}
}
