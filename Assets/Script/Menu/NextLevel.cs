﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevel : MonoBehaviour
{

    //public SceneMan scene_man;
    public string next_name;

    //private GameManagerController gm;

    void Start()
    {
        //gm = GameObject.FindGameObjectWithTag("GM").GetComponent<GameManagerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<PlayerController>())
        {
           
            //Destroy(gm.gameObject);
            //scene_man.NextLevel(next_name);
        }
    }
}
