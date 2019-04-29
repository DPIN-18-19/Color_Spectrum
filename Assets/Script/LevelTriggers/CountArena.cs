using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountArena : MonoBehaviour
{
    public delegate void Condition();
    public event Condition EndArea;         // Evento de entrada

    public List<KillCondition> lvl_trigger_l;

    int arena_count = 0;
    public Text arena_text;

    // Use this for initialization
    void Start()
    {
        for(int i = 0; i < lvl_trigger_l.Count; ++i)
        {
            lvl_trigger_l[i].KillWave += Count;
        }

        //lvl_trigger1.KillWave += Count;
        //lvl_trigger2.KillWave += Count;
        //lvl_trigger3.KillWave += Count;
        //lvl_trigger4.KillWave += Count;
        //lvl_trigger5.KillWave += Count;

        arena_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        arena_text.text = arena_count.ToString();

        if (arena_count >= lvl_trigger_l.Count)
        {
            if (EndArea != null)
                EndArea();
        }

    }

    void Count()
    {
        ++arena_count;
    }
}