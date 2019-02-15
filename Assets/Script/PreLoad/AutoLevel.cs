using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLevel : MonoBehaviour
{
	void Start ()
    {
        SceneManager.LoadScene(DevPreload.Instance.scene_start);
    }
}
