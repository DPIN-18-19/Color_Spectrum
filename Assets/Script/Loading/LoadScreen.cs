using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    AsyncOperation operation;

    void OnEnable()
    {
        Debug.Log("Loading");
        //SceneManager.sceneLoaded += LoadScreenLoaded;
        operation = SceneManager.LoadSceneAsync(SceneMan1.Instance.GetLoadScene(), LoadSceneMode.Additive);
        SceneManager.sceneLoaded += FinishLoading;
    }
    
    void LoadAsync()
    {
        operation = SceneManager.LoadSceneAsync(SceneMan1.Instance.GetLoadScene(), LoadSceneMode.Additive);
        SceneManager.sceneLoaded += FinishLoading;
    }

    void FinishLoading(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == SceneMan1.Instance.GetLoadScene())
        {
            SceneManager.sceneLoaded -= FinishLoading;
            //SceneManager.sceneLoaded -= LoadScreenLoaded;
            Destroy(gameObject);
        }
    }

    //void LoadScreenLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.buildIndex == 1)
    //    {
    //        Debug.Log("Loading screen loaded");
    //        LoadAsync();
    //    }
    //}
}