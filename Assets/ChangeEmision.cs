using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeEmision : MonoBehaviour {

    
    private float Timer;
    private Beacon Baliza;
    //public bool Damage;
    // Use this for initialization
    void Start () {
        Baliza = gameObject.GetComponent<Beacon>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Baliza.isDamage == false) {
            Timer += Time.deltaTime;

            Renderer renderer = GetComponent<Renderer>();
            Material mat = renderer.material;

            float emission = Mathf.PingPong(Time.time * 6, 7f);
            Color baseColor = new Color(1.0f, 0.64f, 0.0f);  //Replace this with whatever you want for your base color at emission level '1'

            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);
        }
        if (Baliza.isDamage)
        {
            Timer += Time.deltaTime;

            Renderer renderer = GetComponent<Renderer>();
            Material mat = renderer.material;

            float emission = Mathf.PingPong(Time.time * 17, 7f);
            Color baseColor = Color.red; //Replace this with whatever you want for your base color at emission level '1'

            Color finalColor = baseColor * Mathf.LinearToGammaSpace(emission);

            mat.SetColor("_EmissionColor", finalColor);
        }

    }
}
