using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    static GameplayManager instance;


    public Text t_health;               // Player's health in UI
    public float health;
    public float max_health;

    public Text t_dash_cooldown;       // Player´s dash cooldown in UI
    public float dash_cooldown;
    public bool dash_activo;

    public Text t_grenade_cooldown;       // Player´s dash cooldown in UI
    public float grenade_cooldown;
    public bool grenade_activo;

    public Text t_cambio_cooldown;      // Player´s cambio color cooldown in UI
    public float cambio_cooldown;
    public bool cambio_activo;
    public bool usarhabilidad;

    public Text t_Shield_cooldown;      // Player´s cambio color cooldown in UI
    public float shield_cooldown;
    public bool Shield_activo;
    public bool usarhabilidadShield;

    //HUD Skills icons
    public GameObject dash_icon;        //Icon DAsh
    public GameObject no_dash_icon;     //Icon No dash
    public GameObject cambio_icon;      //Icon Cambio
    public GameObject no_cambio_icon;   //Icon No cambio
    public GameObject Cambio_Activo;
    public GameObject Grenade_icon;        //Icon DAsh
    public GameObject no_Grenade_icon;
    public GameObject shield_icon;      //Icon Cambio
    public GameObject no_shield_icon;   //Icon No cambio
    public GameObject Shield_Activo;



    // public GameObject father;

    public GameObject dash_father;      //Padre de ambos dash
    public GameObject cambio_father;
    public GameObject Grenade_father;
    public GameObject Shield_father;//Padre de ambos cambios

    //HUD Guns icons
    public GameObject Pistol;        //Icon Pistol
    public GameObject Sniper;        //Icon Sniper
    public GameObject Escopeta;      //Icon Escopeta

    //Health Icon
    public GameObject Health_icon;
    public GameObject Health_bar;

    //Health Materials
    public Material fullhealth;
    public Material midhealth;
    public Material lowhealth;

    public Material NullActivo;


    //HUD MAterials
    public Material cyan;
    public Material yellow;
    public Material magenta;

    // HUD small colored squares
    //- Take these out to HUD
    public GameObject circle_icon;
    public GameObject superior_icon;
    public GameObject inferior_icon;
    public GameObject activo_icon;
   


    // HUD screen transparency
    ////- Take these out to HUD
    public GameObject y_frame;
    public GameObject c_frame;
    public GameObject m_frame;

    // Use this for initialization
    void Start ()
    {
        instance = this;

        //y_icon.SetActive(true);
        //c_icon.SetActive(false);
        //m_icon.SetActive(false);

        //y_frame.SetActive(true);
        //c_frame.SetActive(false);
        //m_frame.SetActive(false);
    }


    public static GameplayManager GetInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update ()
    {
    }

    void FixedUpdate()
    {
        t_health.text = "" + Mathf.RoundToInt(health);
        t_dash_cooldown.text = "" + Mathf.RoundToInt(dash_cooldown);
        t_cambio_cooldown.text = "" + Mathf.RoundToInt(cambio_cooldown);
        t_grenade_cooldown.text = "" + Mathf.RoundToInt(grenade_cooldown);
        t_Shield_cooldown.text = "" + Mathf.RoundToInt(shield_cooldown);

        Health_bar.GetComponent<Image>().fillAmount = Map(health, 0, max_health, 0, 1);

        if (health <= 25)
            changemidhealth();

        if (health <= 10)
            changelowhealth();

        if (health > 25)
            changefullhealth();

        if (usarhabilidad == true )
        {
            Cambio_Activo.GetComponent<Image>().material = midhealth;
        }
        if(cambio_activo == true && usarhabilidad == false)
        {
            Cambio_Activo.GetComponent<Image>().material = fullhealth;
        }
        if(cambio_activo == false){
            Cambio_Activo.GetComponent<Image>().material = NullActivo;
        }
        if (usarhabilidadShield == true)
        {
            Shield_Activo.GetComponent<Image>().material = midhealth;
        }

        if (Shield_activo == true && usarhabilidadShield == false)
        {
            Shield_Activo.GetComponent<Image>().material = fullhealth;
        }
        if (Shield_activo == false)
        {

            Shield_Activo.GetComponent<Image>().material = NullActivo;
        }

        //vida.text = Vida.ToString();
        //textvida.text = "= " + Vida.ToString();
    }

    // Update accordingly to color

    private float Map (float value, float inMin, float inMax, float outMin, float outMax)
    {
        return (value - inMin) * (outMax - outMin) / (inMax - inMin) + outMin;
    }
    
    public void ChangeColor(int color)
    {
        DeactivateAll();

        switch(color)
        {
            case 0:                     // Yellow Color
                circle_icon.GetComponent<Image>().material = yellow;
                circle_icon.transform.GetChild(0).GetComponent<Image>().material = yellow;

                activo_icon.GetComponent<Image>().material = yellow;
                activo_icon.transform.GetChild(0).GetComponent<Image>().material = yellow;

                superior_icon.GetComponent<Image>().material = magenta;
                superior_icon.transform.GetChild(0).GetComponent<Image>().material = magenta;

                inferior_icon.GetComponent<Image>().material = cyan;
                inferior_icon.transform.GetChild(0).GetComponent<Image>().material = cyan;

                //father.transform.rotation = Quaternion.Euler(0, 0, 120);

                y_frame.SetActive(true);
                break;
            case 1:                     // Cyan Color
                circle_icon.GetComponent<Image>().material = cyan;
                circle_icon.transform.GetChild(0).GetComponent<Image>().material = cyan;

                activo_icon.GetComponent<Image>().material = cyan;
                activo_icon.transform.GetChild(0).GetComponent<Image>().material = cyan;

                superior_icon.GetComponent<Image>().material = yellow;
                superior_icon.transform.GetChild(0).GetComponent<Image>().material = yellow;

                inferior_icon.GetComponent<Image>().material = magenta;
                inferior_icon.transform.GetChild(0).GetComponent<Image>().material = magenta;

               // father.transform.rotation = Quaternion.Euler(0, 0, 120);

                c_frame.SetActive(true);
                break;
            case 2:                     // Magenta Color
                circle_icon.GetComponent<Image>().material = magenta;
                circle_icon.transform.GetChild(0).GetComponent<Image>().material = magenta;

                activo_icon.GetComponent<Image>().material = magenta;
                activo_icon.transform.GetChild(0).GetComponent<Image>().material = magenta;

                superior_icon.GetComponent<Image>().material = cyan;
                superior_icon.transform.GetChild(0).GetComponent<Image>().material = cyan;

                inferior_icon.GetComponent<Image>().material = yellow;
                inferior_icon.transform.GetChild(0).GetComponent<Image>().material = yellow;

               // father.transform.rotation = Quaternion.Euler(0, 0, 120);

                m_frame.SetActive(true);
                break;
            default:
                break;
        }

    }
    public void changemidhealth()
    {
        Health_icon.GetComponent<Image>().material = midhealth;
        Health_bar.GetComponent<Image>().material = midhealth;
    }

    public void changelowhealth()
    {
        Health_icon.GetComponent<Image>().material = lowhealth;
        Health_bar.GetComponent<Image>().material = lowhealth;
    }

    public void changefullhealth()
    {
        Health_icon.GetComponent<Image>().material = fullhealth;
        Health_bar.GetComponent<Image>().material = fullhealth;
    }

    public void ChangeGun (int gun)
    {
        DesactivateAllGuns();
        switch (gun)
        {
            case 0:
                Pistol.SetActive(true);
                Shield_father.SetActive(true);
                break;
            case 1:
                Sniper.SetActive(true);
                cambio_father.SetActive(true);
                break;
            case 2:
                Escopeta.SetActive(true);
                Grenade_father.SetActive(true);
                break;
        }
       
}

    public void Dash(bool activo)
    {
       switch (activo)
        {
            case false:   //if(cambio_activo == false)
        {
           
            dash_icon.SetActive(false);
            no_dash_icon.SetActive(true);
            break;
        }
               

            case true:    //if (cambio_activo == true)
        {
           
            dash_icon.SetActive(true);
            no_dash_icon.SetActive(false);
            break;

        }
    } 
         
    }
    public void Grenade(bool activo)
    {
        switch (activo)
        {
            case false:   //if(cambio_activo == false)
                {

                    Grenade_icon.SetActive(false);
                    no_Grenade_icon.SetActive(true);
                    break;
                }


            case true:    //if (cambio_activo == true)
                {

                    Grenade_icon.SetActive(true);
                    no_Grenade_icon.SetActive(false);
                    break;

                }
                

}

    }

    public void CambioColor(bool activo)
    {
        switch (activo)
        {
            case false:
                cambio_icon.SetActive(false);
                no_cambio_icon.SetActive(true);
                break;
            case true:
                cambio_icon.SetActive(true);
                no_cambio_icon.SetActive(false);
                break;
        }

    }
    public void Shield(bool activo)
    {
        switch (activo)
        {
            case false:
                shield_icon.SetActive(false);
                no_shield_icon.SetActive(true);
                break;
            case true:
                shield_icon.SetActive(true);
                no_shield_icon.SetActive(false);
                break;
        }
        

}

    void DeactivateAll()
    {
        //y_icon.SetActive(false);
       // c_icon.SetActive(false);
        //m_icon.SetActive(false);

        y_frame.SetActive(false);
        c_frame.SetActive(false);
        m_frame.SetActive(false);
    }

    void DesactivateAllGuns()
    {
        Pistol.SetActive(false);
        Escopeta.SetActive(false);
        Sniper.SetActive(false);

        Grenade_father.SetActive(false);
        cambio_father.SetActive(false);
        Shield_father.SetActive(false);
    }
}
