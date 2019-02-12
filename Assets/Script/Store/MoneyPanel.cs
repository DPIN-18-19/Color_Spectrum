using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPanel : MonoBehaviour
{
    float player_money;

    TextMeshProUGUI money_t;

	// Use this for initialization
	void Start ()
    {
        money_t = transform.Find("MoneyText").GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}
}
