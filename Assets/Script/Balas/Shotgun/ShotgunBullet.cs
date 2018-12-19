using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBullet : BulletController
{
    ////////////////////////////////////////////////////////////////////////
    // Shotgun Specific variables

    float increase_time = 0.4f;
    float fade_time = 0.15f;
    public bool is_lerping;

    private Collider s_collider;            // Bullet collider
    SpriteRenderer m_sprite;
    ParticleSystem []m_particle;

   


    // Use this for initialization
    void Start ()
    {
        
        m_sprite = GetComponentInChildren<SpriteRenderer>();
        s_collider = GetComponentInChildren<Collider>();
        m_particle = GetComponentsInChildren<ParticleSystem>();
        //m_particle.material.color = Color.yellow;
        //Debug.Log(s_collider.name);

        InitBullet();
        ColorParticle();
    }

    private void Update()
    {
       
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
            this.gameObject.layer = 11;
            s_collider.gameObject.tag = "Yellow";
            s_collider.gameObject.layer = 11;
        }
        else if(n_color == 1)
        {
            this.gameObject.tag = "Blue";
            this.gameObject.layer = 12;
            s_collider.gameObject.tag = "Blue";
            s_collider.gameObject.layer = 12;
        }
        else if(n_color == 2)
        {
            this.gameObject.tag = "Pink";
            this.gameObject.layer = 13;
            s_collider.gameObject.tag = "Pink";
            s_collider.gameObject.layer = 13;
        }
        

        ////- Take out layers sometime
        //// Enemy layers
        //if (!n_friend)
        //    this.gameObject.layer = 16;

        //// Non-color dependent variables
        //bullet_color = n_color;
        increase_time = 0.2f;
        bullet_life_time = 0.05f;
    }

    // If not collided with anything, destroy
    public override IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bullet_life_time);
        Debug.Log("Out of time shotgun");
        Color final = new Color(m_sprite.material.color.r, m_sprite.material.color.g, m_sprite.material.color.b, 0);
        StartCoroutine(Lerp_MeshRenderer_Color(m_sprite, fade_time, m_sprite.material.color, final));
        //Destroy(gameObject);
    }

    // Preparar la bala de la escopeta
    void InitBullet()
    {
        Transform edge = transform.GetChild(0);

        float dist = Vector3.Distance(transform.position, edge.position);

        Vector3 final = transform.localScale * bullet_range / dist;
        final.y = transform.localScale.y;

        StartCoroutine(LerpSize(increase_time, transform.localScale, final));
    }

    // Incrementar gradualmente el tamaño del área de la escopeta
    IEnumerator LerpSize(float lerpDuration, Vector3 init_scale, Vector3 final_scale)
    {
        float lerpStart_Time = Time.time;
        float lerpProgress;
        is_lerping = true;

        while (is_lerping)
        {
            yield return new WaitForEndOfFrame();
            lerpProgress = Time.time - lerpStart_Time;

            transform.localScale = Vector3.Lerp(init_scale, final_scale, lerpProgress / lerpDuration);

            if (lerpProgress >= lerpDuration)
            {
                is_lerping = false;
                StartCoroutine(DestroyBullet());
            }
        }
        yield break;
    }

    // Realizar un fundido a transparente
    private IEnumerator Lerp_MeshRenderer_Color(SpriteRenderer target_MeshRender, 
        float lerpDuration, Color startLerp, Color targetLerp)
    {
        float lerpStart_Time = Time.time;
        float lerpProgress;
        is_lerping = true;
        while (is_lerping)
        {
            yield return new WaitForEndOfFrame();
            lerpProgress = Time.time - lerpStart_Time;
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
                is_lerping = false;
                Destroy(gameObject);
            }
        }
        yield break;
    }
}
