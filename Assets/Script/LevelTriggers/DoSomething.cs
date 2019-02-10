using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour
{
    //
    public KillCondition lvl_trigger;

	// Use this for initialization
	void Start ()
    {
        lvl_trigger.KillWave += DoMyThing;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void DoMyThing()
    {
        //GetComponent<Animator>().SetTrigger("OpenDoor");
        Debug.Log("Hello! I'm " + transform.name);
        Debug.Log("My Trigger is " + lvl_trigger.transform.name);
    }
}
