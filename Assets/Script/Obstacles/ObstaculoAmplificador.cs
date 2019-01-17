using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaculoAmplificador : MonoBehaviour {
    public GameObject Amplificador;
    public float TimeBetweenShot;
    public float TimeShots;
    public Transform FirePos;
    // Use this for initialization
    void Start () {
        TimeBetweenShot = TimeShots;
    }
	
	// Update is called once per frame
	void Update () {
        TimeBetweenShot = TimeBetweenShot - Time.deltaTime;
        if (TimeBetweenShot < 0)
        {
            Instantiate(Amplificador, FirePos.position, FirePos.rotation);
            TimeBetweenShot = TimeShots;
           
        }

    }
   

}
