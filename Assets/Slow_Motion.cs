using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Motion : MonoBehaviour {

    public bool ActivateAbility;
     
   
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKey(KeyCode.Space))
        {
            ActivateAbility = true;
        }
        if (Input.GetKeyUp(KeyCode.Space))
        {
            ActivateAbility = false;
        }
    }
}
