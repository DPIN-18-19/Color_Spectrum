using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour {

    private Rigidbody rb;
    public float dashspeed;
    private float dashTime;
    public float startDashTime;
    private int direction;


	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        dashTime = startDashTime;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
