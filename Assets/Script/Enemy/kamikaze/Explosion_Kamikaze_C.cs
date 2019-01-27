using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Kamikaze_C : MonoBehaviour {
    public float damage;
    public AudioClip SonidoExplosion;
    AudioSource source;
    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        source = GetComponent<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        // DamageTime -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("FAROFrente");
        if (other.gameObject.tag == "EnemyPink" || other.gameObject.tag == "EnemyYellow")
        {
            
            Debug.Log("Enemy");
            RaycastHit hit;
            Vector3 dir = (other.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, dir, out hit, 5f))

            //  if (DamageTime <= 0)
            {
                if (hit.transform.gameObject.tag != "Blue" && hit.transform.gameObject.tag != "Yellow")
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
                if (hit.transform.gameObject.layer != 9)
                {
                    
                    other.GetComponent<HealthController>().GetDamage(damage);
                   
                }
                Debug.Log(hit.transform.name);
                // HacerDañoYellow();

            }
        }


    }
}