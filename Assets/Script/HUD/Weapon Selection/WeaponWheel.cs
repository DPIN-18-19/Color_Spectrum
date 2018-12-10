using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponWheel : MonoBehaviour
{
    GameObject Rueda_Arm; //Imagen del panel de armas.

    public static bool GameIsPaused; //Booleano que detecta cuando esta activado o desactivado la rueda de armas.
    
    dash Sash;
    HabilidadCambioColor CambioColor;
    WeaponController weapon;

    ///////////////////////////////////////////////////////////////////////////////////////////////////

    void Start()
    {
        Rueda_Arm = transform.GetChild(0).gameObject;

        Debug.Log("Start");

        Sash = GameObject.FindGameObjectWithTag("Player").GetComponent<dash>();
        CambioColor = GameObject.FindGameObjectWithTag("Player").GetComponent<HabilidadCambioColor>();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab)) //Input avtiv. rueda armas.
        {
            Pause();
            Debug.Log("Pause Here");
        }

        if (Input.GetKeyUp(KeyCode.Tab)) //Input desact. rueda armas.
        {
            Resume();
        }
    }

    public void Pause()
    {
        Rueda_Arm.SetActive(true);
        Time.timeScale = 0f; //Timepo de juego pausado.
        GameIsPaused = true;
    }

    public void Resume()
    {
        Rueda_Arm.SetActive(false);//Desactiva rueda armas
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    public void Select_Weapon_0()
    {
        //pistola
        GameplayManager.GetInstance().ChangeGun(0);
        Sash.GetComponent<dash>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().enabled = true;
        
        weapon.GetNewWeapon(0);

        Debug.Log("Gun selected");
        Resume();
    }

    public void Select_Weapon_1()
    {
        GameplayManager.GetInstance().ChangeGun(1);
        Sash.GetComponent<dash>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().UsarHabilidad = false;

        weapon.GetNewWeapon(1);

        Debug.Log("Sniper selected");
        Resume();
    }

    public void Select_Weapon_2()
    {
        GameplayManager.GetInstance().ChangeGun(2);
        Sash.GetComponent<dash>().enabled = true;
        CambioColor.GetComponent<HabilidadCambioColor>().enabled = false;
        CambioColor.GetComponent<HabilidadCambioColor>().UsarHabilidad = false;
        
        weapon.GetNewWeapon(2);

        Debug.Log("Shotgun selected");

        Resume();
    }

}
