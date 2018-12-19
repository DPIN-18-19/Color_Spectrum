using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScreen : MonoBehaviour
{
    List<Text> score_texts;
    bool do_once = true;

    private void Awake()
    {
        score_texts = new List<Text>();
        score_texts.AddRange(GetComponentsInChildren<Text>());
    }

    // Use this for initialization
    void Start ()
    {
    }
	
	public void UpdateStats()
    {
        ScoreManager.Instance.CalculateFinalScore();
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

        GameObject.Find("GameManager").GetComponent<SceneMan>().Invoke("ToMenu", 7);
    }
    
}
