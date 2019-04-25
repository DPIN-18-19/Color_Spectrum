using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnTrigger : MonoBehaviour
{
    public AreaCondition area;
    SpawnPoint spawner;
    
	// Use this for initialization
	void Start ()
    {
        spawner = GetComponent<SpawnPoint>();
        area.EnterArea += ActivateSpawn;
	}

    void ActivateSpawn()
    {
        spawner.ActivateSpawn();
    }
}
