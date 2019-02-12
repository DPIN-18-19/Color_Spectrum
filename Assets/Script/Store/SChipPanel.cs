using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SChipPanel : MonoBehaviour
{
    [SerializeField]
    private ChipList store_chips;
    [SerializeField]
    private ChipList inventory_chips;

    public GameObject schip_mould;

    Transform d_panel;
    Button buy_b;

    public ChipData chip_to_buy;
    public bool select_click = false;
    public bool allow_click = true;

    private void Awake()
    {
        d_panel = transform.Find("DisplayPanel");
        buy_b = transform.Find("BuyButton").GetComponent<Button>();
        LoadChips();
    }

    private void Start()
    {
    }

    void LoadChips()
    {
        for(int i = 0; i < store_chips.chips.Count; ++i)
        {
            GameObject n_chip = Instantiate(schip_mould);
            n_chip.transform.SetParent(d_panel);
            n_chip.GetComponent<SChipData>().data.Clone(store_chips.chips[i]);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (chip_to_buy.id != "" && !select_click && allow_click)
            {
                ClearSelect();
            }
        }
        
        if(Input.GetMouseButtonUp(0))
            select_click = false;

        if (chip_to_buy.id == "")
            buy_b.interactable = false;
        else
            buy_b.interactable = true;
    }

    public void ClearSelect()
    {
        for (int i = 0; i < d_panel.childCount; ++i)
        {
            Debug.Log(d_panel.GetChild(i).GetComponent<SChipData>().data.id);
            if (d_panel.GetChild(i).GetComponent<SChipData>().data.id == chip_to_buy.id)
            {
                d_panel.GetChild(i).GetComponent<SChipData>().DeselectChip();
                chip_to_buy.id = "";
            }
        }
    }

    public void BuyItem()
    {
        ChipData bought = new ChipData();
        bought.Clone(chip_to_buy);
        inventory_chips.chips.Add(bought);

        for(int i = 0; i < d_panel.childCount; ++i)
        {
            if(d_panel.GetChild(i).GetComponent<SChipData>().data.id == chip_to_buy.id)
            {
                Destroy(d_panel.GetChild(i).gameObject);
                break;
            }
        }

        int index = store_chips.chips.FindIndex(x => x.id == chip_to_buy.id);
        store_chips.chips.RemoveAt(index);
        chip_to_buy.id = "";
    }
}
