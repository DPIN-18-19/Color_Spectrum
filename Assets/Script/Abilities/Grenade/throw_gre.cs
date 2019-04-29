using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class throw_gre : MonoBehaviour {

    public GameObject grenade_prefabYellow;
    public GameObject grenade_prefabBlue;
    public GameObject grenade_prefabPink;

    public ColorChangingController cambioColor;

    public Transform FirePos;
    public bool AnimGranada;
    public bool AnimMoveGranada;
    public float ThrowForce = 10f;
    public float TiempoAnimRetardo;
    public PlayerJaneMoveController PlayerMove;
    public float Cooldown;
    private float MaxCooldown;
    public WeaponController weapon;
    public bool NotFire;
    public bool SepuedeUsar;
    private AudioSource source;
    public AudioClip FX_Throw_gre;
    public float VolumeFX_Throw_gre = 1f;

    Animator anim;
    // Use this for initialization
    void Start () {
        source = GetComponent<AudioSource>();
        anim = gameObject.GetComponent<Animator>();
        MaxCooldown = Cooldown;
       

    }
	
	// Update is called once per frame
	void Update () {
        GameplayManager.GetInstance().ability_cooldown = Cooldown;
       
        if (NotFire == true)
            weapon.is_firing = false;

        Cooldown -= Time.deltaTime;
        if(Cooldown <= 0)
        {
            Cooldown = 0;
            SepuedeUsar = true;
            GameplayManager.GetInstance().ResetAbility();

        }
        if (Input.GetButtonDown("Habilidad1") && Cooldown <= 0 )
        {
           
            NotFire = true;
            SepuedeUsar = false;
            GameplayManager.GetInstance().DeactivateAbility();
            anim.SetBool("Granada", true);
            weapon.gun.SetActive(false);
            source.PlayOneShot(FX_Throw_gre, VolumeFX_Throw_gre);
            Invoke("Granada", TiempoAnimRetardo);
            Invoke("ActivarArma", TiempoAnimRetardo+1f);
            Cooldown = MaxCooldown;
          

        }
        
        
     
        else
        {
            anim.SetBool("Granada", false);
            
        }

        }

    [SerializeField]
    float upForce = 5;
    public void Granada()
    {

        if (cambioColor.GetColor() == 0)
        {
           // Amarillo
            GameObject gren = Instantiate(grenade_prefabYellow, FirePos.position, transform.rotation) as GameObject;
            gren.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        }

        if (cambioColor.GetColor() == 1) {
            //  Azul
            GameObject gren = Instantiate(grenade_prefabBlue, FirePos.position, transform.rotation) as GameObject;
            gren.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        }
        // Debug.Log("Hoala");
        if (cambioColor.GetColor() == 2)
        {
            //rosa
            GameObject gren = Instantiate(grenade_prefabPink, FirePos.position, transform.rotation) as GameObject;
            gren.GetComponent<Rigidbody>().AddForce(transform.forward * ThrowForce, ForceMode.Impulse);
        }

        


    }
    public void ActivarArma()
    {
        weapon.gun.SetActive(true);
        
        NotFire = false;
    }
  
        
           
        }
    

