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
   
    public int cur_color;
    
    Collider m_collider;

    //Renderers to change
    [SerializeField]
    public List<Renderer> renderersToChangeColor;
    
    //Player's material

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
        if (Input.GetButtonDown("Fire1") && Fire == true )
        {
            weapon.is_firing = true;
            isFiring = true;
            anim.SetBool("isFiring", isFiring);
            Fire = false;
        }

        if (Input.GetButtonUp("Fire1") )
        {
            weapon.is_firing = false;
            isFiring = false;
            anim.SetBool("isFiring", isFiring);
            Fire = true;
        }
    }

    void FixedUpdate ()
    {
    }

    ///////////////////////////////////////////////////////////
    // Color events

    public void ChangeToYellow()
    {
        Instantiate(Change_effectYellow.gameObject, transform.position, Quaternion.identity);
        cur_color = 0;
        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
        // Yellow Layer
    }

    public void ChangeToCyan()
    {
        Instantiate(Change_effectBlue.gameObject, transform.position, Quaternion.identity);
        cur_color = 1;
        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1); // Cyan Layer
    }

    public void ChangeToMagenta()
    {
        Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        cur_color = 2;
        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }

    public void UpdateColor()
    {
        GetComponent<ColorChangingController>().ReColor();
    }
}