using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explosion_Kamikaze_Y : MonoBehaviour {

   
    public float damage;
   
    // Use this for initialization
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        // DamageTime -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
        // Debug.Log("FAROFrente");
        if (other.gameObject.tag == "EnemyPink" || other.gameObject.tag == "EnemyBlue")
        {
            Debug.Log("Enemy");
            RaycastHit hit;
            Vector3 dir = (other.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, dir, out hit, 5f))

            //  if (DamageTime <= 0)
            {
                if (hit.transform.gameObject.tag != "Blue" && hit.transform.gameObject.tag != "Pink")
                {
                    Debug.Log("DañoAmariillo");
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
                if (hit.transform.gameObject.tag != "Blue" && hit.transform.gameObject.tag != "Pink")
                {
                    Debug.Log("DañoAmariillo");
                    other.GetComponent<HealthController>().GetDamage(damage);
                }
                Debug.Log(hit.transform.name);
                // HacerDañoYellow();

            }
        }


    }
}