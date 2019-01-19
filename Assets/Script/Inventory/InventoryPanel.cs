using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InventoryPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public enum PanelType
    {
        Inventory,
        Weapon,
        AbilitySlot,
        WeaponSlot,
        Player
    }

    public PanelType panel_type;

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

        if (panel_type == PanelType.Weapon)
        {
            InventoryWeaponChip w = p_event_data.pointerDrag.GetComponent<InventoryWeaponChip>();
            if (w != null)
            {
                w.inside = true;
                w.weapon_deck = this.transform;
            }
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

        if (panel_type == PanelType.Weapon)
        {
            InventoryWeaponChip w = p_event_data.pointerDrag.GetComponent<InventoryWeaponChip>();
            if (w != null)
            {
                w.inside = false;
                w.weapon_deck = w.org_deck;
            }
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
