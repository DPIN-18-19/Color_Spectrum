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

    Transform weight_panel;

    private void Start()
    {
        weight_panel = GameObject.Find("Weight").transform;
    }

    public void OnPointerEnter(PointerEventData p_event_data)
    {
        if (p_event_data.pointerDrag == null)
            return;

        // Nueva posible zona de retorno
        IChipDrag d = p_event_data.pointerDrag.GetComponent<IChipDrag>();
        if (d != null)
        {
            d.new_possible_deck = this.transform;
        }

        if (panel_type == PanelType.Weapon)
        {
            IWeaponChipDrag w = p_event_data.pointerDrag.GetComponent<IWeaponChipDrag>();
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
        IChipDrag d = p_event_data.pointerDrag.GetComponent<IChipDrag>();
        if (d != null && d.new_possible_deck == this.transform)
        {
            d.new_possible_deck = d.inv_deck;
        }

        if (panel_type == PanelType.Weapon)
        {
            IWeaponChipDrag w = p_event_data.pointerDrag.GetComponent<IWeaponChipDrag>();
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
        IChipDrag d = p_event_data.pointerDrag.GetComponent<IChipDrag>();
        if (d != null)
        {
            if (!ChipLimit())
            {
                d.new_possible_deck = d.inv_deck;
                return;
            }

            d.new_possible_deck = this.transform;

            if(ChipsFilter(d))
            {
                if (panel_type == PanelType.Weapon)
                {
                    IWeaponChipDrag w = p_event_data.pointerDrag.GetComponent<IWeaponChipDrag>();
                    if (w != null)
                    {
                        if (!weight_panel.GetComponent<WeightPanel>().CanFitChip(w.w_data.GetWeight()))
                        {
                            w.new_possible_deck = w.inv_deck;
                            return;
                        }
                        AddWeapon(w);
                        return;
                    }
                }

                if(panel_type == PanelType.AbilitySlot)
                {
                    IAbiChipData ad = p_event_data.pointerDrag.GetComponent<IAbiChipData>();
                    if (!weight_panel.GetComponent<WeightPanel>().CanFitChip(ad.abi_data.weight))
                    {
                        d.new_possible_deck = d.inv_deck;
                        return;
                    }
                    AddAbility(ad);
                    return;
                }

                if (!weight_panel.GetComponent<WeightPanel>().CanFitChip(d.ichip_data.data.weight))
                {
                    d.new_possible_deck = d.inv_deck;
                    return;
                }
                AddData(d);
            }

        }
    }

    bool ChipLimit()
    {
        // Limit the amount of chips the panel can have
        if (panel_type == PanelType.Weapon)
        {
            if (transform.childCount < GetComponentInParent<WeaponPanel>().weapon_slots)
                return true;
        }
        else if(panel_type == PanelType.WeaponSlot)
        {
            if (transform.childCount < GetComponent<WeaponSlotPanel>().w_slots)
                return true;
        }
        else if(panel_type == PanelType.AbilitySlot)
        {
            if (transform.childCount < 1)
            {
                return true;
            }
        }
        // Empy slot was found
        return false;
    }

    bool ChipsFilter(IChipDrag chip)
    {
        IChipData.ChipType chip_type = chip.ichip_data.chip_type;

        switch (chip_type)
        {
            case IChipData.ChipType.Upgrade:
                if (panel_type == PanelType.Weapon || panel_type == PanelType.AbilitySlot)
                {
                    chip.new_possible_deck = chip.inv_deck;
                    return false;
                }
                break;
            case IChipData.ChipType.Weapon:
                if (panel_type == PanelType.AbilitySlot || panel_type == PanelType.Player || panel_type == PanelType.WeaponSlot)
                {
                    chip.new_possible_deck = chip.inv_deck;
                    return false;
                }
                break;
            case IChipData.ChipType.Ability:
                if (panel_type == PanelType.Weapon || panel_type == PanelType.Player || panel_type == PanelType.WeaponSlot)
                {
                    chip.new_possible_deck = chip.inv_deck;
                    return false;
                }
                break;
            default:
                Debug.Log("Filter detected an unknown chip");
                break;
        }

        return true;
    }

    void AddData(IChipDrag chip)
    {
        switch(panel_type)
        {
            case PanelType.AbilitySlot:
                // Ability add data is done in a separate function
                //GetComponent<AbilityPanel>().AddChipToEquippedWeapon(chip.ichip_data.data);
                break;
            case PanelType.Inventory:
                break;
            case PanelType.Player:
                GetComponent<CharacterPanel>().character_p_chips.chips.Add(chip.ichip_data.data);
                break;
            case PanelType.Weapon:
                // Weapon add data is done in a separate function
                break;
            case PanelType.WeaponSlot:
                GetComponent<WeaponSlotPanel>().AddChipToEquippedWeapon(chip.ichip_data.data);
                break;
            default:
                Debug.Log("Add data to panel of unknown type");
                break;
        }
    }

    void AddWeapon(IWeaponChipDrag chip)
    {
        GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.Add(chip.w_data);

        GetComponent<WeaponPanel>().CheckCorrectOrder();
    }

    void AddAbility(IAbiChipData chip)
    {
        GetComponent<AbilityPanel>().AddChipToEquippedWeapon(chip.abi_data);
    }
}
