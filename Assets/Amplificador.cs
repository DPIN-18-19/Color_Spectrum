using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Amplificador : MonoBehaviour {

    float Scalex;
    float Scaley;
    float Scalez;
    float PositionX;
    public float destruir = 3f;
    public float velocidad = 1;
    public float tiempoNoHacerDaño;
    private float tiempoinmuneravilidad;
    Vector3 NewSize;


	// Use this for initialization
	void Start () {
        tiempoNoHacerDaño = tiempoinmuneravilidad;
	}
	
	// Update is called once per frame
	void Update () {
        PositionX += 0.01f * Time.deltaTime * 1f ; // poner 0.5f si es amplificador2
        Scalex += 0.01f * Time.deltaTime * velocidad;
        Scaley += 0.01f * Time.deltaTime * velocidad;
        Scalez += 0.01f * Time.deltaTime * velocidad;

        transform.localScale += new Vector3(Scalex, Scaley, Scalez);
        transform.position += new Vector3(PositionX, 0, 0);
        Invoke("Destruir", destruir);
        if(tiempoNoHacerDaño <= 0)
        {
            tiempoNoHacerDaño = tiempoinmuneravilidad;
        }
    }
    void Destruir()
    {
       Destroy(gameObject);
    }
    //Solo funciona si es azul
    private void OnTriggerEnter(Collider col)
    {
        //  TimeBetweenShot = TimeBetweenShot - Time.deltaTime;
        if (col.gameObject.layer == 8 && col.gameObject.tag == "Player" && tiempoNoHacerDaño <= tiempoinmuneravilidad)
        {
            //col.gameObject.SendMessage("HacerDaño");
            //Destruir();
            tiempoNoHacerDaño = tiempoNoHacerDaño - Time.deltaTime;
        }
        if (col.gameObject.layer == 9 && col.gameObject.tag == "Player" && tiempoNoHacerDaño <= tiempoinmuneravilidad)
        {
           // col.gameObject.SendMessage("RecibeVida");
            //  Destruir();
            tiempoNoHacerDaño = tiempoNoHacerDaño - Time.deltaTime;
        }
        if (col.gameObject.layer == 10 && col.gameObject.tag == "Player" && tiempoNoHacerDaño <= tiempoinmuneravilidad)
        {
          //  col.gameObject.SendMessage("HacerDaño");
            // Destruir();
            tiempoNoHacerDaño = tiempoNoHacerDaño - Time.deltaTime;
        }

    }
}
