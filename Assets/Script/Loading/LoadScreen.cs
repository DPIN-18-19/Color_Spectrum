using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadScreen : MonoBehaviour
{
    void OnEnable()
    {
        SceneManager.LoadSceneAsync(SceneMan1.Instance.GetLoadScene(), LoadSceneMode.Additive);
        SceneManager.sceneLoaded += FinishLoading;
    }
    
    void FinishLoading(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= FinishLoading;
        Destroy(this.gameObject);
    }
}