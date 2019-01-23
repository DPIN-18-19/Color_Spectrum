using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class damage_grenade_blue : MonoBehaviour {

    // Use this for initialization
    public ColorChangingController cambioColor;
    public float damage;
    public float DamageTime;
    // Use this for initialization
    void Start()
    {
        cambioColor = GameObject.FindGameObjectWithTag("Player").GetComponent<ColorChangingController>();
    }

    // Update is called once per frame
    void Update()
    {
        // DamageTime -= Time.deltaTime;
    }
    private void OnTriggerEnter(Collider other)
    {
       // Debug.Log("FAROFrente");
        if (other.gameObject.tag == "EnemyYellow" || other.gameObject.tag == "EnemyPink")
        {
            //  if (DamageTime <= 0)
            RaycastHit hit;
            Vector3 dir = (other.transform.position - transform.position).normalized;
            if (Physics.Raycast(transform.position, dir, out hit, 6.05f))

            //  if (DamageTime <= 0)
            {
                if (hit.transform.gameObject.tag != "Yellow" && hit.transform.gameObject.tag != "Pink")
                {
                    Debug.Log("DañoBlue");
                    other.GetComponent<EnemyHealthController>().GetDamage(damage);
                }
                Debug.Log(hit.transform.name);
                // HacerDañoYellow();

            }
        }
        // if (cambioColor.GetColor() == 1 && other.gameObject.tag == "EnemyYellow" || other.gameObject.tag == "EnemyPink")
        //  {
        //      Debug.Log("TrueDaño");
        //   if (DamageTime <= 0)
        //       {
        //         other.GetComponent<EnemyHealthController>().GetDamage(damage);
        // HacerDañoBlue();
        //          Debug.Log("DañoAzul");
        //      }
        //    }
        //   if (cambioColor.GetColor() == 2 && other.gameObject.tag == "EnemyYellow" || other.gameObject.tag == "EnemyBlue")
        //     {
        // if (DamageTime <= 0)
        //     {
        //         other.GetComponent<EnemyHealthController>().GetDamage(damage);
        //        // HacerDañoPink();
        //         Debug.Log("DañoRosa");
        //     }
        //    }

    }
}
