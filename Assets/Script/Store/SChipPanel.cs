using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SChipPanel : MonoBehaviour
{
    [SerializeField]
    private ChipList store_chips;           // Chips en la tienda
    [SerializeField]
    private ChipList inventory_chips;       // Inventario

    public GameObject schip_mould;          // Molde de chip tienda

    Transform display_p;        // Ventana de chips
    Button buy_b;               // Botón de compra
    Transform money_p;          // Ventana de dinero
    Transform info_p;           // Ventana de información de producto

    // Chip seleccionado
    public ChipData chip_to_buy;


    public bool select_click = false;   // Click de selección. Evitar varias operaciones con el mismo click.
    public bool allow_click = true;     // Permitir selección/deselección en ciertas áreas

    private void Awake()
    {
        // Inicialización
        display_p = transform.Find("DisplayPanel");
        buy_b = transform.parent.Find("BuyButton").GetComponent<Button>();
        money_p = transform.parent.Find("MoneyPanel");
        info_p = transform.parent.Find("InfoPanel");
        LoadChips();
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

        // Actualizar botón de compra
        if (chip_to_buy.id == "")
            buy_b.interactable = false;
        else
            buy_b.interactable = true;

        // Move this to buy function and init
        ItemPurchasable();
    }

    public void MakeSelection(ChipData selected)
    {
        // Guardar datos de selección
        chip_to_buy.Clone(selected);
        // Mostrar datos en ventana de información
        info_p.GetComponent<InfoPanel>().ShowUpgrade(chip_to_buy);
    }

    public void ClearSelect()
    {
        // Deseleccionar objeto seleccionado
        for (int i = 0; i < display_p.childCount; ++i)
        {
            if (display_p.GetChild(i).GetComponent<SChipData>().data.id == chip_to_buy.id)
            {
                display_p.GetChild(i).GetComponent<SChipData>().DeselectChip();
                chip_to_buy.id = "";
                break;
            }
        }

        // Cambiar ventana de información
        info_p.GetComponent<InfoPanel>().ShowDialogue();
    }

    // Compra del objeto seleccionado
    public void BuyItem()
    {
        ChipData bought = new ChipData();
        bought.Clone(chip_to_buy); 
        // Restar precio a dinero
        money_p.GetComponent<MoneyPanel>().SpendMoney(bought.price);
        // Incluir objeto en inventario
        inventory_chips.chips.Add(bought);

        // Eliminar objeto físico de tienda
        for(int i = 0; i < display_p.childCount; ++i)
        {
            if(display_p.GetChild(i).GetComponent<SChipData>().data.id == chip_to_buy.id)
            {
                Destroy(display_p.GetChild(i).gameObject);
                break;
            }
        }
        // Eliminar datos de objeto de tienda
        int index = store_chips.chips.FindIndex(x => x.id == chip_to_buy.id);
        store_chips.chips.RemoveAt(index);
        // Reiniciar selección
        chip_to_buy.id = "";
    }

    // Comprobar si el jugador tiene dinero suficiente para cada objeto.
    void ItemPurchasable()
    {
        for (int i = 0; i < display_p.childCount; ++i)
        {
            if(PlayerManager.Instance.money < display_p.GetChild(i).GetComponent<SChipData>().data.price)
            {
                display_p.GetChild(i).GetComponent<SChipData>().MakeUnpurchaseable();
            }
        }
    }
}
