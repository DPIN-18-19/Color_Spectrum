using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Pausa : MonoBehaviour {

    public GameObject Pausa_Menu;
    public GameObject Controles;
    public GameObject Botones;

    public bool GameIsPaused = false;
    bool KeyDown = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused == true && KeyDown ==false)
        {
            Resume();
            KeyDown = true;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && GameIsPaused == false && KeyDown == false)
        {
            Pause();
            KeyDown = true;
        }


        if (Input.GetKeyUp(KeyCode.Escape))
            KeyDown = false;


    }
   
                

    public void Pause()
    {
        Pausa_Menu.SetActive(true);
        VolverMenu();
        Time.timeScale = 0f; //Timepo de juego pausado.
        GameIsPaused = true;
        Debug.Log("Pausa");
    }

    public void Resume()
    {
        Pausa_Menu.SetActive(false);//Desactiva rueda armas
        Time.timeScale = 1f;
        GameIsPaused = false;
        Debug.Log("Resume");
    }
    
    public void Controls()
    {
        Botones.SetActive(false);
        Controles.SetActive(true);
    }
    public void VolverMenu()
    {
        Botones.SetActive(true);
        Controles.SetActive(false);
    }
}
