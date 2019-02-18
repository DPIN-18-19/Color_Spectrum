using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BuyButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform store_p;
    Button buy_b;

	// Use this for initialization
	void Start ()
    {
        store_p = transform.parent.Find("StoreWindow").GetComponent<SChipPanel>().transform;
        buy_b = GetComponent<Button>();
        buy_b.onClick.AddListener(ClickBuy);
    }
	

    public void OnPointerEnter(PointerEventData p_data)
    {
        Debug.Log("In button");
        store_p.GetComponent<SChipPanel>().allow_click = false;
    }
    public void OnPointerExit(PointerEventData p_data)
    {
        Debug.Log("Out button");
        store_p.GetComponent<SChipPanel>().allow_click = true;
    }

    void ClickBuy()
    {
        store_p.GetComponent<SChipPanel>().BuyItem();
    }
}
