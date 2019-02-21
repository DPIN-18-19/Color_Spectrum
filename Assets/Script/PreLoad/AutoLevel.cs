using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoLevel : MonoBehaviour
{
    public string l_name;

	// Use this for initialization
	void Start ()
    {
        if (l_name != "")
            SceneManager.LoadScene(l_name);
        else
            SceneManager.LoadScene("Main_Menu");
    }
}
