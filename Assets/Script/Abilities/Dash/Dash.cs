using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{
    public float dash_speed;        // Dash movement speed
    public float c_cooldown;        // Cooldown counter
    private float cooldown;         // Cooldown to be applied

    public bool use_ability;        // Dash to be used checker
    public bool is_active;          // Ability activated checker

    // Components
    public PlayerJaneMoveController player;
    public Rigidbody rb;
    public AudioClip dash_fx;
    private AudioSource source;

    // Particles
    public GameObject p_sys;


    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        cooldown = c_cooldown;
        rb = GetComponent<Rigidbody>();

        //GameplayManager.GetInstance().dash_cooldown = c_cooldown;
        //GameplayManager.GetInstance().dash_activo = is_active;
        //Max_Duracion = DuracionHabilidad;
    }

    // Update is called once per frame
    void Update()
    {
        // Update gameplay manager
        GameplayManager.GetInstance().ability_cooldown = c_cooldown;
        //GameplayManager.GetInstance().dash_activo = is_active;
        
        if (Input.GetButton("Dash") && c_cooldown <= 0)
        {
            use_ability = true; // This variable may be erased
            // MakeDash();
        }

        RefreshCooldown();
        MakeDash();

    }

    // Make dash movement
    void MakeDash()
    {
        if (use_ability)
        {
            source.PlayOneShot(dash_fx);
            rb.AddForce(player.move_dir * (dash_speed * 100), ForceMode.Impulse);
            // Dash particles
            Invoke("MakeEffect", 0.02f);

            use_ability = false;
            is_active = false;
            //GameplayManager.GetInstance().Dash(is_active);
            GameplayManager.GetInstance().DeactivateAbility();
            c_cooldown = cooldown;
        }
    }

    // Update cooldown
    void RefreshCooldown()
    {
        c_cooldown -= Time.deltaTime;

        if (c_cooldown <= 0)
        {
            is_active = true;
            //GameplayManager.GetInstance().Dash(is_active);
            GameplayManager.GetInstance().ResetAbility();
            c_cooldown = 0;
        }
    }

    // Make dash effects
    void MakeEffect()
    {
        Quaternion rot = Quaternion.LookRotation(-player.move_dir, Vector3.up);
        Instantiate(p_sys, transform.position, rot);
    }
}