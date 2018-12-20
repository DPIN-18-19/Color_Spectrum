using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Animator anim;
  public ParticleSystem Change_effectYellow;
  public ParticleSystem Change_effectBlue;
  public ParticleSystem Change_effectPink;

   
    public bool isFiring;

    //public float MoveSpeed;         // PLayer's speed
    private Rigidbody myRigidbody;

    WeaponController weapon;

    private Camera maincamera;      // Player Camera


    public Escopeta pistola;
    public Escopeta sniper;
    public Escopeta escopeta;

    public GunController theGun;    // Player's Gun
    
    Collider m_collider;

    //Renderers to change
    [SerializeField]
    public List<Renderer> renderersToChangeColor;
   

    // Player's material
    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;

    public Material DamageBlueMaterial;
    public Material DamageYellowMaterial;
    public Material DamagePinkMaterial;

    public Material HealthBlueMaterial;
    public Material HealthYellowMaterial;
    public Material HealthPinkMaterial;

    public Material BlackGlitchBlueMaterial;
    public Material BlackGlitchYellowMaterial;
    public Material BlackGlitchPinkMaterial;

    public Material BlackBlueMaterial;
    public Material BlackYellowMaterial;
    public Material BlackPinkMaterial;
    public bool Fire;


    //////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Awake()
    {
        anim = this.GetComponent<Animator>();
        isFiring = true;


    }

    void Start ()
    {
        

        // Subscribe to event
        ColorChangingController.Instance.ToYellow += ChangeToYellow;
        ColorChangingController.Instance.ToCyan += ChangeToCyan;
        ColorChangingController.Instance.ToMagenta += ChangeToMagenta;

        
       
        myRigidbody = GetComponent<Rigidbody>();
        maincamera = FindObjectOfType<Camera>();
        weapon = GetComponentInChildren<WeaponController>();
        Fire = true;

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && Fire == true )
        {
            weapon.is_firing = true;
            isFiring = true;
            anim.SetBool("isFiring", isFiring);
            Fire = false;

        }

        if (Input.GetMouseButtonUp(0) )
        {
            weapon.is_firing = false;
            isFiring = false;
            anim.SetBool("isFiring", isFiring);
            Fire = true;
        }

        //if (Input.GetMouseButtonDown(0))
        //{
        //    sniper.is_firing = true;
        //    isFiring = true;
        //    anim.SetBool("isFiring", isFiring);
        //}


        //if (Input.GetMouseButtonUp(0))
        //{
        //    sniper.is_firing = false;
        //    isFiring = false;
        //    anim.SetBool("isFiring", isFiring);
        //}

        //if (Input.GetMouseButtonDown(0))
        //{
        //    escopeta.is_firing = true;
        //    isFiring = true;
        //    anim.SetBool("isFiring", isFiring);
        //}
        //if (Input.GetMouseButtonUp(0))
        //{
        //    escopeta.is_firing = false;
        //    isFiring = false;
        //    anim.SetBool("isFiring", isFiring);
        //}
    }
    void FixedUpdate ()
    {
       
    }


    ///////////////////////////////////////////////////////////





    // Color events

    public void ChangeToYellow()
    {
        foreach(Renderer r in renderersToChangeColor)
        {
            r.material = Yellow_Material;    // Apply player material
            Instantiate(Change_effectYellow.gameObject, transform.position, Quaternion.identity);
         //   Debug.Log("Change to yellow");
        }
        
        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
        // Yellow Layer
    }

    public void ChangeToCyan()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = Blue_Material;      // Apply player material
          //  Debug.Log("Change to cyan");
            Instantiate(Change_effectBlue.gameObject, transform.position, Quaternion.identity);
        }
       
        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1); // Cyan Layer
    }

    public void ChangeToMagenta()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = Pink_Material;      // Apply player material
          //  Debug.Log("Change to magenta");
            Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }
       
        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }

    public void RestoreChangeToYellow()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = Yellow_Material;    // Apply player material
            //Instantiate(Change_effectYellow.gameObject, transform.position, Quaternion.identity);
            //   Debug.Log("Change to yellow");
        }

        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
        // Yellow Layer
    }
    public void RestoreChangeToMagenta()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = Pink_Material;      // Apply player material
                                             //  Debug.Log("Change to magenta");
          //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }
    public void RestoreChangeToCyan()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = Blue_Material;      // Apply player material
                                             //  Debug.Log("Change to cyan");
            //Instantiate(Change_effectBlue.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1); // Cyan Layer
    }




    public void ChangeToDamageYellow()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = DamageYellowMaterial;      // Apply player material
                                             //  Debug.Log("Change to magenta");
          //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
    }
    public void ChangeToDamageBlue()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = DamageBlueMaterial;      // Apply player material
                                             //  Debug.Log("Change to magenta");
                                             //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1);
    }
    public void ChangeToDamagePink()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = DamagePinkMaterial;      // Apply player material
                                                  //  Debug.Log("Change to magenta");
                                                  //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }
    public void ChangeToHealthYellow()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = HealthYellowMaterial;      // Apply player material
                                                    //  Debug.Log("Change to magenta");
                                                    //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
    }
    public void ChangeToHealthBlue()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = HealthBlueMaterial;      // Apply player material
                                                    //  Debug.Log("Change to magenta");
                                                    //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1);
    }
    public void ChangeToHealthPink()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = HealthPinkMaterial;      // Apply player material
                                                    //  Debug.Log("Change to magenta");
                                                    //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }



    public void ChangeToBlackGlitchYellow()
    {
        //BlackGlitchBlueMaterial
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = BlackGlitchYellowMaterial;      // Apply player material
                                                  //  Debug.Log("Change to magenta");
                                                  //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
    }
    public void ChangeToBlackGlitchBlue()
    {
        //BlackGlitchBlueMaterial
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = BlackGlitchBlueMaterial;      // Apply player material
                                                         //  Debug.Log("Change to magenta");
                                                         //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1);
    }

    public void ChangeToBlackGlitchPink()
    {
        //BlackGlitchBlueMaterial
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = BlackGlitchPinkMaterial;      // Apply player material
                                                       //  Debug.Log("Change to magenta");
                                                       //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }

    public void ChangeToBlackYellow()
    {
        //BlackGlitchBlueMaterial
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = BlackYellowMaterial;      // Apply player material
                                                         //  Debug.Log("Change to magenta");
                                                         //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
    }
    public void ChangeToBlackBlue()
    {
        //BlackGlitchBlueMaterial
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = BlackBlueMaterial;      // Apply player material
                                                   //  Debug.Log("Change to magenta");
                                                   //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1);
    }
    public void ChangeToBlackPink()
    {
        //BlackGlitchBlueMaterial
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = BlackPinkMaterial;      // Apply player material
                                                 //  Debug.Log("Change to magenta");
                                                 //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }


    public void UpdateColor()
    {
        GetComponent<ColorChangingController>().ReColor();
    }

}
