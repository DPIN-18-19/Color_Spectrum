using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InicioMenu : MonoBehaviour {

    public GameObject ImagenControles;

    public void Controles()
    {
        ImagenControles.SetActive(true);
    }

    public void CerrarControles()
    {
        ImagenControles.SetActive(false);
    }

    private void Awake()
    {

        Time.timeScale = 1;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
