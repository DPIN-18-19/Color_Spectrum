
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Botiquin : MonoBehaviour {
    public float Salud;
    public HealthController vida;
    public AudioClip SonidoCurar;
    AudioSource source;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        vida = GameObject.Find("Player_Naomi").GetComponent<HealthController>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}
    private void OnTriggerEnter(Collider col)
    {
        if(col.gameObject.tag == "Player" && vida.health < vida.max_health)
        {
            AudioSource.PlayClipAtPoint(SonidoCurar, transform.position);
            vida.RestoreHealth(Salud);
            
            Destroy(gameObject);
        }
    }
}
