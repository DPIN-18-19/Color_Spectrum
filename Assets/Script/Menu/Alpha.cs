using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Alpha : MonoBehaviour {

	
	
	// Update is called once per frame
	void Update () {
		
	}
    
    public float AlphaThreshold = 0.1f; //Valor mínimo de alpha para que un pixel sea accesible.

    void Start()
    {
        this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
    }
}
