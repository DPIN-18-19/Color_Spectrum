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
    private bool Init_Abi;
    public AudioClip ActivateShieldFx;
    public float VolumeActivateShieldFx;
    public GameObject ObjectShield;
    AudioSource source;
     
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        Max_Cooldown = Cooldown;
        Max_Duracion = DuracionHabilidad;
         Init_Abi = true;
        
    }
	
	// Update is called once per frame
	void Update () {

        Cooldown -= Time.deltaTime;
        GameplayManager.GetInstance().ability_cooldown = Cooldown;
       

        
        if ( SePuedeUsar == true && Input.GetButtonDown("Habilidad1") && Init_Abi == true)
        {

            source.PlayOneShot(ActivateShieldFx, VolumeActivateShieldFx);
            DuracionHabilidad = Max_Duracion;
            UsarHabilidad = true;
            Init_Abi = false;
            InstanciateShield();
            GameplayManager.GetInstance().ActivateAbility();

            // SePuedeUsar = false;

        }
        if (UsarHabilidad)
        {
            
            DuracionHabilidad -= Time.deltaTime;

           

            if (DuracionHabilidad <= 0)
            {
                UsarHabilidad = false;
                SePuedeUsar = false;
                GameplayManager.GetInstance().DeactivateAbility();

                Cooldown = Max_Cooldown;
               
                Cooldown = Max_Cooldown;
                Init_Abi = true;
                DuracionHabilidad = Max_Duracion;
            }
            

        }
        if (Cooldown <= 0 && Init_Abi == true)
        {
            //Shield.SetActive(false);
            SePuedeUsar = true;
            GameplayManager.GetInstance().ResetAbility();


            Cooldown = 0;
        }
        if (Cooldown <= 0)
        {
            Cooldown = 0;
        }

    }
    void InstanciateShield()
    {
        Quaternion rot = Quaternion.LookRotation(Vector3.up, Vector3.right);
        Instantiate(ObjectShield, transform.position - new Vector3(0, 1, 0), rot);
    }
}
