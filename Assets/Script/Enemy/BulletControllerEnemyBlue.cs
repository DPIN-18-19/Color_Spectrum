using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerEnemyBlue : MonoBehaviour {
    public float speed;
    Collider m_collider;
    public float PonerColisionPared;
    public float PonerColisionEnemigo;
    
    void Start()
    {
        m_collider = GetComponent<Collider>();
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
        Destroy(gameObject, 3f);
        
    }
    private void OnCollisionEnter(Collision col)
    {
      
        if (col.gameObject.tag == "Blue")
        {
            
            m_collider.enabled = !m_collider.enabled;
            if (m_collider.enabled == false)
                Invoke("PonerCollision", PonerColisionPared);
        }
        
        else if (col.gameObject.tag == "Player" && col.gameObject.layer == 9)
        {
            
            col.gameObject.SendMessage("RecibeVida");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("HacerDaño");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "EnemyBlue")
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("PonerCollision", PonerColisionEnemigo);
        }
        else if (col.gameObject.tag == "EnemyYellow")
        {
            Debug.Log("FuegoAmigo");
            Destroy(gameObject);
        }

        else
        {
            Destroy(gameObject);
        }

    }


    void PonerCollision()
    {
      
        m_collider.enabled = !m_collider.enabled;
    }
}

