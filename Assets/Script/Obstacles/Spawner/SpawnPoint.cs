﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    //////////////////////////////////////////////////////////
    // Variables de spawnees
    [Header("Informacion de elementos para spawn")]
    public List<Transform> spawnee_l;          // Enemigos posibles para spawnear
    public List<Transform> spawn_patrols;      // Patrullas posibles
    public List<Transform> spawn_homes;        // Origen de enemigos posibles

    [Tooltip("Insertar valor que ocupan enemigos en 'spawnee_l'. Para finalizar oleada, insertar '-1'.")]
    public List<int> spawnee_order;     // Orden en el que spawnearan los enemigos
    int spawnee_it;                     // Iterador de orden de spawnees

    //////////////////////////////////////////////////////////
    // Variables generales
    [Header("Datos generales de spawn")]
    public bool active;                 // El spawn esta activado
    List<Spawner> spawner_l;            // Posiciones de spawn
    int spawner_it;                     // Iterador de posiciones de spawn

    float next_spawn_c;                 // Contador de proximo spawn
    public float next_spawn_dur;        // Duracion de proximo spawn

    //////////////////////////////////////////////////////////
    // Variables de comportamiento de oleadas
    public enum WaveType
    {
        Time,                           // Comenzar tras un tiempo X
        KillAll,                        // Comenzar al terminar con todos en la oleada
        KillSome,                       // Comenzar al terminar con parte de oleada
        TimeForward                     // Comenzar tras tiempo X. Adelantar inicio si se ha terminado con todos de la oleada
    }
    [Header("Datos de oleadas")]
    public WaveType start_wave;         // Forma de inicio de oleada

    public float next_wave_dur;         // Duracion de proxima oleada

    List<KillCondition> wave_l;         // Lista de oleadas
    int wave_it = 0;                    // Iterador de oleadas
    bool is_wave;                       // Comprobador de si se esta realizando una oleada
    public int survivour_num;                  // Enemigos dentro de una oleada
    
    public bool can_reset;             // Resetear spawn al finalizar

    //////////////////////////////////////////////////////////

    void Start ()
    {
        spawner_l = new List<Spawner>();
        spawner_l.AddRange(transform.GetComponentsInChildren<Spawner>());
        spawner_it = 0;
        
        if (start_wave != WaveType.Time)
        {
            wave_l = new List<KillCondition>();
        }

        // Quitar esto. Uso para testing
        is_wave = true;
	}


	// Update is called once per frame
	void Update ()
    {
        if (active)
            DoSpawning();
	}
    
    public void ActivateSpawn()
    {
        active = true;
        is_wave = true;
    }

    void DoSpawning()
    {
        next_spawn_c -= Time.deltaTime;
        Debug.Log("Spawn is active");

        // Comprobar si hay activada una oleada y si empieza el siguiente spawn
        if(is_wave && next_spawn_c < 0)
        {
            // Comprobar si proximo spawner esta desocupado
            if(spawner_l[spawner_it].CheckIfSpawning())
            {
                // Comprobar si fin de oleada
                if(spawnee_order[spawnee_it] == -1)
                {
                    PrepareNextWave();
                    ++spawnee_it;
                    return;
                }

                // Hacer spawn con datos de enemigo
                int data = Random.Range(0, spawn_patrols.Count);
                spawner_l[spawner_it].StartSpawning(spawnee_l[spawnee_order[spawnee_it]], spawn_patrols[data], spawn_homes[data]);

                // Iterar siguiente spawn
                spawner_it = (int)Mathf.Repeat(++spawner_it, spawner_l.Count);
                next_spawn_c = next_spawn_dur;
                ++spawnee_it;

                // Comprobar si fin de spawner
                if (spawnee_it >= spawnee_order.Count)
                    FinishSpawn();
            }
        }
    }

    // Realizar operaciones al terminar datos de spawn
    void FinishSpawn()
    {
        // Reiniciar spawn
        if(can_reset)
        {
            spawnee_it = 0;
            next_spawn_c = next_wave_dur;
        }
        // Desactivarlo
        else
        {
            active = false;
        }
    }

    // Realizar preparativos para siguiente oleada
    void PrepareNextWave()
    {
        is_wave = false;

        switch(start_wave)
        {
            case WaveType.Time:
                next_spawn_c = next_wave_dur;
                break;
            case WaveType.KillAll:
                wave_l[wave_it].SwitchKill();
                wave_l[wave_it].KillWave += StartWaveByKill;
                break;
            case WaveType.KillSome:
                wave_l[wave_it].SwitchKill();
                wave_l[wave_it].SurvivourAmount(survivour_num);
                wave_l[wave_it].KillWave += StartWaveByKill;
                break;
            case WaveType.TimeForward:
                next_spawn_c = next_wave_dur;
                wave_l[wave_it].SwitchKill();
                wave_l[wave_it].KillWave += StartWaveByKill;
                break;
        }
    }

    // Comenzar proxima oleada tras tiempo
    void StartWaveByTime()
    {
        if(next_spawn_c < 0)
        {
            is_wave = true;
            ++wave_it;
        }
    }

    // Comenzar proxima oleada tras muertes
    void StartWaveByKill()
    {
        is_wave = true;
        wave_l[wave_it].KillWave -= StartWaveByKill;  // Evitar que se vuelva a llamar la función
        ++wave_it;

        // Prepare next kill condition
        KillCondition n_kill = new KillCondition();
        wave_l.Add(n_kill);
        wave_l[wave_it].SwitchKill();
    }

    // Incluir enemigo en condicion de muertes
    public void IncludeEnemy(Transform enemy)
    {
        if(start_wave != WaveType.Time)
            wave_l[wave_it].kill_enemies.Add(enemy.gameObject);
    }
}
