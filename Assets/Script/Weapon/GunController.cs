using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    AudioSource source;

    //////////////////////////////////////////////////////////////////
    // Informacion de la bala
    GameObject bullet;              // Bala a disparar
    float bullet_speed;             // Velocidad de la bala
    float bullet_damage;            // Dano de la bala
    float bullet_range;             // Distancia maxima de alcance
    float bullet_spray;             // Angulo de desviacion

    //////////////////////////////////////////////////////////////////
    // Informacion de pistola
    Transform spawn;                    // Posicion de spawn de bala
    Transform shell;                    // Posicion de spawn de cartucho

    float gun_num_bullets = 1.0f;       // Balas disparadas simultaneamente
    float gun_cadence_dur;              // Cadencia del arma
    float cadence;                      // Contador de la cadencia
    
    int weapon_color;                   // Color del arma
    AudioClip fx_shot;                  // Efecto sonoro de disparo

    //////////////////////////////////////////////////////////////////
    // Materiales
    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    //////////////////////////////////////////////////////////////////
    // Efecto disparo
    public ParticleSystem Shot_effectYellow;
    public ParticleSystem Shot_effectBlue;
    public ParticleSystem Shot_effectPink;

    public ParticleSystem ShellEfectYellow;
    public ParticleSystem ShellEfectBlue;
    public ParticleSystem ShellEfectPink;
    
    //////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start ()
    {
        source = GetComponent<AudioSource>();

        ColorChangingController.Instance.ToYellow += BulletToYellow;
        ColorChangingController.Instance.ToCyan += BulletToCyan;
        ColorChangingController.Instance.ToMagenta += BulletToMagenta;
        
        spawn = transform.Find("FirePos");
        shell = transform.Find("SpawnShell");
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    public void AddGunInfo(GunData n_data)
    {
        bullet = n_data.bullet;
        bullet_speed = n_data.speed;
        bullet_damage = n_data.damage;
        bullet_range = n_data.range;
        bullet_spray = n_data.spray;

        gun_cadence_dur = n_data.cadence;
        gun_num_bullets = n_data.num_bullets;
        fx_shot = n_data.fx_shot;
    }

    public void FireBullet()
    {
        if (cadence <= 0)
        {
            float c_bullets = gun_num_bullets;

            do
            {
                Debug.Log(spawn.name);
                GameObject bullet_shot = Instantiate(bullet, spawn.position, spawn.rotation);
                source.PlayOneShot(fx_shot);
                //Debug.Break();

                Vector3 bullet_dir = CalculateBulletDirection();
                //bullet_dir = bullet_spawn.transform.TransformDirection(bullet_dir.normalized);

                //Random Spray
                float randspray = Random.Range(bullet_spray, -bullet_spray);
                Quaternion angulo = Quaternion.Euler(0f, randspray, 0f);
                bullet_dir = angulo * bullet_dir;

                //Debug.DrawLine(cam.GetMousePosInPlane(bullet_spawn_pistola.position), bullet_spawn_pistola.position, Color.cyan);
                bullet_shot.GetComponent<BulletController>().AddBulletInfo(weapon_color, bullet_speed, bullet_dir, bullet_damage, bullet_range, true);

                --c_bullets;

            } while (c_bullets > 0);


            cadence = gun_cadence_dur;
            FireEffect();
        }
    }

    void FireEffect()
    {
        //// Change this
        if (weapon_color == 0)
        {
            Instantiate(Shot_effectYellow.gameObject, spawn.position, spawn.rotation);
            Instantiate(ShellEfectYellow.gameObject, shell.position, shell.rotation);
        }

        if (weapon_color == 1)
        {
            Instantiate(Shot_effectBlue.gameObject, spawn.position, spawn.rotation);
            Instantiate(ShellEfectBlue.gameObject, shell.position, shell.rotation);
        }

        if (weapon_color == 2)
        {
            Instantiate(Shot_effectPink.gameObject, spawn.position, spawn.rotation);
            Instantiate(ShellEfectPink.gameObject, shell.position, shell.rotation);
        }
    }

    public void UpdateCadence()
    {
        cadence -= Time.deltaTime;
    }

    public void UpdateColor(int n_color)
    {
        weapon_color = n_color;
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

    // Color dependent functions

    // Changing to yellow
    protected void BulletToYellow()
    {
        weapon_color = 0;
        if (bullet.GetComponent<Renderer>() != null)
            bullet.GetComponent<Renderer>().material = yellow_mat;
        if (bullet.GetComponent<TrailRenderer>() != null)
            bullet.GetComponent<TrailRenderer>().material = yellow_mat;
    }

    // Changing to cyan
    protected void BulletToCyan()
    {
        weapon_color = 1;
        if (bullet.GetComponent<Renderer>() != null)
            bullet.GetComponent<Renderer>().material = cyan_mat;
        if (bullet.GetComponent<TrailRenderer>() != null)
            bullet.GetComponent<TrailRenderer>().material = cyan_mat;
    }

    // Changing to magenta
    protected void BulletToMagenta()
    {
        weapon_color = 2;
        if (bullet.GetComponent<Renderer>() != null)
            bullet.GetComponent<Renderer>().sharedMaterial = magenta_mat;
        if (bullet.GetComponent<TrailRenderer>() != null)
            bullet.GetComponent<TrailRenderer>().sharedMaterial = magenta_mat;
    }
}
