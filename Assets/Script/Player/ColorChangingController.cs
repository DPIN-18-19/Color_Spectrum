


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

    public Colors cur_color;                    // Current selected color
    int i_cur_color;                            // Current selected color (number)
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
            i_cur_color = (int)cur_color;
            i_cur_color = Loop(i_cur_color+1, num_colors);
            cur_color = (Colors)i_cur_color;
            source.PlayOneShot(FxCambioColor);
            do_update = true;
        }

        // Change Colors backwards
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i_cur_color = (int)cur_color;
            i_cur_color = Loop(i_cur_color - 1, num_colors);
            cur_color = (Colors)i_cur_color;
            source.PlayOneShot(FxCambioColor);
            do_update = true;
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

    public void ReColor()
    {
        do_update = true;
    }
}
