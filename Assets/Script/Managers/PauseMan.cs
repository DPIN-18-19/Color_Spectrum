using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMan : MonoBehaviour
{
    public static PauseMan Instance { get; private set; }
    
    public Transform pause_p;       // Panel principal de pause
    public Transform controls_p;    // Panel de controles
    public Transform buttons_p;     // Panel de botones de pausa

    bool can_pause;                 // Comprobador de posibilidad de pausa
    public bool is_pause;           // Comprobador de juego en pausa
    bool pause_press;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Start()
    {
        SceneManager.sceneLoaded += OnGameLoaded;
    }

    void OnGameLoaded(Scene scene, LoadSceneMode mode)
    {
        // Comprobar escenas con menú de pausa
        if (scene.buildIndex >= 8 && scene.buildIndex <= 12)
        {
            // Inicializar panel pausa
            pause_p = GameObject.Find("Pause_Menu").transform;
            controls_p = pause_p.Find("Controls_p");
            buttons_p = pause_p.Find("Buttons_p");

            controls_p.gameObject.SetActive(false);
            pause_p.gameObject.SetActive(false);

            can_pause = true;
        }
        else
            can_pause = false;

        is_pause = false;
    }

    void Update()
    {
        if (can_pause)
        {
            if (Input.GetKeyDown(KeyCode.Escape) && is_pause == true && pause_press == false)
            {
                Resume();
                pause_press = true;
            }

            if (Input.GetKeyDown(KeyCode.Escape) && is_pause == false && pause_press == false)
            {
                Pause();
                pause_press = true;
            }

            if (Input.GetKeyUp(KeyCode.Escape))
                pause_press = false;
        }
    }

    public void Pause()
    {
        pause_p.gameObject.SetActive(true);
        //MainPause();
        Time.timeScale = 0f; //Timepo de juego pausado.
        is_pause = true;
        Debug.Log("Entered Pause");
    }

    public void Resume()
    {
        pause_p.gameObject.SetActive(false);//Desactiva rueda armas
        Time.timeScale = 1f;
        is_pause = false;
        Debug.Log("Exit Pause");
    }

    public void Controls()
    {
        buttons_p.gameObject.SetActive(false);
        controls_p.gameObject.SetActive(true);
    }
    public void MainPause()
    {
        buttons_p.gameObject.SetActive(true);
        controls_p.gameObject.SetActive(false);
    }
}
