using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControllerPink : MonoBehaviour
{

    public float speed;
    Collider m_collider;
    public float PonerColisionPared;
    public float PonerColisionEnemigo;
    // Use this for initialization
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
        if (col.gameObject.tag == "Pink")
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("PonerCollision", PonerColisionPared);
        }
        else if (col.gameObject.tag == "Player" && col.gameObject.layer == 10)
        {
            Debug.Log("RecibeVida");
            col.gameObject.SendMessage("RecibeVida");
            //col.gameObject.SendMessage("HacerDaño");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player")
        {
            Debug.Log("fdf");
            col.gameObject.SendMessage("HacerDaño");
            Destroy(gameObject);

        }

        else if (col.gameObject.tag == "EnemyPink")
        {
            Debug.Log("Hola");
            m_collider.enabled = !m_collider.enabled;
            Invoke("PonerCollision", PonerColisionEnemigo);
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

