using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_kamikaze_M : MonoBehaviour {
    public float damage;
    public AudioClip SonidoExplosion;
    AudioSource source;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        source.PlayOneShot(SonidoExplosion);
    }

    // Update is called once per frame
    void Update()
    {
        // DamageTime -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("FAROFrente");
        if (other.gameObject.tag == "EnemyYellow" || other.gameObject.tag == "EnemyBlue")
        {
           
            Debug.Log("Enemy");
            RaycastHit hit;
            Vector3 dir = (other.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, dir, out hit, 5f))

            //  if (DamageTime <= 0)
            {
                if (hit.transform.gameObject.tag != "Yellow" && hit.transform.gameObject.tag != "Pink")
                {
                   
                    other.GetComponent<EnemyHealthController>().GetDamage(damage);
                   
                }
                Debug.Log(hit.transform.name);
                // HacerDañoYellow();

            }
        }
        if (other.gameObject.tag == "Player")
        {
            Debug.Log("DetectoPlayer");
            RaycastHit hit;
            Vector3 dir = (other.transform.position - transform.position).normalized;
           

            if (Physics.Raycast(transform.position, dir, out hit, 5f))

            //  if (DamageTime <= 0)
            {
                if (hit.transform.gameObject.layer != 10)
                {
                    other.GetComponent<HealthController>().GetDamage(damage);
                    Destroy(hit.collider.GetComponent<BoxCollider>());

                }
                Debug.Log(hit.transform.name);
                // HacerDañoYellow();

            }
        }


    }
}