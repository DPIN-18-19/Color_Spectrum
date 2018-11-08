using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParedBlanca : MonoBehaviour
{
    // private NavMeshModifier navmesh;
    public GameObject ObstaculoAzul;
    public GameObject ObstaculoAmarillo;
    public GameObject ObstaculoRosa;
    public Material MaterialBlue;
    public Material MaterialYellow;
    public Material MaterialPink;
    private float CambiosColor = 0;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.tag == "Blue" && col.gameObject.layer == 20 && CambiosColor < 1)
        {
            gameObject.layer = 9;
            GetComponent<Renderer>().material = MaterialBlue;
            ObstaculoAzul.SetActive(false);
            CambiosColor = CambiosColor + 1;
        }
        else if (col.gameObject.tag == "Pink" && col.gameObject.layer == 19 && CambiosColor < 1)
        {
            gameObject.layer = 10;
            GetComponent<Renderer>().material = MaterialPink;
            ObstaculoRosa.SetActive(false);
            CambiosColor = CambiosColor + 1;
        }
        else if (col.gameObject.tag == "Yellow" && col.gameObject.layer == 18 && CambiosColor < 1)
        {
            gameObject.layer = 8;
            GetComponent<Renderer>().material = MaterialYellow;
            ObstaculoAmarillo.SetActive(false);
            CambiosColor = CambiosColor + 1;
        }
    }
}