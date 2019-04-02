using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseButtons : MonoBehaviour
{
    Transform resume_b, menu_b, controls_b, mainpause_b;

    // Use this for initialization
    void Start()
    {
        // Buscar botones en la escena
        resume_b = transform.Find("Resume_b");
        menu_b = transform.Find("Menu_b");
        controls_b = transform.Find("Controls_b");
        mainpause_b = transform.Find("MainPause_b");

        // Comprobar si cada botón fue encontrado
        if (resume_b)
            resume_b.GetComponent<Button>().onClick.AddListener(ToResume);

        if (menu_b)
            menu_b.GetComponent<Button>().onClick.AddListener(ToMenu);

        if (controls_b)
            controls_b.GetComponent<Button>().onClick.AddListener(ToControls);

        if (mainpause_b)
            mainpause_b.GetComponent<Button>().onClick.AddListener(ToMainPause);
    }

    void ToResume()
    {
        //SceneMan1.Instance.LoadSceneByName("PreparationMenu");
        PauseMan.Instance.Resume();
    }
    void ToMenu()
    {
        SceneMan1.Instance.LoadSceneByName("LevelSelection");
    }
    void ToControls()
    {
        //SceneMan1.Instance.LoadSceneByName("Store");
        PauseMan.Instance.Controls();
    }
    void ToMainPause()
    {
        //SceneMan1.Instance.LoadSceneByName("Store");
        PauseMan.Instance.MainPause();
    }
}
