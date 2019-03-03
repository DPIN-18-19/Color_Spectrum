using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    List<Text> score_texts;
    bool do_once = true;
    public ScoreList level_scores;

    private void Awake()
    {
    }

    private void Start()
    {
    }

    public void Init()
    {
        score_texts = new List<Text>();
        score_texts.AddRange(transform.Find("ModifiableTexts").GetComponentsInChildren<Text>());
        ScoreManager.Instance.LoadScoreData(level_scores);

    }

	public void UpdateStats()
    {
        ScoreManager.Instance.CalculateFinalScore();
        PlayerManager.Instance.AddMoney(ScoreManager.Instance.GetFinalScore());
        //LevelMenuManager.Instance.UpdateScoreInfo();
        // Numero de golpes
        score_texts[0].text = ScoreManager.Instance.GetDamageCount().ToString();
        // Puntuacion de golpes
        score_texts[1].text = ScoreManager.Instance.QuickGetHealthScore().ToString();
        // Numero enemigos
        score_texts[2].text = ScoreManager.Instance.GetEnemyCount().ToString();
        // Puntuacion de enemigos
        score_texts[3].text = ScoreManager.Instance.GetEnemyScore().ToString();
        // Tiempo
        score_texts[4].text = ScoreManager.Instance.GetTime();
        // Puntuacion de tiempo
        score_texts[5].text = ((int)ScoreManager.Instance.QuickGetTimeMultiplier()).ToString();
        // Puntuacion final
        score_texts[6].text = ScoreManager.Instance.GetFinalScore().ToString();
        // Nota final
        score_texts[7].text = ScoreManager.Instance.GetFinalGrade();
        score_texts[7].material = ScoreManager.Instance.QuickGetGradeMat();
        
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Store");
        //SceneMan1.Instance.LoadSceneByName("LevelSelection");
        this.Invoke("LoadNext", 5);
    }

    void LoadNext()
    {
        SceneMan1.Instance.LoadSceneByName("LevelSelection");
        //UnityEngine.SceneManagement.SceneManager.LoadScene("Store");
    }

}
