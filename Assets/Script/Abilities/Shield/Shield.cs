﻿

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shield : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    void OnTriggerEnter (Collider col)
    {
        if (col.gameObject.layer == 16)
        {
            Debug.Log("Escudo");
            Destroy(col.transform.parent.gameObject);
        }
    }
}
