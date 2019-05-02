using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowScore : MonoBehaviour
{
    // public KillCondition lvl_trigger;
    public AreaCondition lvl_trigger2;
    public GameObject ScoreScreen;

    // Use this for initialization
    void Start ()
    {
      //  lvl_trigger.KillWave += ShowScoreScreen;
        lvl_trigger2.EnterArea += ShowScoreScreen;
    }
	
	// Update is called once per frame
	void Update ()
    {
		

	}

    void ShowScoreScreen()
    {
        
        ScoreScreen.SetActive(true);
        ScoreScreen.GetComponentInChildren<ScoreScreen>().UpdateStats();
        Debug.Log("EstoyAqui");
        // Time.timeScale = 0.0f;
        ScoreManager.Instance.is_result_active = true;
    }
}
