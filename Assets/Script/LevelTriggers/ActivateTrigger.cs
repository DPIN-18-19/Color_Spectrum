using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateTrigger : MonoBehaviour
{
    public CountArena level_trigger;
    public GameObject trigger_to_active;

	// Use this for initialization
	void Start ()
    {
        level_trigger.EndArea += TriggerActivate;
	}

    void TriggerActivate()
    {
        trigger_to_active.SetActive(true);
    }
}
