using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityController : MonoBehaviour
{
    GameObject holder;

    string cur_ability = "";

    // Habilidades

    dash a_dash;
    HabilidadCambioColor a_autocolor;
    

    // Use this for initialization
    void Start ()
    {
        holder = GameObject.FindGameObjectWithTag("Player");

        a_dash = holder.GetComponent<dash>();
        a_autocolor = holder.GetComponent<HabilidadCambioColor>();
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
                Debug.Log("Dash activado");
                break;
            case "CambioColor":
                a_autocolor.enabled = true;
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
                Debug.Log("Dash desactivado");
                break;
            case "CambioColor":
                a_autocolor.enabled = false;
                break;
            default:
                break;
        }
    }
}
