using System.Collections;
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

	// Use this for initialization
	void Start ()
    {
        num_colors = System.Enum.GetNames(typeof(Colors)).Length;

    }
	
	// Update is called once per frame
	void Update ()
    {
        ChangeColor();

    }

    void ChangeColor()
    {
        // Change colors forward
        if(Input.GetKeyDown(KeyCode.E))
        {
            i_cur_color = (int)cur_color;
            i_cur_color = Loop(i_cur_color+1, num_colors);
            cur_color = (Colors)i_cur_color;
        }

        // Change Colors backwards
        if (Input.GetKeyDown(KeyCode.Q))
        {
            i_cur_color = (int)cur_color;
            i_cur_color = Loop(i_cur_color - 1, num_colors);
            cur_color = (Colors)i_cur_color;
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
