﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlotPanel : MonoBehaviour
{
    Transform i_panel;
    Transform w_panel;
    Transform w_chip;

    string w_id = "";

    public int w_slots = 3;

    public GameObject chip_mould;

    // Use this for initialization
    void Start()
    {
        i_panel = GameObject.Find("Inventory").transform;
        w_panel = GameObject.Find("WeaponPanel").transform;
        w_chip = GetComponentInParent<IWeaponChipDrag>().transform;
        w_id = w_chip.GetComponent<IWeaponChipDrag>().w_data.id;
        w_slots = w_chip.GetComponent<IWeaponChipDrag>().w_data.slots;
        LoadChips();
    }

    void LoadChips()
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        // Check if there are any weapons to be loaded
        if (index != -1)
        {
            List<ChipData> chips = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].added_chips;
            
            for (int i = 0; i < chips.Count; ++i)
            {
                GameObject n_chip = Instantiate(chip_mould);
                n_chip.transform.SetParent(transform);
                n_chip.GetComponent<IChipData>().data = chips[i];

                if (chips[i].equipped)
                {
                    if (i_panel.GetComponent<InventoryPanel>())
                        n_chip.GetComponent<IChipDrag>().shadow_copy = i_panel.GetComponent<InventoryPanel>().SearchChip(chips[i]);
                    else
                        Debug.Log("There was an error loading character panel");
                }

            }
        }
    }

    public void AddChipToEquippedWeapon(ChipData chip)
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].AddChip(chip);

        // Is neccesary updating inventory weapon chip?
        index = i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips.FindIndex(x => x.id == w_id);
        i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips[index].AddChip(chip);
    }

    public void RemoveChipFromEquippedWeapon(ChipData chip)
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].TakeOutChip(chip);

        index = i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips.FindIndex(x => x.id == w_id);
        i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips[index].TakeOutChip(chip);
    }
}
