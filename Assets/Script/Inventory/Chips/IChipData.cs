using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IChipData : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public enum ChipType
    {
        Upgrade,        // Normal
        Weapon,         // Chip de arma
        Ability         // Chip de habilidad
    }
    public ChipType chip_type;      // Clase de chip
    public ChipData data;

    //public string ability = "Dash";

    public GameObject hover_tooltip;                // Objeto con el que se creará el tooltip
    protected GameObject my_hover_tooltip;                    // Referencia al tooltip creado

    protected Transform canvas;                     // El canvas

    private void Start()
    {
        canvas = GetComponentInParent<Canvas>().transform;          // Coger el canvas de la interfaz
    }

    public virtual void OnPointerEnter(PointerEventData p_event_data)
    {
        //Evitar que se muestre un tooltip si ya se agarrando algo
        if (!Input.GetMouseButton(0))
        {
            my_hover_tooltip = Instantiate(hover_tooltip, p_event_data.position, Quaternion.identity);
            my_hover_tooltip.transform.SetParent(canvas);
            
            my_hover_tooltip.GetComponent<TooltipInfo>().ShowUpgrade(data);
        }
    }

    public virtual void OnPointerExit(PointerEventData p_event_data)
    {
        if (my_hover_tooltip)
            my_hover_tooltip.GetComponent<HoverFade>().Leave();
    }
}
