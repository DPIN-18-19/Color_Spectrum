using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoSomething : MonoBehaviour
{

	// Use this for initialization
	void Start ()
    {
        LevelCondition.Instance.KillWave += DoMyThing;
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    void DoMyThing()
    {
        GetComponent<Animator>().SetTrigger("OpenDoor");
    }
}
