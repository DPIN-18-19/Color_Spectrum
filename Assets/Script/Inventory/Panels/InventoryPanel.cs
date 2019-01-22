using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryPanel : MonoBehaviour
{
    [SerializeField]
    private ChipList i_chips;
    [SerializeField]
    private InventoryWeaponList i_weapons;

    Transform i_panel;
    public GameObject chip_mould;
    public GameObject weapon_chip_mould;

    private void Awake()
    {
        i_panel = transform.Find("InventoryPanel");
        LoadChips();
    }

    // Use this for initialization
    void Start ()
    {
	}

    void LoadChips()
    {
        // Introducir chips normales
        for (int i = 0; i < i_chips.chips.Count; ++i)
        {
            GameObject n_chip = Instantiate(chip_mould);
            n_chip.transform.SetParent(i_panel);
            n_chip.GetComponent<IChipData>().data = i_chips.chips[i];

            if(i_chips.chips[i].equipped)
            {
                n_chip.AddComponent<Darken>();
                Destroy(n_chip.GetComponent<IChipDrag>());
                n_chip.transform.Find("Equipped").gameObject.SetActive(true);
            }
        }

        // Introducir chips de armas
        for (int i = 0; i < i_weapons.i_weapon_chips.Count; ++i)
        {
            GameObject n_w_chip = Instantiate(weapon_chip_mould);
            n_w_chip.transform.SetParent(transform.Find("InventoryPanel"));
            n_w_chip.GetComponent<IWeaponChipDrag>().w_data = i_weapons.i_weapon_chips[i];
        }
    }

    public GameObject SearchChip(ChipData chip)
    {
        for(int i = 0; i < i_panel.childCount; ++i)
        {
            if (i_panel.GetChild(i).GetComponent<IChipData>().data.id == chip.id)
                return i_panel.GetChild(i).gameObject;
        }

        return null;
    }

    public void EquipChip(ChipData chip)
    {
        // Buscar chip con id
        int index = i_chips.chips.FindIndex(x => x.id == chip.id);
        i_chips.chips[index].equipped = true;
    }

    public void UnequipChip(ChipData chip)
    {
        // Buscar chip con id
        int index = i_chips.chips.FindIndex(x => x.id == chip.id);
        i_chips.chips[index].equipped = false;
    }
}
