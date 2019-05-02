using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneMan1 : MonoBehaviour
{
    float TimRestart = 1.5f;
    public bool isdead = false;

    //////////////////////////////////////////////////////
    // Singleton architecture
    public static SceneMan1 Instance { get; private set; }

    public Transform blackscreen;
    public float fade_dur;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }
    private void Update()
    {
        if (isdead)
        {
            TimRestart -= Time.deltaTime;
        }
        if (TimRestart <= 0)
        {
            TimRestart = 2;
            isdead = false;
            ReloadCurrentScene();
        }
    }

    //////////////////////////////////////////////////////
    // Scene Handling

    public enum SceneIndex
    {
        PreLoadScene,
        Loading,
        Main_Menu,
        PreparationMenu,
        LevelSelection,
        Customizacion,
        Store,
        Tuto_Customizacion,
        Nivel_1,
        Nivel_2,
        Nivel_3,
        Nivel_4,
        Nivel_6,
        Tutorial,
        TestSceneJaime

        //UnlockTestingLevel,
    }
    
    SceneIndex scene_to_load;
    
    //public void LoadSceneByName(string scene_name)
    //{
    //    // Cannot check scene names when outside of editor
    //    SceneManager.LoadScene(scene_name);
    //}
    
    public void LoadSceneByName(string scene_name)
    {
        scene_to_load = (SceneIndex)System.Enum.Parse(typeof(SceneIndex), scene_name);

        blackscreen.parent.gameObject.SetActive(true);
        StartCoroutine(FadeToLoad());
        
        //SceneManager.LoadScene("Loading");
        //SceneManager.LoadScene(scene_name);
    }
    
    public  void ReloadCurrentScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    
    public int GetLoadScene()
    {
        return (int)scene_to_load;
    }

    public void ExitGame()
    {
        Application.Quit();
    }
    public void Dead()
    {
        isdead = true;
    }

    IEnumerator FadeToLoad()
    {
        bool do_loop = true;
        float fade_c = 0.0001f;

        while (do_loop)
        {
            blackscreen.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(0, 1, fade_c / fade_dur);
            fade_c += Time.deltaTime;

            if (blackscreen.GetComponent<CanvasGroup>().alpha == 1)
            {
                SceneManager.LoadScene("Loading");
                do_loop = false;
            }

            yield return null;
        }
    }


    public void PostLoad()
    {
        StartCoroutine(FadeAfterLoad());
    }

    IEnumerator FadeAfterLoad()
    {
        bool do_loop = true;
        float fade_c = 0.0001f;

        while (do_loop)
        {
            blackscreen.GetComponent<CanvasGroup>().alpha = Mathf.Lerp(1, 0, fade_c / fade_dur);
            fade_c += Time.deltaTime;

            if (blackscreen.GetComponent<CanvasGroup>().alpha == 0)
            {
                blackscreen.parent.gameObject.SetActive(false);
                do_loop = false;
            }

            yield return null;
        }
    }
}
