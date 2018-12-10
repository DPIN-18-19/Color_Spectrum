using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HabilidadCambioColor : MonoBehaviour {

    public ColorChangingController cambioColor;
    public bool UsarHabilidad;
    public bool SePuedeUsar;
    public float Cooldown;
    private float Max_Cooldown;
    public float DuracionHabilidad;
    private float Max_Duracion;
    public AudioClip FxHabilidad;
     AudioSource source;

    public GameObject effect;
    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
        // Use this for initialization
        void Start () {
       
        Max_Cooldown = Cooldown;
        Max_Duracion = DuracionHabilidad;

    }
	
	// Update is called once per frame
	void Update () {
        
        Cooldown -= Time.deltaTime;
        GameplayManager.GetInstance().cambio_cooldown = Cooldown;
        GameplayManager.GetInstance().cambio_activo = SePuedeUsar;
        GameplayManager.GetInstance().usarhabilidad = UsarHabilidad;
        if (Cooldown <= 0 &&/* DuracionHabilidad == Max_Duracion*/ SePuedeUsar == true && Input.GetButtonDown("Habilidad1"))
        {
            source.PlayOneShot(FxHabilidad);
            DuracionHabilidad = Max_Duracion;
            UsarHabilidad = true;
            CreateEffect();
        }
        if (UsarHabilidad)
        {
           
            DuracionHabilidad -= Time.deltaTime;
            if (DuracionHabilidad <= 0)
            {
                UsarHabilidad = false;
                SePuedeUsar = false;
                GameplayManager.GetInstance().CambioColor(SePuedeUsar);
               
                Cooldown = Max_Cooldown;
                DuracionHabilidad = Max_Duracion;
            }

        }
        if (Cooldown <= 0)
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
            if (collision.gameObject.tag == "Pink" )
            {
                cambioColor.SetColor(2);

            }
            if (collision.gameObject.tag == "Blue" )
            {
                cambioColor.SetColor(1);

            }
            if (collision.gameObject.tag == "Yellow" )
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
