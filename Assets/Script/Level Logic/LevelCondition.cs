using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelCondition : MonoBehaviour
{
    public delegate void Condition();

    public event Condition KillWave;

    ///////////////////////////////////////

    public List <GameObject> kill_enemies;

    bool do_event;

    ///////////////////////////////////////

    public static LevelCondition Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        //kill_enemies = new List<GameObject>();

        if(kill_enemies.Count != 0)
        {
            do_event = true;
        }
    }

    // Update is called once per frame
    void Update ()
    {
        CheckKillCondition();
    }

    void CheckKillCondition()
    {
        if (do_event)
        {
            if (kill_enemies.Count != 0)
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
                do_event = false;

                if (KillWave != null)
                    KillWave();
            }
            
        }
    }

}
