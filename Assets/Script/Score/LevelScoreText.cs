using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class LevelScoreText : MonoBehaviour
{
    TextMeshProUGUI score_t;
	// Use this for initialization
	void Start ()
    {
        score_t = transform.Find("Points_t").GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        score_t.text = ScoreManager.Instance.GetEnemyScore().ToString();
	}
}
