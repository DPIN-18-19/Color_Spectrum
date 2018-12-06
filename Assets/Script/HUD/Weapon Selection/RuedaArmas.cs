using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RuedaArmas : MonoBehaviour
{

    public GameObject Rueda_Arm; //Imagen del panel de armas.

    public static bool GameIsPaused; //Booleano que detecta cuando esta activado o desactivado la rueda de armas.

    public GameObject Weapon1; //Arma situada slot 1.
    public GameObject Weapon2; //Arma situada slot 2.
    public GameObject Weapon3; //Arma situada slot 3.
    public dash Sash;
    public HabilidadCambioColor CambioColor;
    public Escopeta Gun;
    public Escopeta pistola;
    public Escopeta sniper;
    

    void Start()
    {

    }
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //Input avtiv. rueda armas.
        {
            Pause();
        }

        if (Input.GetKeyUp(KeyCode.Tab)) //Input desact. rueda armas.
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
        Time.timeScale = 0f; //Timepo de juego pausado.
        GameIsPaused = true;
    }

    public void Select_Weapon_1()
    {
        //pistola
        Weapon1.SetActive(true); //Activa el del slot 1.
        Weapon2.SetActive(false); //Desactiva el arma del slot 2.
        Weapon3.SetActive(false); //Desactiva el arma del slot 3.
        GameplayManager.GetInstance().ChangeGun(0);
        Sash.GetComponent <dash>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().enabled = true;

        pistola.GetComponent<Escopeta>().pistola = true;
        pistola.GetComponent<Escopeta>().sniper = false;
        pistola.GetComponent<Escopeta>().escopeta = false;

        //otherObject.GetComponent<NameOfScript>().enabled = false;
        Resume();
        Debug.Log("Pistola=" + Gun.GetComponent<Escopeta>().pistola);
        Debug.Log("Cambio Arma 1");
    }

    public void Select_Weapon_2()
    {
        
        Weapon1.SetActive(false);
        Weapon2.SetActive(true);
        Weapon3.SetActive(false);
        GameplayManager.GetInstance().ChangeGun(1);
        Sash.GetComponent<dash>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().UsarHabilidad = false;

        sniper.GetComponent<Escopeta>().pistola = false;
        sniper.GetComponent<Escopeta>().sniper = true;
        sniper.GetComponent<Escopeta>().escopeta = false;

        Resume();
        Debug.Log("Sniper=" + Gun.GetComponent<Escopeta>().sniper);
        Debug.Log("Cambio Arma 2");
    }

    public void Select_Weapon_3()
    {

        Weapon1.SetActive(false);
        Weapon2.SetActive(false);
        Weapon3.SetActive(true);
        GameplayManager.GetInstance().ChangeGun(2);
        Sash.GetComponent<dash>().enabled = true;
        CambioColor.GetComponent<HabilidadCambioColor>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().UsarHabilidad = false;

        Gun.GetComponent<Escopeta>().pistola = false;
        Gun.GetComponent<Escopeta>().sniper = false;
        Gun.GetComponent<Escopeta>().escopeta = true;

        Resume();
        Debug.Log("Escopeta="+Gun.GetComponent<Escopeta>().escopeta);
        Debug.Log("Cambio Arma 1");
    }

}

