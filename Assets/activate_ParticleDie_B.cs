using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_ParticleDie_B : MonoBehaviour {
    public ParticleSystem ParticleBlue;
    
    // Use this for initialization
    void Start () {
        Invoke("Activate_Effect", 0.3f);
    }


// Update is called once per frame

    void Activate_Effect()
    {
        GameObject TemporalParticle;
        TemporalParticle = Instantiate(ParticleBlue.gameObject, transform.position, Quaternion.identity);
        TemporalParticle.GetComponent<ParticleSystem>().Play();
        // Destroy(gameObject);
    }
}
