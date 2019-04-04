using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public bool active;                // El spawn esta activado
    List<Spawner> spawner_l;    // Posiciones de spawn
    int spawner_it;

    List<Transform> spawnee_l;
    [Tooltip("Insertar valor que ocupan enemigos en 'spawnee_l'. Para finalizar oleada, insertar '-1'.")]
    public List<int> spawnee_order;
    int spawnee_it;

    float next_spawn_c;
    float next_spawn_dur;

    float next_wave_dur;

    List<Transform> spawn_patrols;
    List<Transform> spawn_homes;

    bool can_reset;             // Resetear spawn al finalizar
    
    public enum NextWaveType
    {
        Time,                           // Comenzar tras un tiempo X
        KillAll,                        // Comenzar al terminar con todos en la oleada
        KillSome,                       // Comenzar al terminar con parte de oleada
        TimeForward                     // Comenzar tras tiempo X. Adelantar inicio si se ha terminado con todos de la oleada
    }
    public NextWaveType start_wave;

    KillCondition cond;
    bool is_wave;

    // Use this for initialization
    void Start ()
    {
        spawner_l.AddRange(transform.GetComponentsInChildren<Spawner>());
        spawner_it = 0;
	}


	// Update is called once per frame
	void Update () {
		
	}
    
    void DoSpawning()
    {
        if(is_wave && next_spawn_c < 0)
        {
            if(spawner_l[spawner_it].CheckIfSpawning())
            {
                if(spawnee_order[spawnee_it] == -1)
                {
                    PrepareNextWave();
                    ++spawnee_it;
                    return;
                }

                int data = Random.Range(0, spawn_patrols.Count);
                spawner_l[spawner_it].StartSpawning(spawnee_l[spawnee_order[spawnee_it]], spawn_patrols[data], spawn_homes[data]);
                spawner_it = (int)Mathf.Repeat(++spawner_it, spawner_l.Count);
                next_spawn_c = next_spawn_dur;
                ++spawnee_it;

                if (spawnee_it >= spawnee_order.Count)
                    FinishSpawn();
            }
        }
    }

    void FinishSpawn()
    {
        if(can_reset)
        {
            spawnee_it = 0;
            next_spawn_c = next_wave_dur;
        }
        else
        {
            active = false;
        }
    }

    void PrepareNextWave()
    {
        is_wave = false;

        switch(start_wave)
        {
            case NextWaveType.Time:
                next_spawn_c = next_wave_dur;
                break;
            case NextWaveType.KillAll:
                cond.KillWave += StartWave;
                break;
            case NextWaveType.KillSome:
                cond.KillWave += StartWave;
                break;
            case NextWaveType.TimeForward:
                next_spawn_c = next_wave_dur;
                cond.KillWave += StartWave;
                break;
        }
    }

    void StartWave()
    {
        is_wave = true;
        cond.KillWave -= StartWave;
    }

}
