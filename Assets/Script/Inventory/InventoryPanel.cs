using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum PaneType
    {
        Inventory,
        Weapon,
        WeaponSlot,
        Player
    }

    public PaneType panel_type;

    public void OnPointerEnter(PointerEventData p_event_data)
    {
        if (p_event_data.pointerDrag == null)
            return;

        // Nueva posible zona de retorno
        InventoryChip d = p_event_data.pointerDrag.GetComponent<InventoryChip>();
        if (d != null)
        {
            d.new_possible_deck = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData p_event_data)
    {
        if (p_event_data.pointerDrag == null)
            return;

        // Recuperar zona anterior de retorno
        InventoryChip d = p_event_data.pointerDrag.GetComponent<InventoryChip>();
        if (d != null && d.new_possible_deck == this.transform)
        {
            d.new_possible_deck = d.org_deck;
        }
    }

    public void OnDrop(PointerEventData p_event_data)
    {
        // Nueva posicion de retorno es lugar donde se suelta
        InventoryChip d = p_event_data.pointerDrag.GetComponent<InventoryChip>();
        if (d != null)
        {
            d.new_possible_deck = this.transform;
        }
    }
}
