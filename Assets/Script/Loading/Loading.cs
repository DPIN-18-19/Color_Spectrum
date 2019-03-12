using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation operation;

    void OnEnable()
    {
        Debug.Log("Loading " + SceneMan1.Instance.GetLoadScene());
        //SceneManager.sceneLoaded += LoadScreenLoaded;
        operation = SceneManager.LoadSceneAsync(SceneMan1.Instance.GetLoadScene(), LoadSceneMode.Additive);
        operation.allowSceneActivation = false;
        //StartCoroutine(Progress());
        //SceneManager.sceneLoaded += LoadScreenLoaded;
    }


    IEnumerator Progress()
    {
        while(!operation.isDone)
        {
            Debug.Log("Progress " + operation.progress);
            if(operation.progress >= 0.9f)
            {
                Debug.Log("Almost done");
                operation.allowSceneActivation = true;
                SceneManager.UnloadSceneAsync("Loading");
            }
            yield return null;
        }
    }
    void LoadAsync()
    {
        Debug.Log("Loading " + SceneMan1.Instance.GetLoadScene());
        operation = SceneManager.LoadSceneAsync(SceneMan1.Instance.GetLoadScene(), LoadSceneMode.Additive);
        SceneManager.sceneLoaded += FinishLoading;
    }

    void FinishLoading(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded is " + scene.buildIndex + "Scene should be " + SceneMan1.Instance.GetLoadScene());
        if (scene.buildIndex == SceneMan1.Instance.GetLoadScene())
        {
            SceneManager.sceneLoaded -= FinishLoading;
            //SceneManager.sceneLoaded -= LoadScreenLoaded;
            //Destroy(gameObject);
            SceneManager.UnloadSceneAsync("Loading");
        }
    }

    void LoadScreenLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 1)
        {
            Debug.Log("Loading screen loaded");
            LoadAsync();
        }
    }
}
