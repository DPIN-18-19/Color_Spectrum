using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [HideInInspector]
    public int bullet_color;
    [HideInInspector]
    public float bullet_speed;
    [HideInInspector] // Bullet speed(m/s)
    public Vector3 bullet_dir;
    [HideInInspector]
    public float bullet_damage;     // Damage bullet makes 
    [HideInInspector]
    public float bullet_range;      // Distance a bullet can travel(m)
    [HideInInspector]
    public float bullet_life_time;  // Time of bullet(s)

    //- Consider making just one variable
    public float wall_active_time = 0.2f;       // Time to get collision back after wall
    float max_wall_active_time;
    public float enemy_active_time = 0.2f;      // Time to get collision back after enemy
    [HideInInspector]
    public string enemy_ignore;            // Enemy color to ignore collision with
   // [HideInInspector]
    public bool friendly;                 // Who shot the bullet? True: Player, False: Enemy

    protected Collider m_collider;            // Bullet collider


    public ParticleSystem DestroyEffectYellow;
    private bool YellowDestroyeffect;
    public ParticleSystem DestroyEffectPink;
    private bool PinkDestroyeffect;
    public ParticleSystem DestroyEffectBlue;
    private bool BlueDestroyeffect;
    public AudioClip DestroyBulletFx;

    public float TimeTrasparenteMat;
    private float MaxTimeTrasparenteMat;
    public bool Trasparente;

    // Hablidad ralentizar bala
    protected float ralentizarVelocidad = 1f;
    float ralentizarDestruccion = 1f;
    protected float MaxRalentizarVelocidad;
    float MaxralentizarDestruccion;
    public float RalentizarVelBala;
    public float RalentizarDesBala;
    private float i_cur_color;
    protected Slow_Motion Ralentizar;

    private AudioSource source;

    PlayerRenderer MaterialsPlayer;
    
   

    public bool Sniper;

    //////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    private void Awake()
    {
        source = gameObject.GetComponent<AudioSource>();
    }

    void Start()
    { 
        max_wall_active_time = wall_active_time;
        MaxRalentizarVelocidad = ralentizarVelocidad;
        MaxralentizarDestruccion = ralentizarDestruccion;

        m_collider = GetComponent<Collider>();

        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        
        MaterialsPlayer = GameObject.Find("Player_Naomi").GetComponent<PlayerRenderer>();
        StartCoroutine(DestroyBullet());
    }


    // Bullet variable initializer
    public virtual void AddBulletInfo(int n_color, float n_speed, Vector3 n_dir, float n_damage, float n_range, bool n_friend)
    //public void AddBulletInfo(int n_color, float n_speed, float n_damage, float n_range, bool n_friend)
    {
        //Debug.Log(n_color);
        // Color dependent variables
        if (n_color == 0)
        {
            YellowDestroyeffect = true;
            i_cur_color = 0;
            this.gameObject.tag = "Yellow";
            enemy_ignore = "EnemyYellow";
            this.gameObject.layer = 8;             //- Take out layers
        }
        else if (n_color == 1)
        {
            BlueDestroyeffect = true;
            i_cur_color = 1;
            this.gameObject.tag = "Blue";
            enemy_ignore = "EnemyBlue";
            this.gameObject.layer = 9;             //- Take out layers
        }
        else if (n_color == 2)
        {
            Debug.Log("SoyColorRosa");
            i_cur_color = 2;
            PinkDestroyeffect = true;
            this.gameObject.tag = "Pink";
            enemy_ignore = "EnemyPink";
            this.gameObject.layer = 10;             //- Take out layers
        }

        //- Take out layers sometime
        // Enemy layers
        if (!n_friend)
            this.gameObject.layer = 16;

        // Non-color dependent variables
        bullet_color = n_color;
        bullet_speed = n_speed;

        //bullet_dir = transform.forward;

        //bullet_dir = transform.TransformDirection(transform.forward);

        //bullet_dir = transform.InverseTransformDirection(n_dir);
        bullet_dir = n_dir;

        //Debug.Log("Direction is: X:" + bullet_dir.x + " Y:" + bullet_dir.y + " Z:" + bullet_dir.z);

        bullet_damage = n_damage;
        bullet_range = n_range;
        bullet_life_time = bullet_range / Mathf.Abs(bullet_speed);

        friendly = n_friend;




    }

    // Update is called once per frame
    void Update()
    {
        if (SceneMan1.Instance.isdead == false)
        {
            if (Ralentizar.ActivateAbility == true && !friendly)
            {
                source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
            }
            if (Ralentizar.ActivateAbility == false && !friendly)
            {
                source.pitch = 1;
            }


            if (Ralentizar.ActivateAbility == false && !friendly)
            {
                this.gameObject.layer = 16;
            }
        }
       // Debug.Log(YellowDestroyeffect);
        //Debug.Log("Update bullet");
        //Debug.Break();
        // Move bullet
        MoveBullet();
        //transform.Translate(transform.forward * bullet_speed * Time.deltaTime);
        //transform.Translate(transform.forward);
        //transform.position += bullet_dir * -bullet_speed * Time.deltaTime;

        if (Trasparente == true)
        {
            MaterialsPlayer.SameColor();
            TimeTrasparenteMat -= Time.deltaTime;
        }
        if (Trasparente == false && TimeTrasparenteMat < 0)
        {
            MaterialsPlayer.ResetColor();
            TimeTrasparenteMat = MaxTimeTrasparenteMat;

        }
        if (TimeTrasparenteMat < 0)
        {
            Trasparente = false;
        }
        
        //Condicion !friendly(Enemigo) Ralentizar.
        if(!friendly && Ralentizar.ActivateAbility == true )
        {
            //Debug.Log("Ralentizado");
            ralentizarVelocidad = Ability_Time_Manager.Instance.Slow_Bullet_Velocity;
            ralentizarDestruccion = Ability_Time_Manager.Instance.Slow_Bullet_Destroy;     
        }
        if (!friendly && Ralentizar.ActivateAbility == false )
        {
            //Debug.Log("DejadoDeRalentizar");
            ralentizarVelocidad = MaxRalentizarVelocidad;
            ralentizarDestruccion = MaxralentizarDestruccion;  
        }
    }

    public float GetColor()
    {
        return i_cur_color;
    }

    // If not collided with anything, destroy
    public virtual IEnumerator DestroyBullet()
    {
        if (Ralentizar.ActivateAbility == false)
        {
            yield return new WaitForSeconds(bullet_life_time);
            // Debug.Log("Out of time");
            Destroy(gameObject);
        }
        if(Ralentizar.ActivateAbility == true)
        {
            yield return new WaitForSeconds(bullet_life_time * Ability_Time_Manager.Instance.Slow_Bullet_Destroy);
            // Debug.Log("Out of time");
            Destroy(gameObject);
        }
    }


    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Player" && this.gameObject.tag == "Pink" && !friendly && Ralentizar.ActivateAbility == true)
        {
            this.gameObject.layer = 10;
        }
        if (col.gameObject.tag == "Player" && this.gameObject.tag == "Pink" && !friendly && Ralentizar.ActivateAbility == false)
        {
            this.gameObject.layer = 16;
        }
        if (col.gameObject.tag == "Player" && this.gameObject.tag == "Yellow" && !friendly && Ralentizar.ActivateAbility == true)
        {
            this.gameObject.layer = 8;
        }
        if (col.gameObject.tag == "Player" && this.gameObject.tag == "Yellow" && !friendly && Ralentizar.ActivateAbility == false)
        {
            this.gameObject.layer = 16;
        }
        if (col.gameObject.tag == "Player" && this.gameObject.tag == "Blue" && !friendly && Ralentizar.ActivateAbility == true)
        {
            this.gameObject.layer = 9;
        }
        if (col.gameObject.tag == "Player" && this.gameObject.tag == "Blue" && !friendly && Ralentizar.ActivateAbility == false)
        {
            this.gameObject.layer = 16;
        }
        //Debug.Log("Enter Collided with " + col.transform.gameObject.tag);
        //- Collision with player is not working
        // Same color obstacle collision
        if (col.gameObject.tag == gameObject.tag)
        {
            Debug.Log("Collided with a wall");
            if (m_collider == null)
                Debug.Log("Error");
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", wall_active_time);
        }


       else if( bullet_color == 0 && col.gameObject.tag == "ParedNoCambioYellow")
        {
            Debug.Log("Collided with a wall");
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", wall_active_time);
        }
       else if (bullet_color == 1 && col.gameObject.tag == "ParedNoCambioBlue")
        {
            Debug.Log("Collided with a wall");
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", wall_active_time);
        }
       else if (bullet_color == 2 && col.gameObject.tag == "ParedNoCambioPink")
        {
            Debug.Log("Collided with a wall");
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", wall_active_time);
        }

        // Collision with player
        else if (col.gameObject.tag == "Player")
        {
            // Enemy bullet
            if (!friendly)
            {
                // Taking damage to player
                if (col.gameObject.GetComponent<ColorChangingController>().GetColor() != bullet_color)
                {
                    col.gameObject.SendMessage("GetDamage", bullet_damage);
                    Destroy(gameObject);
                }
                // Restoring player health
                else
                {
                 //   Debug.Log("Paso");
                    m_collider.enabled = !m_collider.enabled;
                    Trasparente = true;
                    Invoke("ReactivateCollision", 0.5f);
                    //  col.gameObject.SendMessage("RestoreHealth", bullet_damage);
                }

                
            }
            // Player bullet
            //- Bug here
            else
            {
                Debug.Log("collided with player");
                m_collider.enabled = !m_collider.enabled;
                Invoke("ReactivateCollision", 1);
            }
        }
        // Ignore enemies of same color
        else if (col.gameObject.tag == enemy_ignore)
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", enemy_active_time);
        }
        // Collision with any other object
        else if (col.gameObject.tag.Contains("Enemy"))
        {
            Vector3 rotParticle = col.contacts[0].normal;
            Transform bulletRot = gameObject.transform;
            float Angle = Vector3.SignedAngle(gameObject.transform.forward, rotParticle, Vector3.up);

            if (friendly)
            {
                if (col.gameObject.GetComponent<EnemyHealthController>().IsWeak(gameObject.tag, gameObject.layer))
                {
                    Debug.Log("BalaIsWeak");
                    col.gameObject.GetComponent<EnemyHealthController>().GetDamage(bullet_damage);

                    if (YellowDestroyeffect)
                    {
                        Instantiate(DestroyEffectYellow.gameObject, transform.position, Quaternion.LookRotation(Quaternion.Euler(0, Angle, 0) * transform.forward, Vector3.up));



                    }
                    if (PinkDestroyeffect)
                    {
                        Instantiate(DestroyEffectPink.gameObject, transform.position, Quaternion.LookRotation(Quaternion.Euler(0, Angle, 0) * transform.forward, Vector3.up));


                    }
                    if (BlueDestroyeffect)
                    {
                        Instantiate(DestroyEffectBlue.gameObject, transform.position, Quaternion.LookRotation(Quaternion.Euler(0, Angle, 0) * transform.forward, Vector3.up));

                    }
                    
                }
                
            }
            //LLevarselo al hijo
            if(Sniper == false)
            Destroy(gameObject);
        }
        else
        {
            Vector3 rotParticle = col.contacts[0].normal;
            Transform bulletRot = gameObject.transform;
            float Angle = Vector3.SignedAngle(gameObject.transform.forward,rotParticle,Vector3.up);
           // bulletRot.LookAt(col.contacts[0].);
            if (YellowDestroyeffect)
            {
                Instantiate(DestroyEffectYellow.gameObject, transform.position,Quaternion.LookRotation(Quaternion.Euler(0,Angle, 0)* transform.forward, Vector3.up));
                
               
                Destroy(gameObject);
                //AudioSource.PlayClipAtPoint(DestroyBulletFx, transform.position);
            }
            if (PinkDestroyeffect)
            {
                Instantiate(DestroyEffectPink.gameObject, transform.position, Quaternion.LookRotation(Quaternion.Euler(0, Angle, 0) * transform.forward, Vector3.up));
                // Debug.Log("destroy bullet");
                Destroy(gameObject);
              //  AudioSource.PlayClipAtPoint(DestroyBulletFx, transform.position);
            }
            if (BlueDestroyeffect)
            {
                Instantiate(DestroyEffectBlue.gameObject, transform.position, Quaternion.LookRotation(Quaternion.Euler(0, Angle, 0) * transform.forward, Vector3.up));
                //  Debug.Log("destroy bullet");
                Destroy(gameObject);
              //  AudioSource.PlayClipAtPoint(DestroyBulletFx, transform.position);
            }
        }
    }

    void OnCollisionStay(Collision col)
    {
       

        Debug.Log("Collided with " + col.transform.gameObject.tag);

        // Same color obstacle collision
        if (col.gameObject.tag == gameObject.tag)
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", wall_active_time);
        }
        // Collision with player
        else if (col.gameObject.tag == "Player")
        {
            // Enemy bullet
            if (!friendly)
            {
                // Taking damage to player
                if (col.gameObject.GetComponent<ColorChangingController>().GetColor() != bullet_color)
                    col.gameObject.SendMessage("GetDamage", bullet_damage);
                // Restoring player health
                else
                {
                //   col.gameObject.SendMessage("RestoreHealth", bullet_damage);
                }

                Debug.Log("Destry enemy bullet11");
                Destroy(gameObject);
            }
            // Player bullet
            //- Bug here
            else
            {
                Debug.Log("collided with player");
                m_collider.enabled = !m_collider.enabled;
                Invoke("ReactivateCollision", 1);
            }
        }
        // Ignore enemies of same color
        else if (col.gameObject.tag == enemy_ignore)
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", enemy_active_time);
        }
        // Collision with any other object
        else if (col.gameObject.tag.Contains("Enemy"))
        {
            Debug.Log("Enemy hit");
            if (col.gameObject.GetComponent<EnemyHealthController>().IsWeak(gameObject.tag, gameObject.layer))
                col.gameObject.GetComponent<EnemyHealthController>().GetDamage(bullet_damage);

            Destroy(gameObject);
        }
        else
        {
            Debug.Log("Destry enemy bullet");
            Destroy(gameObject);
        }
    }

    void ReactivateCollision()
    {
        m_collider.enabled = !m_collider.enabled;
    }

    protected void MoveBullet()
    {
        Vector3 final_pos = transform.position + bullet_dir * -bullet_speed * Time.deltaTime ;
        // Move only if no collision is found
        if (!PeekNextPosition(final_pos))
            transform.position += bullet_dir * -bullet_speed * Time.deltaTime/ ralentizarVelocidad;
    }

    // Check next position the bullet will move to
    // Return true: Bullet will collide with something
    // Return false: Bullet will not collide
   protected bool PeekNextPosition(Vector3 f_pos)
    {
        RaycastHit hit;
        
        Vector3 ray_dir = transform.position - f_pos;
        float dist = Vector3.Distance(transform.position, f_pos);


        if (Physics.Raycast(transform.position, ray_dir.normalized, out hit, dist))
        {
            //- Take out LimitEnemigo and Attack enemy
            // Move to collision point
            if (hit.transform.gameObject.tag != "Player" && hit.transform.gameObject.tag != gameObject.tag && (!friendly && hit.transform.gameObject.tag != enemy_ignore))
            {
                transform.position = hit.point;
                return true;
            }


            return false;
        }
        else
            return false;
    }
}
