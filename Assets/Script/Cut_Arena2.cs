using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cut_Arena2 : MonoBehaviour {
    public GameObject ParedNegra1;
    public GameObject ParedNegra2;
    public GameObject ParedNegra3;
    public GameObject ParedOriginal1;
    public GameObject ParedOriginal2;
    public GameObject ParedOriginal3;
    public KillCondition Enemy_Arena;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_Arena.kill_enemies.Count == 0)
        {
            ParedNegra1.SetActive(false);
            ParedNegra2.SetActive(false);
            ParedNegra3.SetActive(false);
            ParedOriginal1.SetActive(true);
            ParedOriginal2.SetActive(true);
            ParedOriginal3.SetActive(true);
            Destroy(gameObject);
        }
        //Debug.Log(Enemy_Arena.survivour_num);
    }
    void OnTriggerEnter(Collider col)
    {
        if (col.gameObject.tag == "Player")
        {
            ParedNegra1.SetActive(true);
            ParedNegra2.SetActive(true);
            ParedNegra3.SetActive(true);
            ParedOriginal1.SetActive(false);
            ParedOriginal2.SetActive(false);
            ParedOriginal3.SetActive(false);
        }
    }
}
