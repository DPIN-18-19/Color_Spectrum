using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Terminal : MonoBehaviour {

    public GameObject mismo;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("Destroy");
        Destroy(mismo);
    }
}
