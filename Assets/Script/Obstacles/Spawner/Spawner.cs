using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    bool is_spawning;               // 

    float to_spawn_c;               // Contador estado "Preparando"
    public float to_spawn_dur;      // Duracion estado "Preparando"

    float refresh_c;                // Contador estado "Terminando"
    public float refresh_dur;       // Duracion estado "Terminando"

    bool is_preparing;              // Estado "Preparando"
    bool is_refreshing;             // Estado "Terminando"

    Transform enemy;                // Enemigo a spawnear
    Transform patrol;               // Patrulla asignada
    Transform home;                 // Origen de enemigo asignada
	
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

    // Estado "Preparando"
    void IsPreparing()
    {
        // Realizar cuenta de estado
        to_spawn_c -= Time.deltaTime;

        // Cambiar a estado "Terminando"
        if(to_spawn_c < 0)
        {
            is_preparing = false;
            SpawnEnemy();           // Realizar spawn
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

        s_enemy.GetComponentInChildren<NavMeshAgent>().Warp(transform.position);

        // Patrol Points
        Transform s_patrol = s_enemy.Find("PatrolPoints");

        // Clear prefab patrol points
        for (int i = 0; i < s_patrol.childCount; ++i)
        {
            Debug.Log("Destroy child " + s_patrol.GetChild(i).name);
            Destroy(s_patrol.GetChild(i).gameObject);
        }
        // Insert new patrol points
        for (int i = 0; i < patrol.childCount; ++i)
        {
            Transform patrol_point = Instantiate(patrol.GetChild(i), patrol.transform);
            patrol_point.SetParent(s_patrol);
        }

        // Home 
        Transform s_home = s_enemy.Find("EnemyHome");
        s_home.position = home.position;

        Debug.Log("Instantiate Enemy");

        GetComponentInParent<SpawnPoint>().IncludeEnemy(s_enemy);
    }
}
