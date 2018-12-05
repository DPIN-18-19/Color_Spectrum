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
   
    
    
    private Camera maincamera;      // Player Camera


    public Escopeta pistola;
    public Escopeta sniper;
    public Escopeta escopeta;

    public GunController theGun;    // Player's Gun
    
    Collider m_collider;

    //Renderers to change
    [SerializeField]
    List<Renderer> renderersToChangeColor;
   

    // Player's material
    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;

    
    
  
    //////////////////////////////////////////////////////////////////////////////

    // Use this for initialization
    void Awake()
    {
        anim = this.GetComponent<Animator>();
      
    }

    void Start ()
    {
        

        // Subscribe to event
        ColorChangingController.Instance.ToYellow += ChangeToYellow;
        ColorChangingController.Instance.ToCyan += ChangeToCyan;
        ColorChangingController.Instance.ToMagenta += ChangeToMagenta;

        
       
        myRigidbody = GetComponent<Rigidbody>();
        maincamera = FindObjectOfType<Camera>();
        

    }
  
	// Update is called once per frame
	void Update () {
        

        if (Input.GetMouseButtonDown(0))
        {
            pistola.is_firing = true;
            isFiring = true;
            anim.SetBool("isFiring", isFiring);

        }
        if (Input.GetMouseButtonUp(0))
        {
            pistola.is_firing = false;
            isFiring = false;
            anim.SetBool("isFiring", isFiring);
        }

        if (Input.GetMouseButtonDown(0))
        {
            sniper.is_firing = true;
            isFiring = true;
            anim.SetBool("isFiring", isFiring);
        }


        if (Input.GetMouseButtonUp(0))
        {
            sniper.is_firing = false;
            isFiring = false;
            anim.SetBool("isFiring", isFiring);
        }

        if (Input.GetMouseButtonDown(0))
        {
            escopeta.is_firing = true;
            isFiring = true;
            anim.SetBool("isFiring", isFiring);
        }
        if (Input.GetMouseButtonUp(0))
        {
            escopeta.is_firing = false;
            isFiring = false;
            anim.SetBool("isFiring", isFiring);
        }



    }
    void FixedUpdate ()
    {
       
    }


    ///////////////////////////////////////////////////////////





    // Color events

    void ChangeToYellow()
    {
        foreach(Renderer r in renderersToChangeColor)
        {
            r.material = Yellow_Material;    // Apply player material
            Instantiate(Change_effectYellow.gameObject, transform.position, Quaternion.identity);
        }
        
        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
        // Yellow Layer
    }

    void ChangeToCyan()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = Blue_Material;      // Apply player material
            Instantiate(Change_effectBlue.gameObject, transform.position, Quaternion.identity);
        }
       
        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1); // Cyan Layer
    }

    void ChangeToMagenta()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = Pink_Material;      // Apply player material
            Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }
       
        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }




    
    
}
