using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPos : MonoBehaviour
{
    bool is_spawning;

    float to_spawn_c;
    float to_spawn_dur;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void SpawnEnemy(Transform n_enemy, Transform n_patrol, Transform n_home)
    {
        Transform enemy = Instantiate(n_enemy, transform);
        Transform patrols = Instantiate(n_patrol);
        patrols.SetParent(enemy);
        Transform home = enemy.Find("EnemyHome");
        home = n_home;
    }
}
