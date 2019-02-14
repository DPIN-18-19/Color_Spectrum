using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Particle : MonoBehaviour {
    Slow_Motion Ralentizar;
    private float TiempoNormal;
    public float TiempoRalentizado;
    private ParticleSystem ps;
    // Use this for initialization
    void Start () {
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        ps = GetComponent<ParticleSystem>();
        TiempoNormal = 1f;
    }
	
	// Update is called once per frame
	void Update () {
        var main = ps.main;
        if (Ralentizar.ActivateAbility == true)
        {
            Debug.Log("RalentizadoEscopetaEffecto");
            main.simulationSpeed = TiempoRalentizado;
        }
        if (Ralentizar.ActivateAbility == false)
        {
            Debug.Log("RalentizadoEscopetaEffecto");
            main.simulationSpeed = TiempoNormal;
        }
    }
}
