using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadCambioColor : MonoBehaviour {

    public ColorChangingController cambioColor;
    public bool UsarHabilidad;
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
        if(Cooldown <= 0 && Input.GetButton("Habilidad1"))
        {
            UsarHabilidad = true;
        }
        if (UsarHabilidad)
        {
            DuracionHabilidad -= Time.deltaTime;
            if (DuracionHabilidad <= 0)
            {
                UsarHabilidad = false;
                Cooldown = Max_Cooldown;
                DuracionHabilidad = Max_Duracion;
            }
        }
	}
    void OnCollisionEnter(Collision collision)
    {
        if (UsarHabilidad)
        {

            if (collision.gameObject.tag == "Pink" )
            {
                cambioColor.SetColor(2);

            }
            if (collision.gameObject.tag == "Blue" )
            {
                cambioColor.SetColor(1);

            }
            if (collision.gameObject.tag == "Yellow" )
            {
                cambioColor.SetColor(0);

            }
        }
    }
 }
