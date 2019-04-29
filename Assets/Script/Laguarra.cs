using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laguarra : MonoBehaviour {

    public GameObject Enemigos_Arena_4;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        
		
	}

    void OnTriggerEnter(Collider other)
    {
        Enemigos_Arena_4.SetActive(true);
    }
}
