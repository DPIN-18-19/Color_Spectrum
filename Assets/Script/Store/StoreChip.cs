using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StoreChip : MonoBehaviour, IPointerDownHandler
{
    public SChipData data;                          // Datos de objeto de tienda

    TextMeshProUGUI price_t;                        // Texto de precio
    Image money_symbol;                             // Simbolo de dinero
    protected Transform canvas;                     // El canvas
    Transform display_p;                            // Ventana donde se situa el objeto
    Transform outline;                              // Recuadro de selección
    Transform icon;                                 // Icono de chip

    public GameObject hover_tooltip;                // Objeto con el que se creara el tooltip
    GameObject my_hover_tooltip;                    // Referencia al tooltip creado
    
    bool purchasable = true;                        // Alcanza el dinero a comprarlo

    bool is_init;
    
    private void Start()
    {
        if (!is_init)
            Init();
    }

    public void Init()
    {
        canvas = GetComponentInParent<Canvas>().transform;          // Coger el canvas de la interfaz
        display_p = GetComponentInParent<SChipPanel>().transform;

        price_t = transform.Find("PricePanel").GetComponentInChildren<TextMeshProUGUI>();
        WritePrice();
        money_symbol = transform.Find("PricePanel").Find("Symbol").GetComponent<Image>();
        icon = transform.Find("Icon");
        DrawIcon();
        outline = transform.Find("Outline");
        is_init = true;
    }

    // Escribir el precio del objeto
    void WritePrice()
    {
        price_t.text = data.price.ToString();
    }

    void DrawIcon()
    {
        switch(data.schip_type)
        {
            case SChipData.SChipType.Upgrade:
                Debug.Log("werw");
                icon.GetComponent<Image>().sprite = data.u_data.display_icon;
                break;
            case SChipData.SChipType.Weapon:
                icon.GetComponent<Image>().sprite = data.g_data.display_icon;
                break;
            case SChipData.SChipType.Ability:
                icon.GetComponent<Image>().sprite = data.a_data.display_icon;
                break;
        }
    }

    // El raton se situa encima del objeto y se ha detectado un click
    public void OnPointerDown(PointerEventData eventData)
    {
        if (purchasable)
        {
            outline.gameObject.SetActive(true);
            // Eliminar la seleccion previa
            display_p.GetComponent<SChipPanel>().ClearSelect();
            // Coger datos de nueva seleccion
            display_p.GetComponent<SChipPanel>().MakeSelection(data);
            // Evitar comportamientos extranos en el mismo click
            display_p.GetComponent<SChipPanel>().select_click = true;
        }
    }

    public void DeselectChip()
    {
        //Debug.Log("I was deselected " + u_data.price);
        outline.gameObject.SetActive(false);
    }

    public void MakeUnpurchaseable()
    {
        if (purchasable)
        {
            purchasable = false;
            // Alterar imagen visual
            Darken darkness = gameObject.AddComponent<Darken>();
            darkness.DarkenColor(0.5f);

            // Alterar precio visual
            price_t.color = Color.red;
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
