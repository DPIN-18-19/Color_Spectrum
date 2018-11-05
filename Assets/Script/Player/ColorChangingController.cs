﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorChangingController : MonoBehaviour {

    public enum Colors
    {
        Yellow,
        Cyan,
        Magenta
    };

    public Colors cur_color;
    int i_cur_color;
    private int num_colors;
    public Colors init_color = Colors.Yellow;

    bool do_update;

    // Events
    public delegate void ColorChange();
    public event ColorChange ToYellow;
    public event ColorChange ToCyan;
    public event ColorChange ToMagenta;



    public static ColorChangingController Instance
    {
        get;
        private set;
    }

    public void Awake()
    {
        Instance = this;
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
            do_update = true;
        }

        // Change Colors backwards
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i_cur_color = (int)cur_color;
            i_cur_color = Loop(i_cur_color - 1, num_colors);
            cur_color = (Colors)i_cur_color;
            do_update = true;
        }
    }

    void UpdateColor()
    {
        if (do_update)
        {
            do_update = false;

            switch (cur_color)
            {
                case Colors.Yellow:
                    if(ToYellow != null)
                        ToYellow();
                    break;
                case Colors.Cyan:
                    if(ToCyan != null)
                        ToCyan();
                    break;
                case Colors.Magenta:
                    if(ToMagenta != null)
                        ToMagenta();
                    break;
                default:
                    Debug.Log("Color event error");
                    break;
            }
        }
    }

    int Loop(int t, int length)
    {
        if (t >= length)
            return 0;
        else if (t < 0)
            return length-1;
        else
            return t;
    }
}
