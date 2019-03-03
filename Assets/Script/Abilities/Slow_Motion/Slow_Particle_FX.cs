using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Particle_FX : MonoBehaviour {

    private AudioSource source;
    Slow_Motion Ralentizar;

    // Use this for initialization
    void Start()
    {
        source = GetComponent<AudioSource>();
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Ralentizar.ActivateAbility == true)
        {
            source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
        }
        if (Ralentizar.ActivateAbility == false)
        {
            source.pitch = 1;
        }
    }
}