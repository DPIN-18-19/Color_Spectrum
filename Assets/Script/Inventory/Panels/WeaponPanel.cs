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

            //IWeaponChipDrag w_drag = n_w_chip.GetComponent<IWeaponChipDrag>();

            //if(w_drag)
            //{


            //    Debug.Log("IWeaponChipDrag detected");
            //        //IChipData the_data = w_drag.ichip_data;

            //    if (w_drag.ichip_data != null)
            //    {
            //        Debug.Log("Data detected");
            //    }
            //    else
            //        Debug.Log("Data not detected");

            //    //if (w_drag.ichip_data.chip_type == IChipData.ChipType.Weapon)
            //    //    Debug.Log("id is " + w_drag.ichip_data.chip_type);
            //}
            //else
            //    Debug.Log("IWeaponChipDrag not detected");

            //w_drag.ichip_data.data.id = weapon_p_chips.i_weapon_chips[i].id;

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
}
