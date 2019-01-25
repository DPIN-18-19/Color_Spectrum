using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeightPanel : MonoBehaviour
{
    public IWeaponChipList weapon_p_chips;
    public ChipList character_p_chips;

    float weight = 0.01f;
    public float max_weight = 100;

    Image weight_bar;
    TextMeshProUGUI weight_text;

    void Start()
    {
        weight_bar = transform.Find("WeightBar").GetComponent<Image>();
        weight_text = transform.Find("WeightText").GetComponent<TextMeshProUGUI>();
        
        CalculateWeight();
    }

    void Update()
    {
        UpdateUI();
    }

    public void CalculateWeight()
    {
        weight = 0;

        // Calculate weapons weight
        for(int i = 0; i < weapon_p_chips.i_weapon_chips.Count; ++i)
        {
            weight += weapon_p_chips.i_weapon_chips[i].GetWeight();
        }

        //Calculate characters weight
        for(int i = 0; i < character_p_chips.chips.Count; ++i)
        {
            weight += character_p_chips.chips[i].weight;
        }
    }

    void UpdateUI()
    {
        //Update image
        weight_bar.fillAmount = weight / max_weight;

        //Update text
        weight_text.text = weight.ToString() + " / " + max_weight.ToString();
    }
}
