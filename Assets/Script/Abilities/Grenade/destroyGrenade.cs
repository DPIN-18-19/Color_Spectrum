using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroyGrenade : MonoBehaviour
{

    public ParticleSystem ExplosionEffect;
    public float destroyTime;

    public ColorChangingController cambioColor;
    public AudioClip ExplosionFx;
    public float wall_active_time = 0.2f;
    public float enemy_active_time = 0.2f;
    Collider m_collider;
    // Use this for initialization
    void Start()
    {
        m_collider = GetComponent<Collider>();
        cambioColor = GameObject.FindGameObjectWithTag("Player").GetComponent<ColorChangingController>();
        // DamageTime = destroyTime - 0.1f;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //DamageTime -= Time.deltaTime;
        if (cambioColor.GetColor() == 0)
        {
            Invoke("DestroyGranadaYellow", destroyTime);
            // Invoke("HacerDañoYellow", DamageTime);
        }
        if (cambioColor.GetColor() == 1)
        {
            Invoke("DestroyGranadaBlue", destroyTime);
            // Invoke("HacerDaño", DamageTime);
        }
        if (cambioColor.GetColor() == 2)
        {
            Invoke("DestroyGranadaPink", destroyTime);
            // Invoke("HacerDaño", DamageTime);
        }
        //Debug.Log(Damage);


    }
    void DestroyGranadaYellow()
    {
        AudioSource.PlayClipAtPoint(ExplosionFx, transform.position);
        Instantiate(ExplosionEffect.gameObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    void DestroyGranadaBlue()
    {
        //DamageBlue = true;
        AudioSource.PlayClipAtPoint(ExplosionFx, transform.position);
        Instantiate(ExplosionEffect.gameObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    void DestroyGranadaPink()
    {
        //DamagePink = true;
        AudioSource.PlayClipAtPoint(ExplosionFx, transform.position);
        Instantiate(ExplosionEffect.gameObject, transform.position, Quaternion.identity);
        Destroy(this.gameObject);
    }
    private void OnCollisionEnter(Collision col)
    {
        //if (col.gameObject.tag == gameObject.tag)
        //{
        //  //  Debug.Log("Collided with a wall");
        //    m_collider.enabled = !m_collider.enabled;
        //    Invoke("ReactivateCollision", wall_active_time);
        //}


        //else if (cambioColor.GetColor() == 0 && col.gameObject.tag == "ParedNoCambioYellow")
        //{
        //  //  Debug.Log("Collided with a wall");
        //    m_collider.enabled = !m_collider.enabled;
        //    Invoke("ReactivateCollision", wall_active_time);
        //}
        //else if (cambioColor.GetColor() == 1 && col.gameObject.tag == "ParedNoCambioBlue")
        //{
        //  //  Debug.Log("Collided with a wall");
        //    m_collider.enabled = !m_collider.enabled;
        //    Invoke("ReactivateCollision", wall_active_time);
        //}
        //else if (cambioColor.GetColor() == 2 && col.gameObject.tag == "ParedNoCambioPink")
        //{
        //  //  Debug.Log("Collided with a wall");
        //    m_collider.enabled = !m_collider.enabled;
        //    Invoke("ReactivateCollision", wall_active_time);
        //}
        


    }
    void ReactivateCollision()
    {
        m_collider.enabled = !m_collider.enabled;
    }
}
