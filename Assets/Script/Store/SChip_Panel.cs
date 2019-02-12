using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SChip_Panel : MonoBehaviour
{
    [SerializeField]
    private ChipList store_chips;

    public GameObject schip_mould;

    Transform d_panel;

    private void Awake()
    {
        d_panel = transform.Find("DisplayPanel");
        LoadChips();
    }
	
	void LoadChips()
    {
        for(int i = 0; i < store_chips.chips.Count; ++i)
        {
            GameObject n_chip = Instantiate(schip_mould);
            n_chip.transform.SetParent(d_panel);
            n_chip.GetComponent<SChipData>().data = store_chips.chips[i];
        }
    }

}
