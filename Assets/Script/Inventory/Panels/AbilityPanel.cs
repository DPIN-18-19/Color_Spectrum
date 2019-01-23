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
        ChipData chip = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability;

        if(chip.ability != "")
        {
            Debug.Log("Ability is " + chip.ability);

            GameObject n_chip = Instantiate(chip_mould);
            n_chip.transform.SetParent(transform);
            n_chip.GetComponent<IChipData>().data = chip;
            n_chip.GetComponent<IChipData>().chip_type = IChipData.ChipType.Ability;

            if (chip.equipped)
            {
                if (i_panel.GetComponent<InventoryPanel>())
                {
                    n_chip.GetComponent<IChipDrag>().shadow_copy = i_panel.GetComponent<InventoryPanel>().SearchChip(chip);

                    Debug.Log(n_chip.GetComponent<IChipDrag>().shadow_copy.transform.name);
                }
                else
                    Debug.Log("There was an error loading character panel");
            }
        }
    }

    public void AddChipToEquippedWeapon(ChipData chip)
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability = chip;
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability_name = chip.ability;

        // Is neccesary updating inventory weapon chip?
        //i_panel.GetComponent<InventoryPanel>().
    }

    public void RemoveChipFromEquippedWeapon(ChipData chip)
    {
        int index = w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.FindIndex(x => x.id == w_id);
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability = null;
        w_panel.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips[index].ability_name = "";
    }
}
