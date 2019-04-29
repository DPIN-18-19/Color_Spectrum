using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WeightPanel : MonoBehaviour
{
    public IWeaponChipList weapon_p_chips;
    public ChipList character_p_chips;

    float weight;               // Cantidad de peso
    float max_weight;           // Máximo peso permitido

    TextMesh text;
    Image weight_bar;
    TextMeshProUGUI weight_text;

    void Start()
    {
        weight_bar = transform.Find("WeightBar").GetComponent<Image>();
        weight_text = transform.Find("WeightText").GetComponent<TextMeshProUGUI>();

        max_weight = PlayerManager.Instance.weight;

        CalculateWeight();
    }

    //void Update()
    //{
        
    //}

    // Calcular peso total de todos los elementos equipados
    public void CalculateWeight()
    {
        // Resetear contador de peso
        weight = 0;

        // Calcular peso de las armas
        for(int i = 0; i < weapon_p_chips.i_weapon_chips.Count; ++i)
        {
            weight += weapon_p_chips.i_weapon_chips[i].GetWeight();
        }

        // Calcular peso del jugador
        for(int i = 0; i < character_p_chips.chips.Count; ++i)
        {
            weight += character_p_chips.chips[i].weight;
        }

        // Actualizar barra con nuevo peso
        UpdateUI();
    }

    void UpdateUI()
    {
        // Actualizar imagen
        weight_bar.fillAmount = weight / max_weight;

        // Actualizar texto
        weight_text.text = weight.ToString() + " / " + max_weight.ToString();
    }

    // Comprobar si es posible insertar un nuevo elemento
    public bool CanFitChip(float n_weight)
    {
        if (n_weight + weight <= max_weight)
            return true;

        return false;
    }
}
