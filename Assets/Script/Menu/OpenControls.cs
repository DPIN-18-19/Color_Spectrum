using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenControls : MonoBehaviour
{
    public GameObject Controles;
    public GameObject BandaNegra;
    public GameObject Botones;
    public GameObject Logo;
    // Use this for initialization
    void Start () {
		
	}
    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    public void AbrirControls()
    {
        Controles.SetActive(true);
        Botones.SetActive(false);
        BandaNegra.SetActive(false);
        Logo.SetActive(false);
       
    }
    public void CloseControls()
    {
        Controles.SetActive(false);
        Botones.SetActive(true);
        BandaNegra.SetActive(true);
        Logo.SetActive(true);
    }
}
