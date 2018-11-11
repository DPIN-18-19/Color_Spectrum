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

    //- Consider making just one variable
    public float wall_active_time = 0.2f;       // Time to get collision back after wall
    public float enemy_active_time = 0.2f;      // Time to get collision back after enemy

    string enemy_ignore;            // Enemy color to ignore collision with

    public bool friendly;           // Who shot the bullet? True: Player, False: Enemy

    Collider m_collider;            // Bullet collider

    //////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Start()
    {
        m_collider = GetComponent<Collider>();
        StartCoroutine(DestroyBullet());
    }

    // Bullet variable initializer
    public void AddBulletInfo(int n_color, float n_speed, float n_damage, float n_range, bool n_friend)
    {
        // Color dependent variables
        if (n_color == 0)
        {
            this.gameObject.tag = "Yellow";
            enemy_ignore = "EnemyYellow";
            this.gameObject.layer = 18;             //- Take out layers
        }
        else if (n_color == 1)
        {
            this.gameObject.tag = "Blue";
            enemy_ignore = "EnemyBlue";
            this.gameObject.layer = 20;             //- Take out layers
        }
        else if (n_color == 2)
        { 
            this.gameObject.tag = "Pink";
            enemy_ignore = "EnemyPink";
            this.gameObject.layer = 19;             //- Take out layers
        }

        //- Take out layers sometime
        // Enemy layers
        if (!n_friend)
            this.gameObject.layer = 16;

        // Non-color dependent variables
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
        // Move bullet
        transform.Translate(Vector3.forward * bullet_speed * Time.deltaTime);
    }

    // If not collided with anything, destroy
    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(bullet_life_time);
        Destroy(gameObject);
    }

    
    private void OnCollisionEnter(Collision col)
    {
        //- Collision with player is not working
        if (col.gameObject.tag == "Player")
        {
            if (friendly)
            {
                Debug.Log("Collided with player");
                m_collider.enabled = !m_collider.enabled;
                Invoke("ReactivateCollision", 1);
            }
        }
    }

    void OnCollisionStay(Collision col)
    {
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
                if(col.gameObject.GetComponent<ColorChangingController>().GetColor() != bullet_color)
                    col.gameObject.SendMessage("GetDamage", bullet_damage);
                // Restoring player health
                else
                    col.gameObject.SendMessage("RestoreHealth", bullet_damage);


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
