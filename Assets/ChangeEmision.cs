using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmision : MonoBehaviour {

    public float intensity;
    private float Timer;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Timer += Time.deltaTime;
        
         Renderer renderer = GetComponent<Renderer> ();
         Material mat = renderer.material;
 
         float emission = Mathf.PingPong (Time.time*6, 7f);
         Color baseColor = new Color(1.0f, 0.64f, 0.0f); ; //Replace this with whatever you want for your base color at emission level '1'
 
         Color finalColor = baseColor * Mathf.LinearToGammaSpace (emission);
 
         mat.SetColor ("_EmissionColor", finalColor);
     



        if (Timer < 5)
        {
         //   DynamicGI.SetEmissive(renderer, new Color(1f, 0.1f, 0.5f, 1.0f) * intensity);
        }
        if(Timer > 5 && Timer < 10)
        {
        //   DynamicGI.SetEmissive(renderer, new Color(1f, 0.1f, 0.5f, 1.0f) / intensity);
        }
        if(Timer > 10)
        {
         //  Timer = 0;
        }
    }
}
