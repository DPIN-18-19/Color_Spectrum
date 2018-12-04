using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{

    //public Image black;
    //public Animator anim;
    //public Image arrow;

    //int index = 0;
    //public int totalLevels = 3;
    //public float yOffset = 1f;

    private void Start()
    {
        Cursor.visible = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            QuitGame();
        }

        //if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        //{
        //    if (index < totalLevels - 1)
        //    {
        //        index++;
        //        Vector2 position = arrow.transform.position;
        //        position.y -= yOffset;
        //        arrow.transform.position = position;
        //    }
        //}
        //else if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        //{
        //    if (index > 0)
        //    {
        //        index--;
        //        Vector2 position = arrow.transform.position;
        //        position.y += yOffset;
        //        arrow.transform.position = position;
        //    }
        //}

        //if (Input.GetKeyDown(KeyCode.Return))
        //{
        //    if (index == 0)
        //    {
        //        PlayGame();
        //    }
        //    else if (index == 1)
        //    {
        //        QuitGame();
        //    }
        //}
    }

    //public void PlayGame()
    //{
    //    StartCoroutine(Fading());
    //    //SceneManager.LoadScene ("Test");
    //}

    public void QuitGame()
    {
        Application.Quit();
    }
    

    //IEnumerator Fading()
    //{
    //    anim.SetBool("Fade", true);
    //    yield return new WaitUntil(() => black.color.a == 1);
    //    SceneManager.LoadScene("Test");
    //}
}

