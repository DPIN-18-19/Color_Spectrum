using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prueba : MonoBehaviour {

    public GameObject Path_1;
    public GameObject Path_2;

    //public GameObject Terminal;

    public List<GameObject> terminales;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (terminales.Count <= 1)
            Activar();

        //Debug.Log(terminales.Count);

        if (terminales[0] == null)
            terminales.RemoveAt(0);
      
            
	}

    /*void OnTriggerEnter(Collider other)
    {
        Path_1.SetActive(false);
        Path_2.SetActive(true);

        //Destroy(Terminal.gameObject);
    }*/

    void Activar()
    {
        Path_1.SetActive(false);
        Path_2.SetActive(true);
    }
}
