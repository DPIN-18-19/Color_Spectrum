using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityPanel : MonoBehaviour
{
    Transform i_panel;
    Transform w_panel;
    Transform w_chip;

    string w_id = "";

    public GameObject chip_mould;

    // Use this for initialization
    void Start()
    {
        i_panel = GameObject.Find("Inventory").transform;
        w_panel = GameObject.Find("WeaponPanel").transform;
        w_chip = GetComponentInParent<IWeaponChipDrag>().transform;
        w_id = w_chip.GetComponent<IWeaponChipDrag>().w_data.id;
        LoadChips();
    }

    void LoadChips()
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        // Check if there weapon to be loaded was found
        if (index != -1)
        {
            ChipData chip = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability;

            if (chip.ability != "")
            {
                GameObject n_chip = Instantiate(chip_mould);
                n_chip.transform.SetParent(transform);
                n_chip.GetComponent<IChipData>().data = chip;
                n_chip.GetComponent<IChipData>().chip_type = IChipData.ChipType.Ability;

                if (chip.equipped)
                {
                    if (i_panel.GetComponent<InventoryPanel>())
                    {
                        n_chip.GetComponent<IChipDrag>().shadow_copy = i_panel.GetComponent<InventoryPanel>().SearchChip(chip);
                    }
                    else
                        Debug.Log("There was an error loading character panel");
                }
            }
        }
    }

    public void AddChipToEquippedWeapon(ChipData chip)
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability = chip;
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability_name = chip.ability;

        // Is neccesary updating inventory weapon chip?
        index = i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips.FindIndex(x => x.id == w_id);
        i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips[index].ability = chip;
        i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips[index].ability_name = chip.ability;
    }

    public void RemoveChipFromEquippedWeapon(ChipData chip)
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability = null;
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability_name = "";

        index = i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips.FindIndex(x => x.id == w_id);
        i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips[index].ability = null;
        i_panel.GetComponent<InventoryPanel>().i_weapons.i_weapon_chips[index].ability_name = "";
    }
}
