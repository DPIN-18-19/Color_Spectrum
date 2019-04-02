using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IAbiChipData : IChipData
{
    public AbilityData abi_data;
    public string abi_name;

	// Use this for initialization
	void Start ()
    {
        canvas = GetComponentInParent<Canvas>().transform;          // Coger el canvas de la interfaz
    }

    public override void OnPointerEnter(PointerEventData p_event_data)
    {
        //Evitar que se muestre un tooltip si ya se agarrando algo
        if (!Input.GetMouseButton(0))
        {
            my_hover_tooltip = Instantiate(hover_tooltip, p_event_data.position, Quaternion.identity);
            my_hover_tooltip.transform.SetParent(canvas);

            my_hover_tooltip.GetComponent<TooltipInfo>().ShowAbility(abi_data);
        }
    }
}
