using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
   // public KillCondition lvl_trigger;
    public AreaCondition lvl_trigger2;
    public GameObject ScoreScreen;
    public bool is_paused;

    // Use this for initialization
    void Start ()
    {
        //lvl_trigger.KillWave += ShowScoreScreen;
        lvl_trigger2.EnterArea += ShowScoreScreen;
    }
	
    void ShowScoreScreen()
    {
        ScoreScreen.SetActive(true);
        ScoreScreen.GetComponentInChildren<ScoreScreen>().UpdateStats();
        Time.timeScale = 0.0f;
        is_paused = true;
    }
}
