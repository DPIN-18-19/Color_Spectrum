using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MoneyPanel : MonoBehaviour
{
    public float player_money;

    TextMeshProUGUI money_t;
    public GameObject spent;

	// Use this for initialization
	void Start ()
    {
        money_t = transform.Find("MoneyText").GetComponent<TextMeshProUGUI>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        money_t.text = player_money.ToString();
	}

    public void SpendMoney(float spend)
    {
        player_money -= spend;
        GameObject amount = Instantiate(spent, money_t.transform);
        amount.GetComponent<TextMeshProUGUI>().text = "- " + spend.ToString();
    }
}
