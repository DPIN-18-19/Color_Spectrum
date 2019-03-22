using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan1 : MonoBehaviour
{
    //////////////////////////////////////////////////////
    // Singleton architecture
    public static SceneMan1 Instance { get; private set; }
    float TimRestart = 1.5f;
    public bool isdead = false;
    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if (isdead)
        {
            TimRestart -= Time.deltaTime;
        }
        if (TimRestart <= 0)
        {
            TimRestart = 2;
            isdead = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);

        }
    }

    //////////////////////////////////////////////////////
    // Scene Handling

    public enum SceneIndex
    {
        PreLoadScene,
        Loading,
        Main_Menu,
        PreparationMenu,
        LevelSelection,
        Customizacion,
        Store,
        Tuto_Customizacion,
        Tutorial,
        Nivel_1,
        Nivel_2,
        TestSceneJaime
        //UnlockTestingLevel,
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
        SceneManager.LoadScene("Loading");
        //SceneManager.LoadScene(scene_name);
    }
    
    public  void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public int GetLoadScene()
    {
        return (int)scene_to_load;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}
