using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    static GameplayManager instance;


    public Text t_health;               // Player's health in UI
    public float health;


    // HUD small colored squares
    //- Take these out to HUD
    public GameObject y_icon;
    public GameObject c_icon;
    public GameObject m_icon;


    // HUD screen transparency
    ////- Take these out to HUD
    public GameObject y_frame;
    public GameObject c_frame;
    public GameObject m_frame;

    // Use this for initialization
    void Start ()
    {
        instance = this;

        //y_icon.SetActive(true);
        //c_icon.SetActive(false);
        //m_icon.SetActive(false);

        //y_frame.SetActive(true);
        //c_frame.SetActive(false);
        //m_frame.SetActive(false);
    }


    public static GameplayManager GetInstance()
    {
        return instance;
    }

    // Update is called once per frame
    void Update ()
    {
    }

    void FixedUpdate()
    {
        t_health.text = "Health " + health;
        //vida.text = Vida.ToString();
        //textvida.text = "= " + Vida.ToString();
    }

    // Update accordingly to color
    public void ChangeColor(int color)
    {
        DeactivateAll();

        switch(color)
        {
            case 0:                     // Yellow Color
                y_icon.SetActive(true);
                y_frame.SetActive(true);
                break;
            case 1:                     // Cyan Color
                c_icon.SetActive(true);
                c_frame.SetActive(true);
                break;
            case 2:                     // Magenta Color
                m_icon.SetActive(true);
                m_frame.SetActive(true);
                break;
            default:
                break;
        }

    }

    void DeactivateAll()
    {
        y_icon.SetActive(false);
        c_icon.SetActive(false);
        m_icon.SetActive(false);

        y_frame.SetActive(false);
        c_frame.SetActive(false);
        m_frame.SetActive(false);
    }
}
