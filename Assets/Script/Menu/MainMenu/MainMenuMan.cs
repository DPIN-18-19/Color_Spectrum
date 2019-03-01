using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuMan : MonoBehaviour
{
    public static MainMenuMan Instance { get; private set; }

    Transform mainbuttons_p;
    Transform controls_p;
    Transform blackribbon;
    Transform logo;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Use this for initialization
    void Start ()
    {
        mainbuttons_p = transform.Find("MainButtons_p");
        controls_p = transform.Find("Controles_Texto");
        blackribbon = transform.Find("Banda Negra");
        logo = transform.Find("Logotipo");

        controls_p.GetComponent<CanvasGroup>().alpha = 0;
    }

    private void Update()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    // Update is called once per frame
    public void Controls()
    {
        mainbuttons_p.GetComponent<CanvasGroup>().alpha = 0;
        controls_p.GetComponent<CanvasGroup>().alpha = 1;
        blackribbon.GetComponent<CanvasGroup>().alpha = 0;
        logo.GetComponent<CanvasGroup>().alpha = 0;

    }
    public void MainMenu()
    {
        mainbuttons_p.GetComponent<CanvasGroup>().alpha = 1;
        controls_p.GetComponent<CanvasGroup>().alpha = 0;
        blackribbon.GetComponent<CanvasGroup>().alpha = 1;
        logo.GetComponent<CanvasGroup>().alpha = 1;
    }
}
