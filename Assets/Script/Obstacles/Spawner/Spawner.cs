using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Spawner : MonoBehaviour
{
    bool is_spawning;               // 

    float to_spawn_c;               // Contador estado "Preparando"
    public float to_spawn_dur;      // Duracion estado "Preparando"
    public float spawn_pause;       // Pausa durante patrulla

    float refresh_c;                // Contador estado "Terminando"
    public float refresh_dur;       // Duracion estado "Terminando"

    bool is_preparing;              // Estado "Preparando"
    bool is_refreshing;             // Estado "Terminando"

    Transform enemy;                // Enemigo a spawnear
    Transform patrol;               // Patrulla asignada
    Transform home;                 // Origen de enemigo asignada
    Transform particle;             // Particula asignada
    Transform target;               // Target asignado

    public bool CheckIfSpawning()
    {
        return is_spawning;
    }

    // Update is called once per frame
    void Update()
    {
        // Estado "Preparando"
        if (is_preparing)
            IsPreparing();

        // Estado "Terminando"
        if (is_refreshing)
            IsRefreshing();
    }

    // Preparar spawner
    public void StartSpawning(Transform n_enemy, Transform n_patrol, Transform n_home, Transform n_particle, Transform n_target)
    {
        // Reiniciar variables de spawner
        is_spawning = true;
        is_preparing = true;
        to_spawn_c = to_spawn_dur;

        // Recoger datos de proximo spawnee
        enemy = n_enemy;
        patrol = n_patrol;
        home = n_home;
        particle = n_particle;
        target = n_target;

        Instantiate(particle, transform.Find("SpawnEffect").position, transform.rotation);
    }

    // Estado "Preparando"
    void IsPreparing()
    {
        // Realizar cuenta de estado
        to_spawn_c -= Time.deltaTime;

        // Cambiar a estado "Terminando"
        if (to_spawn_c < 0)
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
        Transform s_enemy = Instantiate(enemy, transform.GetChild(0));

        s_enemy.GetComponentInChildren<NavMeshAgent>().Warp(transform.position);

        // Patrol Points
        Transform s_patrol = s_enemy.Find("PatrolPoints");

        // Eliminar puntos de patrulla de prefab
        for (int i = 0; i < s_patrol.childCount; ++i)
        {
            Debug.Log("Destroy child " + s_patrol.GetChild(i).name);
            Destroy(s_patrol.GetChild(i).gameObject);
            s_patrol.GetChild(i).parent = null;
        }
        // Insertar nuevos puntos de patrulla
        for (int i = 0; i < patrol.childCount; ++i)
        {
            Transform patrol_point = Instantiate(patrol.GetChild(i), patrol.transform);
            patrol_point.SetParent(s_patrol);
        }

        // Insertar nuevo punto origen 
        Transform s_home = s_enemy.Find("EnemyHome");
        s_home.position = home.position;

        // Insertar nuevo target
        s_enemy.GetComponentInChildren<EnemyBehaviour>().ResetTarget(target);
        if (Random.Range(0, 4) == 0)
            s_enemy.GetComponentInChildren<EnemyBehaviour>().can_change_target = true;

        // Pausar la patrulla durante un tiempo
        s_enemy.GetComponentInChildren<PatrolController>().PausePatrolBySpawn(spawn_pause);

        Debug.Log("Instantiate Enemy");

        // Incluir enemigo en contador de enemigos por oleadas
        GetComponentInParent<SpawnPoint>().IncludeEnemy(s_enemy);
    }
}