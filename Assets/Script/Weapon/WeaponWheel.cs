using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponWheel : MonoBehaviour
{
    Transform UI_weapon_wheel; //Imagen del panel de armas.
    List<Transform> UI_weapon_icons;
    
    //public weapon_List gun_list;            // Lista de armas;
    public IWeaponChipList eq_weapons;

    public static bool GameIsPaused; //Booleano que detecta cuando esta activado o desactivado la rueda de armas.
    
    //dash Sash;
    //HabilidadCambioColor CambioColor;
    WeaponController weapon;
    AbilityController ability;

    ///////////////////////////////////////////////////////////////////////////////////////////////////

    private void Awake()
    {
        UI_weapon_wheel = transform.GetChild(0);
        // Change this to accept differen weapon wheels of different sizes
        UI_weapon_icons = new List<Transform>();
        for (int i = 0; i < UI_weapon_wheel.childCount; ++i)
        {
            UI_weapon_icons.Add(UI_weapon_wheel.GetChild(i).GetChild(0));
        }

        //Sash = GameObject.FindGameObjectWithTag("Player").GetComponent<dash>();
        //CambioColor = GameObject.FindGameObjectWithTag("Player").GetComponent<HabilidadCambioColor>();
        weapon = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<WeaponController>();
        ability = GameObject.FindGameObjectWithTag("Player").GetComponentInChildren<AbilityController>();
    }

    void Start()
    {
        UpdateLook();
        Select_Weapon_0();
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

    public void Pause()
    {
        UpdateLook();
        UI_weapon_wheel.gameObject.SetActive(true);
        Time.timeScale = 0f; //Timepo de juego pausado.
        GameIsPaused = true;
    }

    public void Resume()
    {
        UI_weapon_wheel.gameObject.SetActive(false);//Desactiva rueda armas
        Time.timeScale = 1f;
        GameIsPaused = false;
    }
    
    void UpdateLook()
    {
        for(int i = 0; i < UI_weapon_icons.Count; ++i)
        { 
            // Check if there are equipped weapons
            if (i < weapon.eq_weapons.i_weapon_chips.Count)
                //UI_weapon_icons[i].GetComponent<Image>().sprite = gun_list.weapon_list[i].display_icon;
            UI_weapon_icons[i].GetComponent<Image>().sprite = eq_weapons.i_weapon_chips[i].base_gun.display_icon;
            else
                UI_weapon_icons[i].GetComponent<Image>().sprite = null;
        }
    }

    public void Select_Weapon_0()
    {
        //pistola
        Debug.Log("Here");
        GameplayManager.GetInstance().ChangeGun(0, UI_weapon_icons[0].GetComponent<Image>().sprite);
        Debug.Log("Here3");
        GameplayManager.GetInstance().ChangeAbility(eq_weapons.i_weapon_chips[0].ability.display_icon, eq_weapons.i_weapon_chips[0].ability.is_instant);
        Debug.Log("Here2");
        //Sash.GetComponent<dash>().enabled = false;
        //CambioColor.GetComponent<HabilidadCambioColor>().enabled = true;

        weapon.GetNewWeapon(0);
        ability.ActivateAbility(eq_weapons.i_weapon_chips[0].ability_name);

        Debug.Log("Gun selected");
        Resume();
    }

    public void Select_Weapon_1()
    {
        GameplayManager.GetInstance().ChangeGun(1, UI_weapon_icons[1].GetComponent<Image>().sprite);
        GameplayManager.GetInstance().ChangeAbility(eq_weapons.i_weapon_chips[1].ability.display_icon, eq_weapons.i_weapon_chips[1].ability.is_instant);
        //Sash.GetComponent<dash>().enabled = false;
        //CambioColor.GetComponent<HabilidadCambioColor>().enabled = false;
        //CambioColor.GetComponent<HabilidadCambioColor>().UsarHabilidad = false;

        weapon.GetNewWeapon(1);
        ability.ActivateAbility(eq_weapons.i_weapon_chips[1].ability_name);

        Debug.Log("Sniper selected");
        Resume();
    }

    public void Select_Weapon_2()
    {
        GameplayManager.GetInstance().ChangeGun(2, UI_weapon_icons[2].GetComponent<Image>().sprite);
        GameplayManager.GetInstance().ChangeAbility(eq_weapons.i_weapon_chips[2].ability.display_icon, eq_weapons.i_weapon_chips[2].ability.is_instant);
        //Sash.GetComponent<dash>().enabled = true;
        //CambioColor.GetComponent<HabilidadCambioColor>().enabled = false;
        //CambioColor.GetComponent<HabilidadCambioColor>().UsarHabilidad = false;

        weapon.GetNewWeapon(2);
        ability.ActivateAbility(eq_weapons.i_weapon_chips[2].ability_name);

        Debug.Log("Shotgun selected");

        Resume();
    }

}
