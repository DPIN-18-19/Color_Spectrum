using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillCondition : MonoBehaviour
{
    ///////////////////////////////////////
    // Eventos

    //public enum TriggerType
    //{
    //    KillWave,
    //    AreaCollision
    //}

    //public TriggerType t_type;

    public delegate void Condition();

    public event Condition KillWave;
    
    ///////////////////////////////////////
    // Eliminar elementos
    
    public List <GameObject> kill_enemies;
    bool kill_checking;
    [HideInInspector]
    public int kill_num = 0;

    ///////////////////////////////////////

    private void Start()
    {
        //kill_enemies = new List<GameObject>();

        if(kill_enemies.Count != 0)
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
    void CheckKillCondition()
    {
        if (kill_checking)
        {
            if (kill_enemies.Count != kill_num)
            {
                for(int i = 0; i < kill_enemies.Count; ++i)
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
    }
}
