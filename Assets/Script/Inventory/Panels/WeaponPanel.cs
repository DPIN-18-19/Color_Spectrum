using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    public IWeaponChipList weapon_p_chips;

    Transform i_panel;
    public GameObject weapon_chip_mould;

    public int weapon_slots = 3;

    // Use this for initialization
    void Start ()
    {
        i_panel = GameObject.Find("Inventory").transform;
        LoadChips();
    }

    void LoadChips()
    {
        for (int i = 0; i < weapon_p_chips.i_weapon_chips.Count; ++i)
        {
            GameObject n_w_chip = Instantiate(weapon_chip_mould);
            n_w_chip.transform.SetParent(transform);
            n_w_chip.GetComponent<IWeaponChipDrag>().w_data = weapon_p_chips.i_weapon_chips[i];

            n_w_chip.GetComponent<IChipData>().data.id = weapon_p_chips.i_weapon_chips[i].id;

            // Show weapon chip form
            Transform weapon_panel = n_w_chip.transform.Find("WeaponForm");
            weapon_panel.gameObject.SetActive(true);
            Transform chip_form = n_w_chip.transform.Find("ChipForm");
            chip_form.gameObject.SetActive(false);


            if (weapon_p_chips.i_weapon_chips[i].equipped)
            {
                if (i_panel.GetComponent<InventoryPanel>())
                {
                    n_w_chip.GetComponent<IWeaponChipDrag>().shadow_copy = i_panel.GetComponent<InventoryPanel>().SearchChip(n_w_chip.GetComponent<IChipData>().data);
                }
                else
                    Debug.Log("There was an error loading character panel");
            }
        }
    }

    public void CheckCorrectOrder()
    {
        for(int i = 0; i < transform.childCount; ++i)
        {
            // Check if current weapon is at correct position
            if(transform.GetChild(i).GetComponent<IChipData>().data.id != weapon_p_chips.i_weapon_chips[i].id)
            {
                //Search Correct position
                for(int j = i; j < weapon_p_chips.i_weapon_chips.Count; ++j)
                {
                    // Search where weapon is actually stored
                    if(transform.GetChild(i).GetComponent<IChipData>().data.id == weapon_p_chips.i_weapon_chips[j].id)
                    {
                        // Swap
                        IWeaponData temp = weapon_p_chips.i_weapon_chips[j];
                        weapon_p_chips.i_weapon_chips[j] = weapon_p_chips.i_weapon_chips[i];
                        weapon_p_chips.i_weapon_chips[i] = temp;
                    }
                }
            }
        }
    }
}
