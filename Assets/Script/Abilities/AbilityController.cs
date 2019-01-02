using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    GameObject holder;

    string cur_ability = "";

    // Habilidades

    Dash a_dash;
    AutoColor a_autocolor;
    throw_gre a_grenade;
    activate_Shield a_shield;

    // Use this for initialization
    void Start ()
    {
        holder = GameObject.FindGameObjectWithTag("Player");

        a_dash = holder.GetComponent<Dash>();
        a_autocolor = holder.GetComponent<AutoColor>();
        a_grenade = holder.GetComponent<throw_gre>();
        a_shield = holder.GetComponent<activate_Shield>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Activar una habilidad mediante un nombre
    public void ActivateAbility(string name)
    {
        // Desactivar habilidad previa
        DeactivateAbility();
        
        cur_ability = name;

        // Activar habilidad
        //- Consider adding a component instead of enabling/disabling components
        switch (name)
        {
            case "Dash":
                a_dash.enabled = true;
                break;
            case "CambioColor":
                a_autocolor.enabled = true;
                break;
            case "Granada":
                a_grenade.enabled = true;
                break;
            case "Escudo":
                a_shield.enabled = true;
                break;
            default:
                cur_ability = "";
                break;
        }

    }

    void DeactivateAbility()
    {
        // No hay ninguna habilidad activada
        if (cur_ability == "")
            return;

        // Desactivar habilidad
        switch (cur_ability)
        {
            case "Dash":
                a_dash.enabled = false;
                break;
            case "CambioColor":
                a_autocolor.enabled = false;
                break;
            case "Granada":
                a_grenade.enabled = false;
                break;
            case "Escudo":
                a_shield.enabled = false;
                break;
            default:
                break;
        }
    }
}
