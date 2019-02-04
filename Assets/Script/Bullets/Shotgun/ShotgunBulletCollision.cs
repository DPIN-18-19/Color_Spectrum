using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunBulletCollision : MonoBehaviour
{
    ShotgunBullet bullet;
    Collider s_collider;
    BulletController Ref_Bullet;

    public float TimeTrasparenteMat;
    private float MaxTimeTrasparenteMat;
    public bool Trasparente;


    public PlayerRenderer MaterialsPlayer;

    // Use this for initialization
    void Start()
    {
        bullet = transform.parent.GetComponent<ShotgunBullet>();
        s_collider = GetComponentInChildren<Collider>();
        Ref_Bullet = transform.parent.gameObject.GetComponent<BulletController>();
        MaterialsPlayer = GameObject.Find("Player_Naomi").GetComponent<PlayerRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
     
        if (Trasparente == true)
        {
            MaterialsPlayer.SameColor();
            // TimeTrasparenteMat -= Time.deltaTime;
            Invoke("ResetMaterial", TimeTrasparenteMat);
        }
        if (Trasparente == false)
        {
            MaterialsPlayer.ResetColor();
        }
    }

    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Shotgun Enter Collided with " + col.transform.gameObject.tag);
        //- Collision with player is not working
        // Same color obstacle collision
        if (col.gameObject.tag == gameObject.tag)
        {
            Debug.Log("Collided with a wall");
        }
        // Collision with player
        else if (col.gameObject.tag == "Player")
        {
            // Enemy bullet
            if (!bullet.friendly)
            {
                // Taking damage to player
                if (col.gameObject.GetComponent<ColorChangingController>().GetColor() != bullet.bullet_color)
                    col.gameObject.SendMessage("GetDamage", bullet.bullet_damage);
                // Restoring player health
                else
                    Trasparente = true;
                //  col.gameObject.SendMessage("RestoreHealth", bullet.bullet_damage);

                //Destroy(gameObject);
            }
            // Player bullet
            //- Bug here
            else
            {
                Debug.Log("collided with player");
                //s_collider.enabled = !s_collider.enabled;
                //Invoke("ReactivateCollision", 1);
            }
        }
        // Ignore enemies of same color
        else if (col.gameObject.tag == bullet.enemy_ignore)
        {
            //s_collider.enabled = !s_collider.enabled;
            //Invoke("ReactivateCollision", bullet.enemy_active_time);
        }
        // Collision with any other object
        else if (col.gameObject.tag.Contains("Enemy"))
        {
            if (bullet.friendly)
            {
                if (col.gameObject.GetComponent<EnemyHealthController>().IsWeak(gameObject.tag, gameObject.layer))
                {
                    col.gameObject.GetComponent<EnemyHealthController>().GetDamage(bullet.bullet_damage);
                }
            }

            //Destroy(gameObject);
        }
        else
        {
            bullet.is_lerping = false;
            StartCoroutine(bullet.DestroyBullet());
        }
    }
    public void ResetMaterial()
    {
        Trasparente = false;
    }
}
