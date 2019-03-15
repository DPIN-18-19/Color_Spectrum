using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IWeaponChipData : IChipData
{
    IWeaponData w_data;

	// Use this for initialization
	void Start ()
    {
        canvas = GetComponentInParent<Canvas>().transform;          // Coger el canvas de la interfaz
        w_data = GetComponent<IWeaponChipDrag>().w_data;
    }
	
    public override void OnPointerEnter(PointerEventData p_event_data)
    {
        //Evitar que se muestre un tooltip si ya se agarrando algo
        if (!Input.GetMouseButton(0))
        {
            my_hover_tooltip = Instantiate(hover_tooltip, p_event_data.position, Quaternion.identity);
            my_hover_tooltip.transform.SetParent(canvas);

            my_hover_tooltip.GetComponent<TooltipInfo>().ShowWeapon(w_data);
        }
    }
}
