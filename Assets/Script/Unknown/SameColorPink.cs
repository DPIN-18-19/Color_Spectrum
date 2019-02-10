using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SameColorPink : MonoBehaviour {
  
    public bool ParedCambioNo;
    public float DuracionMismoColor;
    private float MaxDuracion;
    // Use this for initialization
    void Start () {

        MaxDuracion = DuracionMismoColor;

    }

    // Update is called once per frame
    void Update () {
      
	}
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player" && col.gameObject.layer == 10)
        {
           // DuracionMismoColor += Time.deltaTime;
            ParedCambioNo = true;
            Debug.Log("PasoPorAqui");
        }
       // if (col.gameObject.tag != "Player" && col.gameObject.layer != 10)
       // {
          
          //  DuracionMismoColor = 0;
       // }
    }
}
