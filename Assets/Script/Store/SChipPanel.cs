using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SChipPanel : MonoBehaviour
{
    [SerializeField]
    private SChipList store_chips;                  // Chips en la tienda
    [SerializeField]
    private ChipList inventory_upgrades;            // Inventario bonus
    [SerializeField]
    private IWeaponChipList inventory_weapons;      // Inventario armas
    [SerializeField]
    private AbilityList inventory_abilities;

    public GameObject schip_mould;          // Molde de chip tienda

    Transform display_p;        // Ventana de chips
    Transform display_scroll;   // Scroll de ventana
    Button buy_b;               // Botón de compra
    Transform money_p;          // Ventana de dinero
    Transform info_p;           // Ventana de información de producto

    // Chip seleccionado
    public SChipData chip_to_buy;

    public bool select_click = false;   // Click de selección. Evitar varias operaciones con el mismo click.
    public bool allow_click = true;     // Permitir selección/deselección en ciertas áreas

    // Scroll
    public int scroll_min;              // Minimo numero de filas para incluir scroll
    public float display_step;          // Tamaño de un aumento de ventana display
    float org_height;
    bool is_loading = true;
    float load_c = 0.2f;

    private void Awake()
    {
        // Inicialización
        display_p = transform.Find("DisplayPanel");
        display_scroll = transform.parent.Find("DisplayScroll");
        buy_b = transform.parent.Find("BuyButton").GetComponent<Button>();
        money_p = transform.parent.Find("MoneyPanel");
        info_p = transform.parent.Find("InfoPanel");
       
        LoadChips();
    }

    void LoadChips()
    {
        for(int i = 0; i < store_chips.schip_l.Count; ++i)
        {
            GameObject n_chip = Instantiate(schip_mould);
            n_chip.transform.SetParent(display_p);
            n_chip.GetComponent<StoreChip>().data.Clone(store_chips.schip_l[i]);
            n_chip.GetComponent<StoreChip>().Init();
            //n_chip.GetComponent<StoreChip>().u_data.Clone(store_chips.chips[i]);
        }

        org_height = display_p.GetComponent<RectTransform>().sizeDelta.y;

        // Comprobar a que objetos alcanza el dinero
        ItemPurchasable();
        // Ajustar tamano de ventana a lineas de objetos
        RowsOnDisplay();
    }

    private void Update()
    {
        if (is_loading)
            IsLoading();

        if (Input.GetMouseButtonDown(0))
        {
            if (chip_to_buy.store_id != 0 && !select_click && allow_click)
            {
                ClearSelect();
            }
        }
        
        if(Input.GetMouseButtonUp(0))
            select_click = false;

        // Actualizar botón de compra
        if (chip_to_buy.store_id == 0)
            buy_b.interactable = false;
        else
            buy_b.interactable = true;
    }

    public void MakeSelection(SChipData selected)
    {
        // Guardar datos de selección
        chip_to_buy.Clone(selected);
        // Mostrar datos en ventana de información
        info_p.GetComponent<InfoPanel>().ShowChip(chip_to_buy);
    }

    public void ClearSelect()
    {
        // Deseleccionar objeto seleccionado
        for (int i = 0; i < display_p.childCount; ++i)
        {
            if (display_p.GetChild(i).GetComponent<StoreChip>().data.store_id == chip_to_buy.store_id)
            {
                display_p.GetChild(i).GetComponent<StoreChip>().DeselectChip();
                chip_to_buy.store_id = 0;
                break;
            }
        }

        // Cambiar ventana de información
        info_p.GetComponent<InfoPanel>().ShowDialogue();
    }

    // Compra del objeto seleccionado
    public void BuyItem()
    {
        SChipData bought = new SChipData();
        bought.Clone(chip_to_buy);
        // Restar precio a dinero
        money_p.GetComponent<MoneyPanel>().SpendMoney(bought.price);
        // Incluir objeto en inventario
        AddToInventory(bought);

        // Eliminar objeto físico de tienda
        for (int i = 0; i < display_p.childCount; ++i)
        {
            if (display_p.GetChild(i).GetComponent<StoreChip>().data.store_id == chip_to_buy.store_id)
            {
                Destroy(display_p.GetChild(i).gameObject);
                break;
            }
        }
        // Eliminar datos de objeto de tienda
        int index = store_chips.schip_l.FindIndex(x => x.store_id == chip_to_buy.store_id);
        store_chips.schip_l.RemoveAt(index);
        // Reiniciar selección
        chip_to_buy.store_id = 0;

        ItemPurchasable();
        RowsOnDisplay();
        ClearSelect();
    }

    void AddToInventory(SChipData to_add)
    {
        switch(to_add.schip_type)
        {
            case SChipData.SChipType.Upgrade:
                inventory_upgrades.chips.Add(to_add.u_data);
                break;
            case SChipData.SChipType.Weapon:
                IWeaponData weapon = new IWeaponData();
                weapon.NewGun(to_add.g_data);
                inventory_weapons.i_weapon_chips.Add(weapon);
                break;
            case SChipData.SChipType.Ability:
                inventory_abilities.abi_list.Add(to_add.a_data);
                break;
        }
    }

    // Comprobar si el jugador tiene dinero suficiente para cada objeto.
    void ItemPurchasable()
    {
        for (int i = 0; i < display_p.childCount; ++i)
        {
            if (PlayerManager.Instance)
                if (PlayerManager.Instance.money < display_p.GetChild(i).GetComponent<StoreChip>().data.price)
                {
                    display_p.GetChild(i).GetComponent<StoreChip>().MakeUnpurchaseable();
                }
        }
    }

    void RowsOnDisplay()
    {
        // Numero de items por fila
        float row_size = (float)display_p.GetComponent<GridLayoutGroup>().constraintCount;
        int num_rows = Mathf.CeilToInt(display_p.childCount / row_size);

        // No hace falta scroll
        if (num_rows < scroll_min)
        {
            display_scroll.gameObject.SetActive(false);
            display_p.GetComponent<RectTransform>().sizeDelta = new Vector2(display_p.GetComponent<RectTransform>().sizeDelta.x, org_height);
        }
        else
        {
            display_scroll.gameObject.SetActive(true);
            // Calculate height of the new rect
            display_p.GetComponent<RectTransform>().sizeDelta = new Vector2(display_p.GetComponent<RectTransform>().sizeDelta.x, display_step * num_rows);
        }
    }

    void IsLoading()
    {
        load_c -= Time.deltaTime;
        // Colocar la barra al inicio. La barra se reajusta en el segundo frame tras la rescalación
        display_scroll.GetComponent<Scrollbar>().value = 1.0f;

        if (load_c < 0)
            is_loading = false;
    }
}
