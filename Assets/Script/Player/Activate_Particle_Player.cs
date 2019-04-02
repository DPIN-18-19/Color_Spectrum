using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Activate_Particle_Player : MonoBehaviour {
    public ParticleSystem ParticleEffect;
    public float TimeBetweenExplosion = 3f;
	// Use this for initialization
	void Start () {
        Invoke("Activate_Effect", TimeBetweenExplosion);
	}
	void Activate_Effect()
    {
        GameObject TemporalParticle;
        TemporalParticle = Instantiate(ParticleEffect.gameObject, transform.position, Quaternion.identity);
        TemporalParticle.GetComponent<ParticleSystem>().Play();
        
    }
	// Update is called once per frame
	void Update () {
		
	}
}
