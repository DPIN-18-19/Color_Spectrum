using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadCambioColor : MonoBehaviour
{

    public ColorChangingController cambioColor;
    public bool UsarHabilidad;
    public bool SePuedeUsar;
    public float Cooldown;
    private float Max_Cooldown;
    public float DuracionHabilidad;
    private float Max_Duracion;
    public AudioClip FxHabilidad;
    AudioSource source;
    private bool Init_Abi;

    public GameObject effect;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start()
    {

        Max_Cooldown = Cooldown;
        Max_Duracion = DuracionHabilidad;

        Init_Abi = true;
    }

    // Update is called once per frame
    void Update()
    {

        Cooldown -= Time.deltaTime;
        GameplayManager.GetInstance().cambio_cooldown = Cooldown;
        GameplayManager.GetInstance().cambio_activo = SePuedeUsar;
        GameplayManager.GetInstance().usarhabilidad = UsarHabilidad;
        if (SePuedeUsar == true && Input.GetButtonDown("Habilidad1") && Init_Abi == true)
        {
            source.PlayOneShot(FxHabilidad);
            DuracionHabilidad = Max_Duracion;
            UsarHabilidad = true;
            CreateEffect();
            Init_Abi = false;
        }
        if (UsarHabilidad)
        {

            DuracionHabilidad -= Time.deltaTime;
            if (DuracionHabilidad <= 0)
            {
                UsarHabilidad = false;
                SePuedeUsar = false;
                Cooldown = Max_Cooldown;
                GameplayManager.GetInstance().CambioColor(SePuedeUsar);

                Init_Abi = true;
                DuracionHabilidad = Max_Duracion;
            }

        }
        if (Cooldown <= 0  && Init_Abi == true)
        {
            SePuedeUsar = true;
            GameplayManager.GetInstance().CambioColor(SePuedeUsar);
            Cooldown = 0;
        }
    }
    void OnCollisionEnter(Collision collision)
    {
        if (UsarHabilidad)
        {
            if (collision.gameObject.tag == "Pink" && collision.gameObject.layer != 16)
            {
                cambioColor.SetColor(2);

            }
            if (collision.gameObject.tag == "Blue" && collision.gameObject.layer != 16)
            {
                cambioColor.SetColor(1);

            }
            if (collision.gameObject.tag == "Yellow" && collision.gameObject.layer != 16)
            {
                cambioColor.SetColor(0);

            }
        }
    }
    void CreateEffect()
    {
        Quaternion rot = Quaternion.LookRotation(Vector3.up, Vector3.right);
        Instantiate(effect, transform.position - new Vector3(0, 1, 0), rot);
    }
}
