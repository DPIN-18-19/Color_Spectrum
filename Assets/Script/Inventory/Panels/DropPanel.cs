using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropPanel : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
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
            d.new_possible_deck = d.inv_deck;
        }

        if (panel_type == PanelType.Weapon)
        {
            InventoryWeaponChip w = p_event_data.pointerDrag.GetComponent<InventoryWeaponChip>();
            if (w != null)
            {
                w.inside = false;
                w.weapon_deck = w.inv_deck;
            }
        }
    }

    public void OnDrop(PointerEventData p_event_data)
    {
        // Nueva posicion de retorno es lugar donde se suelta
        InventoryChip d = p_event_data.pointerDrag.GetComponent<InventoryChip>();
        if (d != null)
        {
            // Limit the amount of chips the panel can have
            if(panel_type == PanelType.Weapon)
            {
                if (transform.childCount >= GetComponentInParent<WeaponPanel>().weapon_slots)
                    return;
            }

            d.new_possible_deck = this.transform;

            if(ChipsFilter(d))
            {
                AddData(d);
            }
        }
    }

    bool ChipsFilter(InventoryChip chip)
    {
        InventoryChip.ChipType chip_type = chip.chip_type;

        switch (chip_type)
        {
            case InventoryChip.ChipType.Upgrade:
                if (panel_type == PanelType.Weapon || panel_type == PanelType.AbilitySlot)
                {
                    chip.new_possible_deck = chip.org_deck;
                    return false;
                }
                break;
            case InventoryChip.ChipType.Weapon:
                if (panel_type == PanelType.AbilitySlot || panel_type == PanelType.Player || panel_type == PanelType.WeaponSlot)
                {
                    chip.new_possible_deck = chip.org_deck;
                    return false;
                }
                break;
            case InventoryChip.ChipType.Ability:
                if (panel_type == PanelType.Weapon || panel_type == PanelType.Player || panel_type == PanelType.WeaponSlot)
                {
                    chip.new_possible_deck = chip.org_deck;
                    return false;
                }
                break;
            default:
                Debug.Log("Filter detected an unknown chip");
                break;
        }

        return true;
    }

    void AddData(InventoryChip chip)
    {
        switch(panel_type)
        {
            case PanelType.AbilitySlot:
                break;
            case PanelType.Inventory:
                break;
            case PanelType.Player:
                GetComponent<CharacterPanel>().character_chips.chips.Add(chip.data);
                break;
            case PanelType.Weapon:
                break;
            case PanelType.WeaponSlot:
                break;
            default:
                Debug.Log("Add data to panel of unknown type");
                break;
        }
    }
}
