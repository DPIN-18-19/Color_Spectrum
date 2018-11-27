using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateOnTime : MonoBehaviour
{
    public float angle_velocity = 1;


	// Use this for initialization
	void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        RotateMyself();
    }

    void RotateMyself()
    {
        transform.RotateAround(transform.position, Vector3.up, angle_velocity * Time.deltaTime);
    }
}
