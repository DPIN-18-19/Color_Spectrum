using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    int bullet_color;
    public float bullet_speed;      // Bullet speed(m/s)
    public float bullet_damage;     // Damage bullet makes 
    public float bullet_range;      // Distance a bullet can travel(m)
    public float bullet_life_time;  // Time of bullet(s)

    public float wall_active_time = 0.2f;
    public float enemy_active_time = 0.2f;

    string enemy_ignore;

    public bool friendly;

    Collider m_collider;

    //////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start()
    {
        m_collider = GetComponent<Collider>();
        StartCoroutine(DestroyBullet());
    }

    public void AddBulletInfo(int n_color, float n_speed, float n_damage, float n_range, bool n_friend)
    {
        if (n_color == 0)
        {
            this.gameObject.tag = "Yellow";
            enemy_ignore = "EnemyYellow";
        }
        else if (n_color == 1)
        {
            this.gameObject.tag = "Blue";
            enemy_ignore = "EnemyBlue";
        }
        else if (n_color == 2)
        { 
            this.gameObject.tag = "Pink";
            enemy_ignore = "EnemyPink";
        }

        bullet_color = n_color;
        bullet_speed = n_speed;
        bullet_damage = n_damage;
        bullet_range = n_range;
        bullet_life_time = bullet_range / bullet_speed;

        friendly = n_friend;
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
            Invoke("ReactivateCollision", wall_active_time);
        }
        //else if (col.gameObject.tag == "Player" && col.gameObject.layer == 9)
        //{
        //    Debug.Log("RecibeVida");
        //    col.gameObject.SendMessage("RecibeVida");
        //    //col.gameObject.SendMessage("HacerDaño");
        //    Destroy(gameObject);
        //}
        else if (col.gameObject.tag == "Player")
        {
            if (!friendly)
            {
                // Taking damage to player
                if(col.gameObject.GetComponent<ColorChangingController>().GetColor() != bullet_color)
                    col.gameObject.SendMessage("GetDamage", bullet_damage);
                // Restoring player health
                else
                    col.gameObject.SendMessage("RestoreHealth", bullet_damage);


                Destroy(gameObject);
            }
        }
        // Ignore enemies of same color
        else if (col.gameObject.tag == enemy_ignore)
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("ReactivateCollision", enemy_active_time);
        }
        // Collision with any other object
        else
        {
            Destroy(gameObject);
        }

    }


    void ReactivateCollision()
    {
        m_collider.enabled = !m_collider.enabled;
    }
}
