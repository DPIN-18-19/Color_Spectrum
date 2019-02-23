using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLevel : MonoBehaviour
{
	void Start ()
    {
        SceneMan1.Instance.LoadSceneByName(DevPreload.Instance.scene_start);
        //SceneManager.LoadScene(DevPreload.Instance.scene_start);
    }
}
