using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class IChipDrag : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler//, IPointerEnterHandler, IPointerExitHandler
{
    //public enum ChipType
    //{
    //    Upgrade,        // Normal
    //    Weapon,         // Chip de arma
    //    Ability         // Chip de habilidad
    //}
    //public ChipType chip_type;      // Clase de chip

    //[HideInInspector]
    //public ChipData data;           // Contenido del chip

    public IChipData ichip_data;

    public Transform inv_deck = null;               // Area inventario
    public Transform org_deck = null;               // Area de retorno original
    public Transform new_possible_deck = null;      // Nueva area de retorno
    protected Transform canvas;                     // El canvas

    public GameObject shadow_copy = null;                  // Area hueco


    //public GameObject hover_tooltip;                // Objeto con el que se creará el tooltip
    //GameObject my_hover_tooltip;                    // Referencia al tooltip creado

    void Start()
    {
        canvas = GetComponentInParent<Canvas>().transform;          // Coger el canvas de la interfaz
        inv_deck = GameObject.Find("InventoryPanel").transform;     // Coger el área del inventario
        ichip_data = GetComponent<IChipData>();
    }

    public virtual void OnBeginDrag(PointerEventData p_event_data)
    {
        // Recoger datos de lugar de retorno
        org_deck = this.transform.parent;
        new_possible_deck = org_deck;
        
        RemoveChip();                                                   // Realizar operaciones necesarias al sacar coger un chip

        this.transform.SetParent(canvas);                                 // Sacar fuera de area al objeto
        GetComponent<CanvasGroup>().blocksRaycasts = false;             // Permitir hacer raycast de puntero
    }

    protected virtual void RemoveChip()
    {
        DropPanel.PanelType panel_type = org_deck.GetComponentInParent<DropPanel>().panel_type;

        switch(panel_type)
        {
            case DropPanel.PanelType.AbilitySlot:
                org_deck.GetComponent<AbilityPanel>().RemoveChipFromEquippedWeapon(ichip_data.data);
                break;
            case DropPanel.PanelType.Inventory:
            {
                // Crear copia oscurecida
                shadow_copy = Instantiate(gameObject);
                shadow_copy.AddComponent<Darken>();                         // Componente oscurecer
                Destroy(shadow_copy.GetComponent<IChipDrag>());         // Eliminar componente que permite arrastrarlo
                shadow_copy.transform.SetParent(inv_deck);

                // Posicion de hueco
                shadow_copy.transform.SetSiblingIndex(this.transform.GetSiblingIndex());
                break;
            }
            case DropPanel.PanelType.Player:
                org_deck.GetComponent<CharacterPanel>().character_p_chips.chips.Remove(ichip_data.data);
                break;
            case DropPanel.PanelType.Weapon:
                // Removal of weapon is done in IWeaponChipDrag
                //org_deck.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.Remove();
                break;
            case DropPanel.PanelType.WeaponSlot:
                org_deck.GetComponent<WeaponSlotPanel>().RemoveChipFromEquippedWeapon(ichip_data.data);
                break;
            default:
                break;
        }
    }

    public virtual void OnDrag(PointerEventData p_event_data)
    {
        // Actualizar posicion del objeto arrastrandose
        transform.position = p_event_data.position;

        // Actualizar tablero donde aparece el hueco de arma
        //if (weapon_slot.transform.parent != new_possible_deck)
        //    weapon_slot.transform.SetParent(canvas);

        // Posicion hueco en el orden
        //int new_sibling_index = new_possible_deck.childCount;

        // Actualizar hueco mientras se arrastra objeto
        //for (int i = 0; i < new_possible_deck.childCount; ++i)
        //{
        //    if (this.transform.position.x < new_possible_deck.GetChild(i).position.x)
        //    {
        //        new_sibling_index = i;

        //        if (shadow_copy.transform.GetSiblingIndex() < new_sibling_index)
        //            new_sibling_index--;

        //        break;
        //    }
        //}

        //// Actualizar posicion de hueco
        //shadow_copy.transform.SetSiblingIndex(new_sibling_index);
    }



    public virtual void OnEndDrag(PointerEventData p_event_data)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;

        // Colocar objeto en panel de armas
        if (new_possible_deck.GetComponentInParent<DropPanel>().panel_type == DropPanel.PanelType.Weapon)
        {
            // Cambiar forma a Arma
            Transform weapon_panel = transform.Find("WeaponForm");
            weapon_panel.gameObject.SetActive(true);
            Transform chip_form = transform.Find("ChipForm");
            chip_form.gameObject.SetActive(false);
            
            this.transform.SetParent(new_possible_deck);
            Transform equipped = shadow_copy.transform.Find("Equipped");
            equipped.gameObject.SetActive(true);
            inv_deck.GetComponentInParent<InventoryPanel>().EquipChip(ichip_data);
        }
        // Colocar objeto en inventario
        else if (new_possible_deck.GetComponentInParent<DropPanel>().panel_type == DropPanel.PanelType.Inventory)
        {
            // Colocar objeto en zona de retorno en posición de copia
            this.transform.SetParent(new_possible_deck);
            this.transform.SetSiblingIndex(shadow_copy.transform.GetSiblingIndex());
            Destroy(shadow_copy);

            // Desequipar chip
            inv_deck.GetComponentInParent<InventoryPanel>().UnequipChip(ichip_data);

            if (ichip_data.chip_type == IChipData.ChipType.Weapon)
            {
                // Cambiar forma a Chip
                Transform weapon_panel = transform.Find("WeaponForm");
                weapon_panel.gameObject.SetActive(false);
                Transform chip_form = transform.Find("ChipForm");
                chip_form.gameObject.SetActive(true);
            }
        }
        else
        {
            // Colocar objeto en zona de retorno
            this.transform.SetParent(new_possible_deck);

            // Equipar chip
            Transform equipped = shadow_copy.transform.Find("Equipped");
            equipped.gameObject.SetActive(true);
            inv_deck.GetComponentInParent<InventoryPanel>().EquipChip(ichip_data);
        }

    }
    
    //public void OnPointerEnter(PointerEventData p_event_data)
    //{
    //    // Evitar que se muestre un tooltip si ya se agarrando algo
    //    if (!Input.GetMouseButton(0))
    //    {
    //        my_hover_tooltip = Instantiate(hover_tooltip, p_event_data.position, Quaternion.identity);
    //        my_hover_tooltip.transform.SetParent(canvas);
    //    }
    //}

    //public void OnPointerExit(PointerEventData p_event_data)
    //{
    //    if(my_hover_tooltip)
    //        my_hover_tooltip.GetComponent<HoverTooltip>().Leave();
    //}
}

