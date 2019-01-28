using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    private void Start()
    {
        Cursor.visible = true;
    }

    public string cur_level;

    public void QuitGame()
    {
        Application.Quit();
    }

    public void ToMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void ToLevel1()
    {
        SceneManager.LoadScene("Pruebas_CinemaMachine");
    }
    public void ToCustomizacion()
    {
        SceneManager.LoadScene("Drag and Drop MINe");
    }

    public void NextLevel(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void ResetLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
