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


    public bool ParedCambioNo;
    public SameColorPink ParedRosa;
    public float DuracionMismoColor;
    public float MaxDuracion;
    
    public Colors cur_color;                    // Current selected color
    public int i_cur_color;                            // Current selected color (number)
    private int num_colors;                     // Number of colors

    public Colors init_color = Colors.Yellow;   // Initial color

    bool do_update;                             // Has color changed?

    public AudioClip FxCambioColor;
    AudioSource source;

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
        Instance = this;
        source = GetComponent<AudioSource>();
    }

    // Use this for initialization
    void Start ()
    {
        num_colors = System.Enum.GetNames(typeof(Colors)).Length;
        do_update = true;
    }
	
	// Update is called once per frame
	void Update ()
    {
        ChangeColor();
        UpdateColor();
    }

    void ChangeColor()
    {
        // Change colors forward
        if(Input.GetKeyDown(KeyCode.E))
        {
            if (ParedCambioNo == false){

                i_cur_color = (int)cur_color;
                i_cur_color = Loop(i_cur_color + 1, num_colors);
                cur_color = (Colors)i_cur_color;
                do_update = true;
                source.PlayOneShot(FxCambioColor);
            }
        }

        // Change Colors backwards
        if (Input.GetKeyDown(KeyCode.Q))
        {
            if (ParedCambioNo == false){

                i_cur_color = (int)cur_color;
            i_cur_color = Loop(i_cur_color - 1, num_colors);
            cur_color = (Colors)i_cur_color;
            do_update = true;
                source.PlayOneShot(FxCambioColor);
            }
        }
        if(ParedCambioNo == true)
        {
            DuracionMismoColor += Time.deltaTime;
            if(DuracionMismoColor > MaxDuracion)
            {
               ParedCambioNo = false;
               DuracionMismoColor = 0;
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
        if(col.gameObject.tag == "ParedNoCambioRosa" && this.gameObject.layer == 10 )
        {
            
                ParedCambioNo = true;
                Debug.Log("NoPasarRosa");
            
        }
        if (col.gameObject.tag == "ParedNoCambioAzul" && this.gameObject.layer == 9 )
        {
            
                ParedCambioNo = true;
                Debug.Log("NoPasarAzul");
            
        }
        if (col.gameObject.tag == "ParedNoCambioYellow" && this.gameObject.layer == 8 )
        {
            
                ParedCambioNo = true;
                Debug.Log("NoPasarYellow");
            
        }
    }
    public void ReColor()
    {
        do_update = true;
    }
}
