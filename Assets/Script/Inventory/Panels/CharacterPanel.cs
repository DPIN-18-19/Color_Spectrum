using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    [SerializeField]
    private ChipList character_chips;

    public GameObject chip_mould;

    // Use this for initialization
    void Start()
    {
        LoadChips();
    }

    void LoadChips()
    {
        for (int i = 0; i < character_chips.chips.Capacity; ++i)
        {
            GameObject n_chip = Instantiate(chip_mould);
            n_chip.transform.SetParent(transform.Find("CharacterPanel"));
            n_chip.GetComponent<InventoryChip>().data = character_chips.chips[i];

        }
    }
}
