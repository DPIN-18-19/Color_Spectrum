using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour
{
    AsyncOperation loading;
    AsyncOperation unloading;

    // Loading is done in Start due to Unity bugs
    // https://issuetracker.unity3d.com/issues/loadsceneasync-allowsceneactivation-flag-is-ignored-in-awake
    void Start()
    {
       // Debug.Log("Loading " + SceneMan1.Instance.GetLoadScene());
        loading = SceneManager.LoadSceneAsync(SceneMan1.Instance.GetLoadScene(), LoadSceneMode.Additive);
        loading.allowSceneActivation = false;
        SceneMan1.Instance.blackscreen.GetComponent<CanvasGroup>().alpha = 0;
        StartCoroutine(LoadingNewScene());
        //SceneManager.sceneLoaded += LoadScreenLoaded;
    }

    // Loading is done in Update due to Unity bugs
    // https://issuetracker.unity3d.com/issues/loadsceneasync-allowsceneactivation-flag-is-ignored-in-awake
    private void Update()
    {
        if (loading.isDone)
        {
            SceneMan1.Instance.PostLoad();
            unloading = SceneManager.UnloadSceneAsync("Loading");
        }
    }

    //void LoadScreenLoaded(Scene scene, LoadSceneMode mode)
    //{
    //    if (scene.buildIndex == 1)
    //    {
    //        Debug.Log("Loading screen loaded");
    //        SceneManager.SetActiveScene(scene);
    //        LoadAsync();
    //    }
    //}

    //void LoadAsync()
    //{
    //    Debug.Log("Loading " + SceneMan1.Instance.GetLoadScene());
    //    loading = SceneManager.LoadSceneAsync(SceneMan1.Instance.GetLoadScene(), LoadSceneMode.Additive);
    //    loading.allowSceneActivation = false;
    //    //SceneManager.sceneLoaded += FinishLoading;
    //    StartCoroutine(Progress());
    //}

    IEnumerator LoadingNewScene()
    {
        while(!loading.isDone)
        {
           // Debug.Log("Progress " + loading.progress.ToString());
            if(loading.progress >= 0.9f)
            {
           //     Debug.Log("I'm ready");
                loading.allowSceneActivation = true;
                //unloading = SceneManager.UnloadSceneAsync("Loading");
            }
            yield return null;
        }
    }


    //void FinishLoading(Scene scene, LoadSceneMode mode)
    //{
    //    Debug.Log("Scene loaded is " + scene.buildIndex + "Scene should be " + SceneMan1.Instance.GetLoadScene());
    //    if (scene.buildIndex == SceneMan1.Instance.GetLoadScene())
    //    {
    //        SceneManager.sceneLoaded -= FinishLoading;
    //        SceneManager.SetActiveScene(scene);
    //        //SceneManager.sceneLoaded -= LoadScreenLoaded;
    //        //Destroy(gameObject);
    //        SceneManager.UnloadSceneAsync("Loading");
    //    }
    //}
}
