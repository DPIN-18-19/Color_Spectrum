using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SChipData : MonoBehaviour, IPointerDownHandler
{
    public enum ChipType
    {
        Upgrade,        // Normal
        Weapon,         // Chip de arma
        Ability         // Chip de habilidad
    }
    public ChipType chip_type;      // Clase de chip
    public ChipData data;

    TextMeshProUGUI price;
    Image money_symbol;

    public GameObject hover_tooltip;                // Objeto con el que se creará el tooltip
    GameObject my_hover_tooltip;                    // Referencia al tooltip creado

    protected Transform canvas;                     // El canvas
    Transform display_panel;
    Transform outline;

    bool purchasable = true;

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>().transform;          // Coger el canvas de la interfaz
        display_panel = GetComponentInParent<SChipPanel>().transform;
        price = transform.Find("PricePanel").GetComponentInChildren<TextMeshProUGUI>();
        WritePrice();
        money_symbol = transform.Find("PricePanel").Find("Symbol").GetComponent<Image>();
        outline = transform.Find("Outline");
    }

    void WritePrice()
    {
        price.text = data.price.ToString();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("I was selected " + data.price);
        if (purchasable)
        {
            outline.gameObject.SetActive(true);
            display_panel.GetComponent<SChipPanel>().ClearSelect();
            display_panel.GetComponent<SChipPanel>().MakeSelection(data);
            display_panel.GetComponent<SChipPanel>().select_click = true;
        }
    }

    public void DeselectChip()
    {
        Debug.Log("I was deselected " + data.price);
        outline.gameObject.SetActive(false);
    }

    public void MakeUnpurchaseable()
    {
        if (purchasable)
        {
            purchasable = false;
            Darken darkness = gameObject.AddComponent<Darken>();
            darkness.DarkenColor(0.5f);

            price.color = Color.red;
            money_symbol.color = Color.red;
        }
    }
    //public void OnPointerEnter(PointerEventData p_event_data)
    //{
    //    // Evitar que se muestre un tooltip si ya se agarrando algo
    //    //if (!Input.GetMouseButton(0))
    //    //{
    //    //    my_hover_tooltip = Instantiate(hover_tooltip, p_event_data.position, Quaternion.identity);
    //    //    my_hover_tooltip.transform.SetParent(canvas);
    //    //}
    //}

    //public void OnPointerExit(PointerEventData p_event_data)
    //{
    //    //if (my_hover_tooltip)
    //    //    my_hover_tooltip.GetComponent<HoverTooltip>().Leave();
    //}
}
