﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan1 : MonoBehaviour
{
    //////////////////////////////////////////////////////
    // Singleton architecture
    public static SceneMan1 Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    //////////////////////////////////////////////////////
    // Scene Handling
    
    public enum SceneIndex
    {
        PreLoadScene,
        Loading,
        Main_Menu,
        Pruebas_CinemaMachine,
        UnlockTestingLevel,
        Store,
        LevelSelection
    }
    SceneIndex scene_to_load;
    
    //public void LoadSceneByName(string scene_name)
    //{
    //    // Cannot check scene names when outside of editor
    //    SceneManager.LoadScene(scene_name);
    //}
    
    public void LoadSceneByName(string scene_name)
    {
        scene_to_load = (SceneIndex)System.Enum.Parse(typeof(SceneIndex), scene_name);
        Debug.Log("To Load " + (int)scene_to_load + " " + scene_to_load);
        SceneManager.LoadScene("Loading");
    }
    
    void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public int GetLoadScene()
    {
        return (int)scene_to_load;
    }
}
