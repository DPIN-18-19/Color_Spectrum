﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryChip : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum ChipType
    {
        Upgrade,
        Weapon,
        Ability
    }
    public ChipType chip_type;

    public Transform org_deck = null;         // Area de retorno original
    public Transform new_possible_deck = null;        // Nueva area de retorno
    Transform canvas;

    GameObject shadow_copy = null;              // Area hueco

    void Start()
    {
        canvas = GetComponentInParent<Canvas>().transform;
    }

    public void OnBeginDrag(PointerEventData p_event_data)
    {
        // Comprobar si el objeto sale del inventario
        if (this.transform.parent.GetComponentInParent<InventoryPanel>().panel_type == InventoryPanel.PanelType.Inventory)
        {
            // Crear copia oscurecida
            shadow_copy = Instantiate(gameObject);
            shadow_copy.AddComponent<Darken>();                         // Componente oscurecer
            Destroy(shadow_copy.GetComponent<InventoryChip>());         // Eliminar componente que permite arrastrarlo
            shadow_copy.transform.SetParent(this.transform.parent);
            
            // Posicion de hueco
            shadow_copy.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

            // Recoger datos de lugar de retorno
            org_deck = this.transform.parent;
            new_possible_deck = org_deck;
        }

        this.transform.parent = canvas;   // Sacar fuera de area al objeto

        GetComponent<CanvasGroup>().blocksRaycasts = false;             // Permitir hacer raycast de puntero
    }

    public void OnDrag(PointerEventData p_event_data)
    {
        // Actualizar posicion del objeto arrastrandose
        transform.position = p_event_data.position;

        // Actualizar tablero donde aparece el hueco
        //if (shadow_copy.transform.parent != new_possible_deck)
          //  shadow_copy.transform.SetParent(new_possible_deck);

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

    public void OnEndDrag(PointerEventData p_event_data)
    {
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        ChipFilter();

        // Colocar objeto en panel de armas
        if (new_possible_deck.GetComponentInParent<InventoryPanel>().panel_type == InventoryPanel.PanelType.Weapon)
        {
            Transform weapon_panel = transform.Find("WeaponPanel");
            weapon_panel.gameObject.SetActive(true);

            Transform chip_form = transform.Find("ChipForm");
            chip_form.gameObject.SetActive(false);
        }

        if (new_possible_deck.GetComponentInParent<InventoryPanel>().panel_type == InventoryPanel.PanelType.Inventory)
        {
            // Colocar objeto en zona de retorno
            this.transform.parent = new_possible_deck;
            this.transform.SetSiblingIndex(shadow_copy.transform.GetSiblingIndex());
            Destroy(shadow_copy);
            
            if (chip_type == ChipType.Weapon)
            {
                Transform weapon_panel = transform.Find("WeaponPanel");
                weapon_panel.gameObject.SetActive(false);

                Transform chip_form = transform.Find("ChipForm");
                chip_form.gameObject.SetActive(true);
            }
        }
        else
        {
            // Colocar objeto en zona de retorno
            this.transform.parent = new_possible_deck;
            Transform equipped = shadow_copy.transform.Find("Equipped");
            equipped.gameObject.SetActive(true);
        }
    }

    void ChipFilter()
    {
        InventoryPanel.PanelType panel_type = new_possible_deck.GetComponentInParent<InventoryPanel>().panel_type;

        switch (chip_type)
        {
            case ChipType.Upgrade:
                if (panel_type == InventoryPanel.PanelType.Weapon || panel_type == InventoryPanel.PanelType.AbilitySlot)
                    new_possible_deck = org_deck;
                else
                    Debug.Log("Tryn'");
                break;
            case ChipType.Weapon:
                if(panel_type == InventoryPanel.PanelType.AbilitySlot || panel_type == InventoryPanel.PanelType.Player || panel_type == InventoryPanel.PanelType.WeaponSlot)
                    new_possible_deck = org_deck;
                break;
            case ChipType.Ability:
                Debug.Log("Trynss'");
                if (panel_type == InventoryPanel.PanelType.Weapon || panel_type == InventoryPanel.PanelType.Player || panel_type == InventoryPanel.PanelType.WeaponSlot)
                    new_possible_deck = org_deck;
                else
                    Debug.Log("Off you go");
                break;
            default:
                Debug.Log("Trysfn'");
                break;
        }
    }
}

