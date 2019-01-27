using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioMenu : MonoBehaviour {

    public Animation BandaNegra;
    public Animation Logotipo;
    public Animation Botones;


    private void Awake()
    {
      
        BandaNegra.Play();
        Logotipo.Play();
        Botones.Play();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
