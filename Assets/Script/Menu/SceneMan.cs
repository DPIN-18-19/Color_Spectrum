using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan : MonoBehaviour
{
    public string cur_level;

    public void ToMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void ToLevel1()
    {
        SceneManager.LoadScene("Pruebas_CinemaMachine");
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
