using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Kamikaze_C : MonoBehaviour {
    public float damage;
    public AudioClip SonidoExplosion;
    AudioSource source;
    Slow_Motion Ralentizar;
    // Use this for initialization
    void Start()
    {
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        source = GetComponent<AudioSource>();
        source.PlayOneShot(SonidoExplosion);

    }

    // Update is called once per frame
    void Update()
    {
        if (Ralentizar.ActivateAbility == true)
        {
            source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
        }
        if (Ralentizar.ActivateAbility == false)
        {
            source.pitch = 1;
        }
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
                    
                    other.GetComponent<EnemyHealth>().GetDamage(damage);
                    
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
                    Destroy(gameObject.GetComponent<BoxCollider>());

                }
                Debug.Log(hit.transform.name);
                // HacerDañoYellow();

            }
        }


    }
}