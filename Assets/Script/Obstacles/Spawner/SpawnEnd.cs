using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnd : MonoBehaviour
{
    // Coger Kill condidion de spawn point
    public Transform spawn_point;
    public bool is_end = false;
    
    // Crear funcion llama al evento
    private void Start()
    {
        spawn_point.GetComponent<SpawnPoint>().all_spawnees.KillWave += SpawnIsFinish;
    }

    //public void ActivateEnd()
    //{
    //    Debug.Log("Activating End");
    //    //spawn_point = GetComponent<SpawnPoint>();
    //}

    void SpawnIsFinish()
    {
        Debug.Log("Spawn is finished");
        spawn_point.GetComponent<SpawnPoint>().active = false;
        is_end = true;
        //EndSpawn();
    }
}
