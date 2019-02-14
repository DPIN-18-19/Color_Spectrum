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

    Transform display_p;
    Button buy_b;
    Transform money_p;

    public ChipData chip_to_buy;
    public bool select_click = false;
    public bool allow_click = true;

    private void Awake()
    {
        display_p = transform.Find("DisplayPanel");
        buy_b = transform.Find("BuyButton").GetComponent<Button>();
        money_p = transform.Find("MoneyPanel");
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
            n_chip.transform.SetParent(display_p);
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

        ItemPurchasable();
    }

    public void ClearSelect()
    {
        for (int i = 0; i < display_p.childCount; ++i)
        {
            Debug.Log(display_p.GetChild(i).GetComponent<SChipData>().data.id);
            if (display_p.GetChild(i).GetComponent<SChipData>().data.id == chip_to_buy.id)
            {
                display_p.GetChild(i).GetComponent<SChipData>().DeselectChip();
                chip_to_buy.id = "";
            }
        }
    }

    public void BuyItem()
    {
        ChipData bought = new ChipData();
        bought.Clone(chip_to_buy);
        money_p.GetComponent<MoneyPanel>().SpendMoney(bought.price);
        inventory_chips.chips.Add(bought);

        for(int i = 0; i < display_p.childCount; ++i)
        {
            if(display_p.GetChild(i).GetComponent<SChipData>().data.id == chip_to_buy.id)
            {
                Destroy(display_p.GetChild(i).gameObject);
                break;
            }
        }

        int index = store_chips.chips.FindIndex(x => x.id == chip_to_buy.id);
        store_chips.chips.RemoveAt(index);
        chip_to_buy.id = "";
    }

    void ItemPurchasable()
    {
        for (int i = 0; i < display_p.childCount; ++i)
        {
            if(money_p.GetComponent<MoneyPanel>().player_money < display_p.GetChild(i).GetComponent<SChipData>().data.price)
            {
                display_p.GetChild(i).GetComponent<SChipData>().MakeUnpurchaseable();
            }
        }
    }
}
