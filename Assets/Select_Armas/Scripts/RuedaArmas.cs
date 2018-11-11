using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaArmas : MonoBehaviour {

    public GameObject Rueda_Arm; //Imagen del panel de armas.

    public static bool GameIsPaused; //Booleano que detecta cuando esta activado o desactivado la rueda de armas.

    public GameObject Weapon1; //Arma situada slot 1.
    public GameObject Weapon2; //Arma situada slot 2.
    public GameObject Weapon3; //Arma situada slot 3.



    void Start () {
		
	}

    
    void Update()
    {



        if (Input.GetKeyDown(KeyCode.Tab)) //Input avtiv. o desact. rueda armas.
        {
            
                Pause();
            
        }

        if (Input.GetKeyUp(KeyCode.Tab))
        {
            Resume();
        }

    }

    public void Resume()
    {
        Rueda_Arm.SetActive(false);//Desactiva rueda armas
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    public void Pause()
    {
        Rueda_Arm.SetActive(true);
        Time.timeScale = 0.1f; //Timepo de juego pausado.
        GameIsPaused = true;
    }

    public void Select_Weapon_1()
    {
        Weapon1.SetActive(true); //Activa el del slot 1.
        Weapon2.SetActive(false); //Desactiva el arma del slot 2.
        Weapon3.SetActive(false); //Desactiva el arma del slot 3.

        Resume();

        Debug.Log("Cambio Arma 1");
    }

    public void Select_Weapon_2()
    {
        Weapon1.SetActive(false); 
        Weapon2.SetActive(true); 
        Weapon3.SetActive(false); 

        Resume();

        Debug.Log("Cambio Arma 2");
    }

    public void Select_Weapon_3()
    {
        Weapon1.SetActive(false); 
        Weapon2.SetActive(false); 
        Weapon3.SetActive(true); 

        Resume();

        Debug.Log("Cambio Arma 1");
    }

}
