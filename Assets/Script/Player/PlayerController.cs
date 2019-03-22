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
    //public Material Blue_Material;
    //public Material Yellow_Material;
    //public Material Pink_Material;

    //public Material DamageBlueMaterial;
    //public Material DamageYellowMaterial;
    //public Material DamagePinkMaterial;

    //public Material HealthBlueMaterial;
    //public Material HealthYellowMaterial;
    //public Material HealthPinkMaterial;

    //public Material BlackGlitchBlueMaterial;
    //public Material BlackGlitchYellowMaterial;
    //public Material BlackGlitchPinkMaterial;

    //public Material BlackBlueMaterial;
    //public Material BlackYellowMaterial;
    //public Material BlackPinkMaterial;

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
        //foreach (Renderer r in renderersToChangeColor)
        //{
        //    r.material = Yellow_Material;    // Apply player material
        //    Instantiate(Change_effectYellow.gameObject, transform.position, Quaternion.identity);
        //}
        Instantiate(Change_effectYellow.gameObject, transform.position, Quaternion.identity);
        cur_color = 0;
        gameObject.layer = 8;
        GameplayManager.GetInstance().ChangeColor(0);
        // Yellow Layer
    }

    public void ChangeToCyan()
    {
        //foreach (Renderer r in renderersToChangeColor)
        //{
        //    r.material = Blue_Material;      // Apply player material
        //    Instantiate(Change_effectBlue.gameObject, transform.position, Quaternion.identity);
        //}
        Instantiate(Change_effectBlue.gameObject, transform.position, Quaternion.identity);
        cur_color = 1;
        gameObject.layer = 9;
        GameplayManager.GetInstance().ChangeColor(1); // Cyan Layer
    }

    public void ChangeToMagenta()
    {
        //foreach (Renderer r in renderersToChangeColor)
        //{
        //    // Magenta Layer
        //    r.material = Pink_Material;      // Apply player material
        //    Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        //}
        Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        cur_color = 2;
        gameObject.layer = 10;
        GameplayManager.GetInstance().ChangeColor(2);
    }

    //// Jaime functions
    //// Reset Colors
    //public void ResetColor()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        switch (cur_color)
    //        {
    //            case 0:
    //                r.material = Yellow_Material;    // Apply player material
    //                break;
    //            case 1:
    //                r.material = Blue_Material;    // Apply player material
    //                break;
    //            case 2:
    //                r.material = Pink_Material;    // Apply player material
    //                break;
    //        }
    //    }
    //}

    //public void RestoreChangeToYellow()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        r.material = Yellow_Material;    // Apply player material
    //    }

    //    gameObject.layer = 8;
    //    GameplayManager.GetInstance().ChangeColor(0);   // Yellow Layer
    //}

    //public void RestoreChangeToCyan()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        r.material = Blue_Material;      // Apply player material
    //    }

    //    gameObject.layer = 9;
    //    GameplayManager.GetInstance().ChangeColor(1); // Cyan Layer
    //}

    //public void RestoreChangeToMagenta()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = Pink_Material;      // Apply player material
    //    }

    //    gameObject.layer = 10;
    //    GameplayManager.GetInstance().ChangeColor(2);
    //}

    //// Damage
    //public void DamageColor()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        switch (cur_color)
    //        {
    //            case 0:
    //                r.material = DamageYellowMaterial;    // Apply player material
    //                break;
    //            case 1:
    //                r.material = DamageBlueMaterial;    // Apply player material
    //                break;
    //            case 2:
    //                r.material = DamagePinkMaterial;    // Apply player material
    //                break;
    //        }
    //    }
    //}

    //public void ChangeToDamageYellow()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = DamageYellowMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 8;
    //    GameplayManager.GetInstance().ChangeColor(0);
    //}

    //public void ChangeToDamageBlue()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = DamageBlueMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 9;
    //    GameplayManager.GetInstance().ChangeColor(1);
    //}

    //public void ChangeToDamagePink()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = DamagePinkMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 10;
    //    GameplayManager.GetInstance().ChangeColor(2);
    //}

    //// Health Material


    //public void ChangeToHealthYellow()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = HealthYellowMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 8;
    //    GameplayManager.GetInstance().ChangeColor(0);
    //}

    //public void ChangeToHealthBlue()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = HealthBlueMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 9;
    //    GameplayManager.GetInstance().ChangeColor(1);
    //}

    //public void ChangeToHealthPink()
    //{
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = HealthPinkMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 10;
    //    GameplayManager.GetInstance().ChangeColor(2);
    //}

    //// Black Material Glitch
    //public void ChangeToBlackGlitchYellow()
    //{
    //    //BlackGlitchBlueMaterial
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = BlackGlitchYellowMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 8;
    //    GameplayManager.GetInstance().ChangeColor(0);
    //}

    //public void ChangeToBlackGlitchBlue()
    //{
    //    //BlackGlitchBlueMaterial
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = BlackGlitchBlueMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 9;
    //    GameplayManager.GetInstance().ChangeColor(1);
    //}

    //public void ChangeToBlackGlitchPink()
    //{
    //    //BlackGlitchBlueMaterial
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = BlackGlitchPinkMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 10;
    //    GameplayManager.GetInstance().ChangeColor(2);
    //}

    //// Black material
    //public void ChangeToBlackYellow()
    //{
    //    //BlackGlitchBlueMaterial
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = BlackYellowMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 8;
    //    GameplayManager.GetInstance().ChangeColor(0);
    //}

    //public void ChangeToBlackBlue()
    //{
    //    //BlackGlitchBlueMaterial
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = BlackBlueMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 9;
    //    GameplayManager.GetInstance().ChangeColor(1);
    //}

    //public void ChangeToBlackPink()
    //{
    //    //BlackGlitchBlueMaterial
    //    foreach (Renderer r in renderersToChangeColor)
    //    {
    //        // Magenta Layer
    //        r.material = BlackPinkMaterial;      // Apply player material
    //    }

    //    gameObject.layer = 10;
    //    GameplayManager.GetInstance().ChangeColor(2);
    //}

    public void UpdateColor()
    {
        GetComponent<ColorChangingController>().ReColor();
    }
}