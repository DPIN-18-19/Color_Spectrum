using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoColor : MonoBehaviour
{
    public bool use_ability;        // Dash to be used checker
    public bool is_active;          // Ability activated checker

    public float c_cooldown;        // Cooldown counter
    private float cooldown;         // Cooldown to be applied

    public float dur;               // Ability Duration
    private float max_dur;          // Ability max duration
    private bool Init_Abi;

    public GameObject effect;
    
    public AudioClip FxHabilidad;
    AudioSource source;
    public ColorChangingController cambioColor;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start()
    {
        cooldown = c_cooldown;
        max_dur = dur;

        Init_Abi = true;
    }

    // Update is called once per frame
    void Update()
    {
        GameplayManager.GetInstance().ability_cooldown = c_cooldown;
        //GameplayManager.GetInstance().cambio_activo = is_active;
        //GameplayManager.GetInstance().usarhabilidad = use_ability;

        if (is_active == true && Input.GetButtonDown("Habilidad1") && Init_Abi == true)
        {
            source.PlayOneShot(FxHabilidad);
            dur = max_dur;
            use_ability = true;
            CreateEffect();
            Init_Abi = false;
            GameplayManager.GetInstance().ActivateAbility();
        }

        RefreshCooldown();
        MakeAutoColor();
    }

    // Make ability
    void MakeAutoColor()
    {
        if (use_ability)
        {
            dur -= Time.deltaTime;

            if (dur <= 0)
            {
                use_ability = false;
                is_active = false;
                c_cooldown = cooldown;
                //GameplayManager.GetInstance().CambioColor(is_active);
                GameplayManager.GetInstance().DeactivateAbility();

                Init_Abi = true;
                dur = max_dur;
            }
        }
    }

    // Update cooldown
    void RefreshCooldown()
    {
        c_cooldown -= Time.deltaTime;

        if (c_cooldown <= 0 && Init_Abi == true)
        {
            is_active = true;
            //GameplayManager.GetInstance().CambioColor(is_active);
            GameplayManager.GetInstance().ResetAbility();
            c_cooldown = 0;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (use_ability)
        {
            if (collision.gameObject.tag == "Pink" && collision.gameObject.layer != 16)
                cambioColor.SetColor(2);

            if (collision.gameObject.tag == "Blue" && collision.gameObject.layer != 16)
                cambioColor.SetColor(1);

            if (collision.gameObject.tag == "Yellow" && collision.gameObject.layer != 16)
                cambioColor.SetColor(0);
        }
    }

    void CreateEffect()
    {
        Quaternion rot = Quaternion.LookRotation(Vector3.up, Vector3.right);
        Instantiate(effect, transform.position - new Vector3(0, 1, 0), rot);
    }
}
