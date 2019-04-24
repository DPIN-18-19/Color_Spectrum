using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow_Motion : MonoBehaviour {

    public bool ActivateAbility;
    public bool SePuedeUsar;
    public float Cooldown;
    private float Max_Cooldown;
    public float DuracionHabilidad;
    private float Max_Duracion;
    private bool Init_Abi;
    public AudioClip ActivateSlowFx;
    public float VolumeActivateSlowFx = 1f;
    AudioSource source;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    void Start () {
        Max_Cooldown = Cooldown;
        Max_Duracion = DuracionHabilidad;
        Init_Abi = true;
    }
	
	// Update is called once per frame
	void Update () {

        Cooldown -= Time.deltaTime;

        if (SePuedeUsar == true && Input.GetButtonDown("Habilidad1") && Init_Abi == true)
        {

            source.PlayOneShot(ActivateSlowFx, VolumeActivateSlowFx);
            DuracionHabilidad = Max_Duracion;
            ActivateAbility = true;
            Init_Abi = false;   
           // GameplayManager.GetInstance().ActivateAbility();

            // SePuedeUsar = false;

        }
        if (ActivateAbility)
        {

            DuracionHabilidad -= Time.deltaTime;



            if (DuracionHabilidad <= 0)
            {
                ActivateAbility = false;
                SePuedeUsar = false;


                Cooldown = Max_Cooldown;

                Cooldown = Max_Cooldown;
                Init_Abi = true;
                DuracionHabilidad = Max_Duracion;
            }
        }
        if (Cooldown <= 0 && Init_Abi == true)
        {
            SePuedeUsar = true;
           // GameplayManager.GetInstance().ResetAbility();


            Cooldown = 0;
        }
        if (Cooldown <= 0)
        {
            Cooldown = 0;
        }




        //if (Input.GetKey(KeyCode.Space))
        //{
        //    ActivateAbility = true;
        //}
        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    ActivateAbility = false;
        //}
    }
}
