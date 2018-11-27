using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManController : MonoBehaviour {

    SceneMan scenem;

	// Use this for initialization
	void Start ()
    {
        scenem = GetComponent<SceneMan>();
	}

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            scenem.ToMenu();
        }

        if (Input.GetKeyDown(KeyCode.R))
        {

            scenem.ToLevel1();
        }
    }
}
