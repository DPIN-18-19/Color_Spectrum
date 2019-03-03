using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountArena1 : MonoBehaviour
{
    public delegate void Condition();
    public event Condition EndArea;         // Evento de entrada

    public KillCondition lvl_trigger1;
   

    int arena_count = 0;
    public Text arena_text;

    // Use this for initialization
    void Start()
    {
        lvl_trigger1.KillWave += Count;
       

        arena_count = 0;
    }

    // Update is called once per frame
    void Update()
    {
        arena_text.text = arena_count.ToString();

        if (arena_count >= 1)
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