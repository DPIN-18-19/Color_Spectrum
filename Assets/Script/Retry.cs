using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Retry : MonoBehaviour {

    public string Escena;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("r"))
        {
            SceneManager.LoadScene(Escena);
        }
        if (Input.GetKeyDown("escape"))
        {
            SceneManager.LoadScene("Menu"); ;
        }


    }
}
