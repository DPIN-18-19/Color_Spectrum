using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCondition : MonoBehaviour
{
    ///////////////////////////////////////
    // Eventos

    public delegate void Condition();

    public event Condition KillWave;
    
    ///////////////////////////////////////
    // Eliminar elementos
    public List <GameObject> kill_enemies;      // Elementos a eliminar
    bool kill_checking;                         // Realizar comprobacion de muerte
    [HideInInspector]
    public int survivour_num = 0;               // Numero de supervivientes maximi

    ///////////////////////////////////////

    private void Start()
    {
        Debug.Log("Start Kill Condition");
        if (kill_enemies == null)
        {
            Debug.Log("New kill enemy list");
            kill_enemies = new List<GameObject>();
        }

        if(kill_enemies.Count != 0)
        {
            kill_checking = true;
        }
    }

    public KillCondition()
    {
        if (kill_enemies == null)
        {
            Debug.Log("New kill enemy list");
            kill_enemies = new List<GameObject>();
        }

        if (kill_enemies.Count != 0)
        {
            kill_checking = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        CheckKillCondition();
    }

    // Chequear si se han eliminado los elementos específicos
    public void CheckKillCondition()
    {
        if (kill_checking)
        {
            Debug.Log("Checking");

            if (kill_enemies.Count > survivour_num)
            {
                Debug.Log("Enemies alive " + kill_enemies.Count);
                for (int i = 0; i < kill_enemies.Count; ++i)
                {
                    if (kill_enemies[i] == null)
                    {
                        //Debug.Log("Dead");
                        kill_enemies.RemoveAt(i);
                        break;
                    }
                }
            }
            else
            {
                kill_checking = false;

                if (KillWave != null)
                    KillWave();
            }
            
        }
    }

    // Activar/desactivar 
    public void SwitchKill()
    {
        kill_checking = !kill_checking;
        Debug.Log("Kill_checking is " + kill_checking);
    }

    public void SurvivourAmount(int amount)
    {
        survivour_num = amount;
    }
}
