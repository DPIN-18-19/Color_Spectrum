using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuFlow : MonoBehaviour
{
    Transform new_game_b, continue_b, controls_b, main_b, exit_b;

	// Use this for initialization
	void Start ()
    {
        // Buscar botones en la escena
        new_game_b = transform.Find("NewGame_b");
        continue_b = transform.Find("Continue_b");
        controls_b = transform.Find("Controls_b");
        main_b = transform.Find("MainMenu_b");
        exit_b = transform.Find("Exit_b");

        // Comprobar si cada botón fue encontrado
        if (new_game_b)
            new_game_b.GetComponent<Button>().onClick.AddListener(ToNewGame);

        if (continue_b)
            continue_b.GetComponent<Button>().onClick.AddListener(ToContinue);

        if (controls_b)
            controls_b.GetComponent<Button>().onClick.AddListener(ToControls);

        if (main_b)
            main_b.GetComponent<Button>().onClick.AddListener(ToMainMenu);

        if (exit_b)
            exit_b.GetComponent<Button>().onClick.AddListener(ToExit);
    }

    void ToNewGame()
    {
        SceneMan1.Instance.LoadSceneByName("PreparationMenu");
        //PauseMan.Instance.Resume();
    }
    void ToContinue()
    {
        //SceneMan1.Instance.LoadSceneByName("Main_Menu");
    }
    void ToControls()
    {
        //SceneMan1.Instance.LoadSceneByName("Store");
        //PauseMan.Instance.Controls();
        MainMenuMan.Instance.Controls();
    }
    void ToMainMenu()
    {
        //SceneMan1.Instance.LoadSceneByName("Store");
        //PauseMan.Instance.MainPause();
        MainMenuMan.Instance.MainMenu();
    }
    
    void ToExit()
    {
        SceneMan1.Instance.ExitGame();
    }
}
