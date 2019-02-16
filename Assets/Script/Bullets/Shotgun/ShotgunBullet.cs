using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : BulletController
{
    ////////////////////////////////////////////////////////////////////////
    // Shotgun Specific variables

   public float increase_time = 20f;
    float fade_time = 0.15f;
    public bool is_lerping;

    private Collider s_collider;            // Bullet collider
    SpriteRenderer m_sprite;
    ParticleSystem []m_particle;


    public bool is_waiting;
    public float RalentizarBala;

    //Slow_Motion Ralentizar1;
    float tiempoNormal = 1f;
    float TiempoRalentizado;
    public float SlowBullet;


    // Use this for initialization
    void Start ()
    {
        MaxRalentizarVelocidad = ralentizarVelocidad;

        m_sprite = GetComponentInChildren<SpriteRenderer>();
        s_collider = GetComponentInChildren<Collider>();
        m_particle = GetComponentsInChildren<ParticleSystem>();
        //m_particle.material.color = Color.yellow;
        //Debug.Log(s_collider.name);
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        TiempoRalentizado = 1f;

        InitBullet();
        ColorParticle();


    }

    private void FixedUpdate()
    {
       if(Ralentizar.ActivateAbility == true &&!friendly)
        {
            TiempoRalentizado = Ability_Time_Manager.Instance.Slow_ShotgunBullet_Velocity;
        }
        if (Ralentizar.ActivateAbility == false && !friendly)
        {
            TiempoRalentizado = 1f;
        }
        Debug.Log("Gonca11"+TiempoRalentizado);
    }
    void ColorParticle()
    {
        if (bullet_color == 0)
        {
            m_particle[0].Play();
            m_particle[1].Play();
            m_sprite.material.color = Color.yellow;
        }
        else if (bullet_color == 1)
        {
            m_particle[2].Play();
            m_particle[3].Play();
            m_sprite.material.color = Color.cyan;
        }
        else if (bullet_color == 2)
        {
            m_particle[4].Play();
            m_particle[5].Play();
            m_sprite.material.color = Color.magenta;
        }
    }

    // Bullet variable initializer
    public override void AddBulletInfo(int n_color, float n_increase_time, Vector3 n_dir, float n_damage, float n_range, bool n_friend)
    {
        base.AddBulletInfo(n_color, n_increase_time, n_dir, n_damage, n_range, n_friend);
        s_collider = GetComponentInChildren<Collider>();
        if (n_color == 0)
        {
            this.gameObject.tag = "Yellow";
            this.gameObject.layer = 8;
            s_collider.gameObject.tag = "Yellow";
            s_collider.gameObject.layer = 8;
        }
        else if(n_color == 1)
        {
            this.gameObject.tag = "Blue";
            this.gameObject.layer = 9;
            s_collider.gameObject.tag = "Blue";
            s_collider.gameObject.layer = 9;
        }
        else if(n_color == 2)
        {
            this.gameObject.tag = "Pink";
            this.gameObject.layer = 10;
            s_collider.gameObject.tag = "Pink";
            s_collider.gameObject.layer = 10;
        }


        ////- Take out layers sometime
        //// Enemy layers
        //if (!n_friend)
        //    this.gameObject.layer = 16;

        //// Non-color dependent variables
        //bullet_color = n_color;
        
        bullet_life_time = 0.3f;
    }

    // If not collided with anything, destroy
    public override IEnumerator DestroyBullet()
    {
        Debug.Log("DestoryBulletCalled");
        is_waiting = true;
        float progress = 0.0f;
       
        while (is_waiting)
        {
            progress += Time.deltaTime/TiempoRalentizado;
            //Debug.Log("Progress " + progress);

            if (progress >= bullet_life_time)
            {
                
                Debug.Log(bullet_life_time);
                is_waiting = false;
                Debug.Log("OutOfTimeShotgun");
                Color final = new Color(m_sprite.material.color.r, m_sprite.material.color.g, m_sprite.material.color.b, 0);
                StartCoroutine(Lerp_MeshRenderer_Color(m_sprite, fade_time, m_sprite.material.color, final));
            }
            yield return null;
        }
      
    }

    // Preparar la bala de la escopeta
    void InitBullet()
    {
        Transform edge = transform.GetChild(0);

        float dist = Vector3.Distance(transform.position, edge.position);

        Vector3 final = transform.localScale * 2.8f / dist;
        final.y = transform.localScale.y;

        StartCoroutine(LerpSize(increase_time, transform.localScale, final));
    }

    // Incrementar gradualmente el tamaño del área de la escopeta
    IEnumerator LerpSize (float lerpDuration, Vector3 init_scale, Vector3 final_scale)
    {
        Debug.Log("LerpSizeCalled");
        float lerpProgress = 0;
        is_lerping = true;

        while (is_lerping)
        {
            lerpProgress += Time.deltaTime/ TiempoRalentizado; //RalentizarCrecimiento

            transform.localScale = Vector3.Lerp(init_scale, final_scale, lerpProgress / lerpDuration);

            if (lerpProgress >= lerpDuration)
            {
                is_lerping = false;
                StartCoroutine(DestroyBullet());
                Debug.Log("Start Destroy Counter");
            }

            yield return null;
        }
    }

    // Realizar un fundido a transparente
    private IEnumerator Lerp_MeshRenderer_Color(SpriteRenderer target_MeshRender, 
        float lerpDuration, Color startLerp, Color targetLerp)
    {
        Debug.Log("Lerp_MeshRenderer_Color called");
        float lerpStart_Time = Time.time;
        float lerpProgress = 0;
        is_lerping = true;
        while (is_lerping)
        {
            lerpProgress += Time.deltaTime/ TiempoRalentizado;
            if (target_MeshRender != null)
            {
                target_MeshRender.material.color = 
                    Color.Lerp(startLerp, targetLerp, lerpProgress / lerpDuration);
            }
            else
            {
                is_lerping = false;
                Destroy(gameObject);
            }


            if (lerpProgress >= lerpDuration)
            {
                Debug.Log("DestroyBullet");
                is_lerping = false;
                Destroy(gameObject);
            }
            yield return null;
        }
    }
}
