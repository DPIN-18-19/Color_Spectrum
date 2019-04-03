using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool active;        // El spawner esta activado

    List<Transform> spawnees;
    List<int> spawn_order;

    float next_spawn_c;
    float next_spawn_dur;

    List<Transform> spawn_patrols;
    List<Transform> spawn_homes;




	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    
}
