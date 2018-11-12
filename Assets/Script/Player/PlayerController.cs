using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {


    //public Text vida;               // Player's health in UI
    //public float Vida;              // Player's current health
    //public float VidaQuitada;       // Player's received damage per attack
    // float vida maxima
    

    //public float MoveSpeed;         // PLayer's speed
    private Rigidbody myRigidbody;
   
    //private Vector3 moveInput;      // Player direction. Input dependent
    //private Vector3 moveVelocity;   // Player new vector speed
    
    private Camera maincamera;      // Player Camera

    public GunController theGun;    // Player's Gun

    Collider m_collider;
    private Renderer renderPlayer;

    // HUD small colored squares
    //- Take these out to HUD
    public GameObject YellowNormal;
    public GameObject BlueNormal;

    
    public GameObject PinkNormal;

    // Death particles
    //public ParticleSystem DieEffectYellow;
    //public ParticleSystem DieEffectBlue;
    //public ParticleSystem DieEffectPink;


    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;
    





    // HUD screen transparency
    ////- Take these out to HUD
    //public GameObject HUDAmarillo;
    //public GameObject HUDRosa;
    //public GameObject HUDAzul;



    



    // Use this for initialization
    void Awake()
    {

    }

    void Start ()
    {
        // Subscribe to event
        ColorChangingController.Instance.ToYellow += ChangeToYellow;
        ColorChangingController.Instance.ToCyan += ChangeToCyan;
        ColorChangingController.Instance.ToMagenta += ChangeToMagenta;

        //DieEffectYellow.Stop();
        //DieEffectBlue.Stop();
        //DieEffectPink.Stop();
        //gameObject.layer = 8;           // Player's color
        //YellowNormal.SetActive(true);
        //BlueNormal.SetActive(false);
        //PinkNormal.SetActive(false);

        //HUDAmarillo.SetActive(true);
        //HUDRosa.SetActive(false);
        //HUDAzul.SetActive(false);

        renderPlayer = GetComponent<Renderer>();
        myRigidbody = GetComponent<Rigidbody>();
        maincamera = FindObjectOfType<Camera>();
        //m_collider = GetComponent<Collider>();



       // dashTime = startDashTime;

    }
  
	// Update is called once per frame
	void Update () {
        // Debug.Log(Vida);
       
        // Death code
        //if (Vida <= 0)
        //{
        //   // if(theGun.BulletYellow)
        //   // {
        //   //     Instantiate(DieEffectYellow.gameObject, transform.position, Quaternion.identity);
        //   //     Vida  = 0;
        //   //     Destroy(gameObject);
        //   //     // Destroy Particles when finished
        //   // }
        //   //if(theGun.BulletBlue)
        //   // {
        //   //     Instantiate(DieEffectBlue.gameObject, transform.position, Quaternion.identity);
        //   //     Destroy(gameObject);
        //   //     Vida = 0;
        //   // }
        //   //if (theGun.BulletPink)
        //   // {
        //   //     Instantiate(DieEffectPink.gameObject, transform.position, Quaternion.identity);
        //   //     Destroy(gameObject);
        //   //     Vida = 0;
        //   // }
        //}

        // Health maximum limit
        // Make variable from this
        //if(Vida > 50)
        //{
        //    Vida = 50;
        //}

        ///////////////////////////////////////////////////

        // Player changes to yellow
        //if (theGun.BulletYellow)
        //{
        //    gameObject.layer = 8;
           
        //    GetComponent<Renderer>().material = Yellow_Material;


        //    YellowNormal.SetActive(true);
        //    BlueNormal.SetActive(false);
        //    PinkNormal.SetActive(false);

        //    HUDAmarillo.SetActive(true);
        //    HUDRosa.SetActive(false);
        //    HUDAzul.SetActive(false);


        //}
        //// Player changes to blue
        //if (theGun.BulletBlue)
        //{
        //    gameObject.layer = 9;
            
        //    GetComponent<Renderer>().material = Blue_Material;
        //    YellowNormal.SetActive(false);
        //    BlueNormal.SetActive(true);
        //    PinkNormal.SetActive(false);

        //    HUDAmarillo.SetActive(false);
        //    HUDRosa.SetActive(false);
        //    HUDAzul.SetActive(true);
        //}
        //// Player changes to pink
        //if (theGun.BulletPink)
        //{
        //    gameObject.layer = 10;
            
        //    GetComponent<Renderer>().material = Pink_Material;
        //    YellowNormal.SetActive(false);
        //    BlueNormal.SetActive(false);
        //    PinkNormal.SetActive(true);

        //    HUDAmarillo.SetActive(false);
        //    HUDRosa.SetActive(true);
        //    HUDAzul.SetActive(false);
        //}

        // Apply movement to player
        //moveInput = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        //moveVelocity = moveInput * MoveSpeed;
        // Fix diagonal speed

        ///////////////////////////////////////

        // Player Rotation
        //Ray cameraRay = maincamera.ScreenPointToRay(Input.mousePosition);
        //Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        //float rayLeght;

        //if(groundPlane.Raycast(cameraRay, out rayLeght))
        //{
        //    Vector3 pointToLook = cameraRay.GetPoint(rayLeght);
        //    Debug.DrawLine(cameraRay.origin, pointToLook, Color.blue);
        //    transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        //}

        ////////////////////////////////////////

        // Firing automatic weapon
        //- Move this to GunController?
        if(Input.GetMouseButtonDown(0))
         theGun.is_firing = true;
        if (Input.GetMouseButtonUp(0))
            theGun.is_firing = false;
        //Codigo del Dash
        
            //Instantiate(DashEffectYellow.gameObject, transform.position, Quaternion.identity);
           // moveInput = new Vector3(Input.GetAxisRaw("Horizontal") * dashspeed, 0f, Input.GetAxisRaw("Vertical") * dashspeed);
           // dashTime = startDashTime;
        
        //Fin codigo del dash
    }
    void FixedUpdate ()
    {
        //vida.text = Vida.ToString();
        // textvida.text = "= " + Vida.ToString();
        //myRigidbody.velocity = moveVelocity;
    }


    ///////////////////////////////////////////////////////////

    // Color events

    void ChangeToYellow()
    {
        gameObject.layer = 8;                       // Yellow Layer
        renderPlayer.material = Yellow_Material;    // Apply player material
        Debug.Log("Change to yellow");
    }

    void ChangeToCyan()
    {
        gameObject.layer = 9;                       // Cyan Layer
        renderPlayer.material = Blue_Material;      // Apply player material
        Debug.Log("Change to cyan");
    }

    void ChangeToMagenta()
    {
        gameObject.layer = 10;                      // Magenta Layer
        renderPlayer.material = Pink_Material;      // Apply player material
        Debug.Log("Change to magenta");
    }


    


    private void OnColliderEnter(Collision collision)
    {
        //Codigo Cambio de Color automatico
      //   if (collision.gameObject.tag == "Yellow" && collision.gameObject.layer == 8 && theGun.BulletBlue == true)
       // {
       //     theGun.BulletYellow = true;
       //     Debug.Log("Auto");
       // }
    }

    //public void HacerDaño()
    //{
    //    Vida = Vida - VidaQuitada;
    //    Debug.Log(Vida);
    //}

    //public void RecibeVida()
    //{
    //    Vida = Vida + VidaQuitada;
    //    Debug.Log(Vida);

    //}


    //private void OnColliderEnter(Collider col)
    //{
        
    //    // if (collision.gameObject.tag == "Pink" || collision.gameObject.tag == "Blue" || collision.gameObject.tag == "Yellow" )
    //    //{
    //    //  m_collider.enabled = !m_collider.enabled;
    //    //}
    //}
    
}
