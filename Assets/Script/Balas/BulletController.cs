using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float bullet_speed;      // Bullet speed(m/s)
    public float bullet_damage;     // Damage bullet makes 
    public float bullet_range;      // Distance a bullet can travel(m)
    public float bullet_life_time;  // Time of bullet(s)

    public float PonerColisionPared = 0.2f;
    public bool friendly;

    Collider m_collider;

    //////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start()
    {
        m_collider = GetComponent<Collider>();
        StartCoroutine(DestroyBullet());
    }

    public void AddBulletInfo(int n_color, float n_speed, float n_damage, float n_range)
    {
        if (n_color == 0)
            this.gameObject.tag = "Yellow";
        else if (n_color == 1)
            this.gameObject.tag = "Blue";
        else if (n_color == 2)
            this.gameObject.tag = "Pink";

        bullet_speed = n_speed;
        bullet_damage = n_damage;
        bullet_range = n_range;
        bullet_life_time = bullet_range / bullet_speed;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * bullet_speed * Time.deltaTime);
        //Destroy(gameObject, 3f);
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "Player")
        {
            Debug.Log("Bala");
            col.gameObject.SendMessage("HacerDaño");
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bullet_life_time);
        Destroy(gameObject);
    }

    //Collider m_collider;
    //public float PonerColisionPared;
    //public float PonerColisionEnemigo;

    // Use this for initialization
    //void Start()
    //{
    //    m_collider = GetComponent<Collider>();
    //}

    // Update is called once per frame
    //void Update()
    //{
    //    transform.Translate(Vector3.forward * speed * Time.deltaTime);
    //    Destroy(gameObject, 3f);
    //}


    private void OnCollisionStay(Collision col)
    {
        if (col.gameObject.tag == gameObject.tag)
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("PonerCollision", PonerColisionPared);
        }
        //else if (col.gameObject.tag == "Player" && col.gameObject.layer == 9)
        //{
        //    Debug.Log("RecibeVida");
        //    col.gameObject.SendMessage("RecibeVida");
        //    //col.gameObject.SendMessage("HacerDaño");
        //    Destroy(gameObject);
        //}
        //else if (col.gameObject.tag == "Player")
        //{
        //    Debug.Log("fdf");
        //    col.gameObject.SendMessage("HacerDaño");
        //    Destroy(gameObject);

        //}

        //else if (col.gameObject.tag == "EnemyBlue")
        //{
        //    Debug.Log("Hola");
        //    m_collider.enabled = !m_collider.enabled;
        //    Invoke("PonerCollision", PonerColisionEnemigo);
        //}
        //else
        //{
        //    Destroy(gameObject);
        //}

    }


    void PonerCollision()
    {
        m_collider.enabled = !m_collider.enabled;
    }
}
