using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trasparente : MonoBehaviour {
    private BulletController bullet;
	// Use this for initialization
	void Start () {
        bullet = GetComponentInParent<BulletController>();

    }
  

    // Update is called once per frame
    void Update () {
		
	}
    private void OnTriggerEnter(Collider other)
    {
        if( other.gameObject.tag == "Player")
        {
            bullet.Trasparente = true;
        }
    }
}
