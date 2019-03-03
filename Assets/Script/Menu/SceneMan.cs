using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    
    public void Start()
    {
        Time.timeScale = 1f;
    }
    public string cur_level;

    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ToLevel1()
    {
        SceneManager.LoadScene("Nivel_1");
    }
    public void ToCustomizacion()
    {
        SceneManager.LoadScene("Customizacion");
    }

    public void NextLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void ToTutorial()
    {
        SceneManager.LoadScene("Tutorial");
    }
    public void ExitGame()
    {
        Application.Quit();
    }
   

    // public void Controls()
}
