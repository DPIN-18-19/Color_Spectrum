using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Destroy_Terminal : MonoBehaviour {

    public ParticleSystem DestroyTerminal;
    public AudioClip FxDestroyTerminal;
   
    // Use this for initialization
    void Start () {
       
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Terminal")
        {
           
            Destroy(other.transform.parent.gameObject);
            Instantiate(DestroyTerminal.gameObject, other.transform.position, Quaternion.identity);

            Destroy(gameObject); 
        }

    }
}
