using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {

    Animator anim;

  public bool isFiring;

    //public float MoveSpeed;         // PLayer's speed
    private Rigidbody myRigidbody;
   
    
    
    private Camera maincamera;      // Player Camera

    public GunController theGun;    // Player's Gun
    
    Collider m_collider;

    //Renderers to change
    [SerializeField]
    List<Renderer> renderersToChangeColor;
   

    // Player's material
    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;

    public Material[] cara;
    
  
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

        cara = GetComponent<SkinnedMeshRenderer>().materials;
       
        myRigidbody = GetComponent<Rigidbody>();
        maincamera = FindObjectOfType<Camera>();
        

    }
  
	// Update is called once per frame
	void Update () {
        

        if (Input.GetMouseButtonDown(0))
        {
            theGun.is_firing = true;
            isFiring = true;
            anim.SetBool("isFiring", isFiring);
        }
        if (Input.GetMouseButtonUp(0))
        {
            theGun.is_firing = false;
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
            
            Debug.Log("Change to yellow");
        }
        for ( int i =0; i < cara.Length; i++)
        {
            cara[i] = Yellow_Material;
        }
        gameObject.layer = 8;                       // Yellow Layer
    }

    void ChangeToCyan()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = Blue_Material;      // Apply player material
            Debug.Log("Change to cyan");
        }
        for (int i = 0; i < cara.Length; i++)
        {
            cara[i] = Blue_Material;
        }
        gameObject.layer = 9;                       // Cyan Layer
    }

    void ChangeToMagenta()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = Pink_Material;      // Apply player material
            Debug.Log("Change to magenta");
        }
        for (int i = 0; i < cara.Length; i++)
        {
            cara[i] = Pink_Material;
        }
        gameObject.layer = 10;
    }


    
    
}
