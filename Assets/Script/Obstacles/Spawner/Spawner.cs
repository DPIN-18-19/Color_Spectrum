using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    bool is_spawning;

    float to_spawn_c;
    float to_spawn_dur;

    float refresh_c;
    float refresh_dur;

    bool is_preparing;
    bool is_refreshing;

    Transform enemy;
    Transform patrol;
    Transform home;
	
    public bool CheckIfSpawning()
    {
        return is_spawning;
    }

	// Update is called once per frame
	void Update ()
    {
        if (is_preparing)
            IsPreparing();

        if (is_refreshing)
            IsRefreshing();
	}

    public void StartSpawning(Transform n_enemy, Transform n_patrol, Transform n_home)
    {
        is_spawning = true;
        is_preparing = true;
        to_spawn_c = to_spawn_dur;

        enemy = n_enemy;
        patrol = n_patrol;
        home = n_home;
    }

    void IsPreparing()
    {
        to_spawn_c -= Time.deltaTime;

        if(to_spawn_c < 0)
        {
            is_preparing = false;
            SpawnEnemy();
            is_refreshing = true;
        }
    }

    void IsRefreshing()
    {
        refresh_c -= Time.deltaTime;

        if (refresh_c < 0)
        {
            is_spawning = false;
            is_refreshing = false;

        }
    }

    void SpawnEnemy()
    {
        Transform s_enemy = Instantiate(enemy, transform);
        Transform s_patrol = Instantiate(patrol);
        s_patrol.SetParent(enemy);
        Transform s_home = enemy.Find("EnemyHome");
        s_home = home;
    }


}
