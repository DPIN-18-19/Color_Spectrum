using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaCondition : MonoBehaviour
{

    /////////////////////////////////////////////
    // Events
    public delegate void Condition();
    public event Condition EnterArea;       // Evento de entrada
    public event Condition ExitArea;        // Evento de salida

    ////////////////////////////////////////////
    // Variables

    // Clases de áreas
    public enum AreaType
    {
        DoOnce,             // El trigger se acciona una vez
        OnlyEnter,          // Eltrigger se acciona siempre que se entra
        InAndOut            // El trigger se acciona cada vez que se entra y que se sale
    }
    public AreaType a_type;
    
    // Clases de accionador del evento
    public enum TrigereeType
    {
        Player,             // Accionador es el jugador
        Custom              // Accionador customizado
    }
    public TrigereeType t_type;

    GameObject triggeree;               // Quien acciona el trigger
    public GameObject custom_target;    // Campo no obligatotio. Accionador customizado.

    bool area_checking;     // Trigger está activado

    // Use this for initialization
    void Start ()
    {
		switch(t_type)
        {
            case TrigereeType.Player:
                triggeree = GameObject.FindGameObjectWithTag("Player");
                break;
            case TrigereeType.Custom:
                triggeree = custom_target;
                break;
            default:
                break;
        }

        area_checking = true;
	}
	
    private void OnTriggerEnter(Collider other)
    {
        if (area_checking)
        {
            if (other.transform.tag == triggeree.tag)
            {
                if (EnterArea != null)
                    EnterArea();

                // Destruir trigger si es de único uso
                if (a_type == AreaType.DoOnce)
                    Destroy(this.gameObject);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Solo si trigger se activa por salidas
        if (a_type == AreaType.InAndOut) 
        {
            if (area_checking)
            {
                if (other.transform.tag == triggeree.tag)
                {
                    if (ExitArea != null)
                        ExitArea();
                }
            }
        }
    }
}
