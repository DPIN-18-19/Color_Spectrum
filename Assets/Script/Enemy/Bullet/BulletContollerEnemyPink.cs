using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletContollerEnemyPink : MonoBehaviour {
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
        if (col.gameObject.tag == "Pink")
        {
            Debug.Log("Hola");
            m_collider.enabled = !m_collider.enabled;
            if (m_collider.enabled == false)
                Invoke("PonerCollision", PonerColisionPared);
        }
        else if (col.gameObject.tag == "Player" && col.gameObject.layer == 10)
        {
            Debug.Log("PasoPorAqui");
            col.gameObject.SendMessage("RecibeVida");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "Player")
        {
            col.gameObject.SendMessage("HacerDaño");
            Destroy(gameObject);
        }
        else if (col.gameObject.tag == "EnemyPink")
        {
            m_collider.enabled = !m_collider.enabled;
            Invoke("PonerCollision", PonerColisionEnemigo);
        }
        else if ((col.gameObject.tag == "EnemyYellow" || col.gameObject.tag == "EnemyBlue")){
            Destroy(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

    }


    void PonerCollision()
    {
        Debug.Log("PongoLacollisio");
        m_collider.enabled = !m_collider.enabled;
    }
}

