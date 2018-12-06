using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_gre : MonoBehaviour {

    public GameObject grenade_prefab;
    public Transform FirePos;
    public float ThrowForce = 10f;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if(Input.GetButtonDown("Habilidad1"))
        {
            Debug.Log("Hoala");
            GameObject gren = Instantiate(grenade_prefab, FirePos.position, FirePos.rotation) as GameObject;
            gren.GetComponent<Rigidbody>().AddForce(FirePos.right * ThrowForce,ForceMode.Impulse);
        }
		
	}
}
