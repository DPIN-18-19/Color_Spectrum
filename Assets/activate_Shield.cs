using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class activate_Shield : MonoBehaviour {
    public GameObject Shield;

    public bool UsarHabilidad;
    public bool SePuedeUsar;
    public float Cooldown;
    private float Max_Cooldown;
    public float DuracionHabilidad;
    private float Max_Duracion;

    // Use this for initialization
    void Start () {
        Max_Cooldown = Cooldown;
        Max_Duracion = DuracionHabilidad;
        
    }
	
	// Update is called once per frame
	void Update () {

        Cooldown -= Time.deltaTime;
        if (Cooldown <= 0 &&/* DuracionHabilidad == Max_Duracion*/ SePuedeUsar == true && Input.GetButtonDown("Habilidad1"))
        {
            Shield.SetActive(true);
            DuracionHabilidad = Max_Duracion;
            UsarHabilidad = true;
            

        }
        if (UsarHabilidad)
        {
            
            DuracionHabilidad -= Time.deltaTime;
            if (DuracionHabilidad <= 0)
            {
                UsarHabilidad = false;
                SePuedeUsar = false;
                //GameplayManager.GetInstance().CambioColor(SePuedeUsar);
               
                Cooldown = Max_Cooldown;
                Shield.SetActive(false);
                DuracionHabilidad = Max_Duracion;
            }
            

        }
        if (Cooldown <= 0)
        {
            //Shield.SetActive(false);
            SePuedeUsar = true;
           
            // GameplayManager.GetInstance().CambioColor(SePuedeUsar);
            Cooldown = 0;
        }

    }
}
