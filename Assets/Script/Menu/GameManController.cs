using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
       /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused == false)
                Pause();

            if (GameIsPaused == true)
                Resume();

        }
        */
       /*if (Input.GetKeyDown(KeyCode.R))
        {

            /scenem.ToLevel1();
        }
        */
    }

    public void ToMenu()
    {
        scenem.ToMenu();
    }
    public void Reset()
    {
        scenem.ResetLevel();
    }
    
}
