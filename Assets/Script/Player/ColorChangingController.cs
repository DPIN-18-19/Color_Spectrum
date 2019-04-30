using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingController : MonoBehaviour {

    public enum Colors
    {
        Yellow,         // Yellow = 0
        Cyan,           // Cyan = 1
        Magenta         // Magenta = 2
    };
    
    private PlayerRenderer MaterialsPlayer;
    HealthController health;

    public Colors cur_color;                    // Current selected color
    int i_cur_color;                            // Current selected color (number)
    private int num_colors;                     // Number of colors

    public Colors init_color = Colors.Yellow;   // Initial color

    bool do_update;                             // Has color changed?

    public AudioClip FxCambioColor;
    public float VolumeFxCambioColor = 1;
    public AudioClip FxNoChangeColor;
    public float VolumeFxNoChangeColor = 1;
    AudioSource source;

    // Variables de cambio de color
    [HideInInspector]
    public bool same_color;                     // Estado "MismoColor"
    float same_color_c = 0;                  // Contador en estado "MismoColor"
    public float same_color_dur;                // Duracion de "MismoColor"
    public float same_color_glitch_dur;         // Duracion de primera fase de "Mismo Color"
    private bool DoOnce = true;

    //////////////////////////////////////////////////////////////////////////////

    // Events
    public delegate void ColorChange();
    public event ColorChange ToYellow;
    public event ColorChange ToCyan;
    public event ColorChange ToMagenta;

    //////////////////////////////////////////////////////////////////////////////

    public static ColorChangingController Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
           MaterialsPlayer = GetComponent<PlayerRenderer>();
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        health = GetComponent<HealthController>();

        num_colors = System.Enum.GetNames(typeof(Colors)).Length;
        do_update = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ChangeColor();
        UpdateColor();
       // Debug.Log(GetColor());
    }

    void ChangeColor()
    {
        // Change colors forward
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (same_color == false){

                i_cur_color = (int)cur_color;
                i_cur_color = Loop(i_cur_color + 1, num_colors);
                cur_color = (Colors)i_cur_color;
                do_update = true;
                source.PlayOneShot(FxCambioColor, VolumeFxCambioColor);
            }
        }

        // Change Colors backwards
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (same_color == false){
               
                i_cur_color = (int)cur_color;
                i_cur_color = Loop(i_cur_color - 1, num_colors);
                cur_color = (Colors)i_cur_color;
                do_update = true;
                source.PlayOneShot(FxCambioColor, VolumeFxNoChangeColor);
            }
        }

        if(same_color)
        {
            //if (DoOnce)
            //{

            //    DoOnce = false;
            //}


            //////////


            Debug.Log("Contando NoCambioColor ");
            same_color_c += Time.deltaTime;

            // Estado "Glitch" al inicio de pared
            if (same_color_c < same_color_glitch_dur)
            {
                Debug.Log("Hello");
                MaterialsPlayer.BlackGlitchColor();
            }
            else
            {
                if(health.Daño)
                    MaterialsPlayer.DamageColor();
                else
                    MaterialsPlayer.BlackColor();
            }
            
            // Ejecutar al terminar tiempo de "Negro"
            if(same_color_c > same_color_dur)
            {
                Debug.Log("NoCambioColor Vuelta");
                same_color = false;
                same_color_c = 0;
                MaterialsPlayer.ResetColor();
                source.PlayOneShot(FxNoChangeColor);
            }
        }
    }

    // If color was changed, send event
    void UpdateColor()
    {
        if (do_update)
        {
            do_update = false;

            switch (cur_color)
            {
                case Colors.Yellow:
                    if (ToYellow != null)
                    {
                        ToYellow();
                       
                    }

                    break;
                case Colors.Cyan:
                    if (ToCyan != null)
                    {
                        ToCyan();
                        //source.PlayOneShot(FxCambioColor);
                    }
                    break;
                case Colors.Magenta:
                    if (ToMagenta != null)
                    {
                        ToMagenta();
                      //  source.PlayOneShot(FxCambioColor);
                    }
                    break;
                default:
                    Debug.Log("Color event error");
                    break;
            }
        }
    }

    // Loop between values
    // Mathf.Repeat
    int Loop(int t, int length)
    {
        if (t >= length)
            return 0;
        else if (t < 0)
            return length-1;
        else
            return t;
    }

    // Get current color int value
    public int GetColor()
    {
        return i_cur_color;
    }
    public void SetColor(int N_Color)
    {
        if (N_Color >= 0 && N_Color <= num_colors)
        {
            i_cur_color = N_Color;
            cur_color = (Colors)i_cur_color;
            do_update = true;
        }
    }
    private void OnTriggerStay(Collider col)
    {
        if(col.gameObject.tag == "ParedNoCambioPink" && this.gameObject.layer == 10 )
        {
            same_color = true;
            Debug.Log("NoPasarRosa");
            
        }
        if (col.gameObject.tag == "ParedNoCambioBlue" && this.gameObject.layer == 9 )
        {

            same_color = true;
            Debug.Log("NoPasarAzul");
            
        }
        if (col.gameObject.tag == "ParedNoCambioYellow" && this.gameObject.layer == 8 )
        {

            same_color = true;
            Debug.Log("NoPasarYellow");
            
        }
    }
    public void ReColor()
    {
        do_update = true;
    }
}
