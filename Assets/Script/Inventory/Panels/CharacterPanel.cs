using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : MonoBehaviour
{
    public ChipList character_chips;

    Transform i_panel;
    public GameObject chip_mould;

    // Use this for initialization
    void Start()
    {
        i_panel = GameObject.Find("Inventory").transform;
        LoadChips();
    }

    void LoadChips()
    {
        for (int i = 0; i < character_chips.chips.Count; ++i)
        {
            GameObject n_chip = Instantiate(chip_mould);
            n_chip.transform.SetParent(transform);
            n_chip.GetComponent<IChipData>().data = character_chips.chips[i];

            if(character_chips.chips[i].equipped)
            {
                if (i_panel.GetComponent<InventoryPanel>())
                    n_chip.GetComponent<IChipDrag>().shadow_copy = i_panel.GetComponent<InventoryPanel>().SearchChip(character_chips.chips[i]);
                else
                    Debug.Log("There was an error loading character panel");
            }
        }
    }
}
