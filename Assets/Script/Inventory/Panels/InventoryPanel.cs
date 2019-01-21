using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField]
    private ChipList i_chips;
    [SerializeField]
    private InventoryWeaponList i_weapons;

    public GameObject chip_mould;
    public GameObject weapon_chip_mould;

	// Use this for initialization
	void Start ()
    {
        LoadChips();
	}

    void LoadChips()
    {
        for (int i = 0; i < i_chips.chips.Capacity; ++i)
        {
            GameObject n_chip = Instantiate(chip_mould);
            n_chip.transform.SetParent(transform.Find("InventoryPanel"));
            n_chip.GetComponent<InventoryChip>().data = i_chips.chips[i];
        }

        for (int i = 0; i < i_weapons.i_weapon_chips.Capacity; ++i)
        {
            GameObject n_w_chip = Instantiate(weapon_chip_mould);
            n_w_chip.transform.SetParent(transform.Find("InventoryPanel"));
            n_w_chip.GetComponent<InventoryWeaponChip>().w_data = i_weapons.i_weapon_chips[i];
        }
    }
}
