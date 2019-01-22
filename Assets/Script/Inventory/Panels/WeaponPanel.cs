using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPanel : MonoBehaviour
{
    public IWeaponChipList weapon_p_chips;
    public GameObject weapon_chip_mould;

    public int weapon_slots = 3;

    // Use this for initialization
    void Start ()
    {
        LoadChips();
    }

    void LoadChips()
    {
        for (int i = 0; i < weapon_p_chips.i_weapon_chips.Capacity; ++i)
        {
            GameObject n_w_chip = Instantiate(weapon_chip_mould);
            n_w_chip.transform.SetParent(transform.Find("WeaponPanel"));
            n_w_chip.GetComponent<IWeaponChipDrag>().w_data = weapon_p_chips.i_weapon_chips[i];
        }
    }
}
