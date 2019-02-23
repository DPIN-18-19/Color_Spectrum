using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_ParticleDie_B : MonoBehaviour {
    public ParticleSystem ParticleBlue;
    public float TimeBetweenExplosion = 0.3f;
    public bool Doit = true;
    private ParticleSystem ps;
    Slow_Motion Ralentizar;
    
    // Use this for initialization
    void Start () {
        ps = GetComponent<ParticleSystem>();
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        //Invoke("Activate_Effect", TimeBetweenExplosion);
    }
    private void Update()
    {
       
        var main2 = ps.main;
        if (Ralentizar.ActivateAbility == true)
        {
           
            main2.simulationSpeed = Ability_Time_Manager.Instance.RalentizarComprimir;
        }
        if (Ralentizar.ActivateAbility == false)
        {
           
            main2.simulationSpeed = 1f;
        }


       
        
       
        TimeBetweenExplosion -= Time.deltaTime;
        if(TimeBetweenExplosion<= 0 && Doit == true)
        {
            Activate_Effect();
        }
    }


    // Update is called once per frame

    void Activate_Effect()
    {
        GameObject TemporalParticle;
        TemporalParticle = Instantiate(ParticleBlue.gameObject, transform.position, Quaternion.identity);
        TemporalParticle.GetComponent<ParticleSystem>().Play();
        Doit = false;
        // Destroy(gameObject);
    }
}
