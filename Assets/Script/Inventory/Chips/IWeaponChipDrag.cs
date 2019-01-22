using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class IWeaponChipDrag : IChipDrag
{
    //[HideInInspector]
    public IWeaponData w_data;

    GameObject weapon_slot = null;
    RectTransform weapon_panel = null;
    public Transform weapon_deck = null;

    public bool inside = false;
    
    // Use this for initialization
    void Start()
    {
        canvas = GetComponentInParent<Canvas>().transform;
        inv_deck = GameObject.Find("InventoryPanel").transform;
        ichip_data = GetComponent<IChipData>();

        weapon_panel = GameObject.Find("WeaponPanel").GetComponent<RectTransform>();
    }

    public override void OnBeginDrag(PointerEventData p_event_data)
    {
        base.OnBeginDrag(p_event_data);
        
        weapon_slot = Instantiate(gameObject);
        Destroy(weapon_slot.GetComponent<IChipDrag>());
        weapon_slot.GetComponent<CanvasGroup>().alpha = 0;
        weapon_slot.transform.SetParent(canvas);
        weapon_slot.transform.Find("WeaponForm").gameObject.SetActive(true);

        weapon_deck = org_deck;
    }

    protected override void RemoveChip()
    {
        base.RemoveChip();

        if(org_deck.GetComponentInParent<DropPanel>().panel_type == DropPanel.PanelType.Weapon)
            org_deck.GetComponent<WeaponPanel>().weapon_p_chips.i_weapon_chips.Remove(w_data);
    }

    public override void OnDrag(PointerEventData p_event_data)
    {
        base.OnDrag(p_event_data);

        UpdateWeaponSlot();
    }

    public override void OnEndDrag(PointerEventData p_event_data)
    {
        base.OnEndDrag(p_event_data);

        if (new_possible_deck.GetComponentInParent<DropPanel>().panel_type == DropPanel.PanelType.Weapon)
        {
            this.transform.SetSiblingIndex(weapon_slot.transform.GetSiblingIndex());
        }
        
        if (weapon_slot)
            Destroy(weapon_slot);
    }

    void UpdateWeaponSlot()
    {
        if (weapon_deck.GetComponentInParent<DropPanel>().panel_type == DropPanel.PanelType.Weapon)
        {
            Debug.Log("Updating weapon hole");
            weapon_slot.transform.SetParent(weapon_deck);

            // Posicion hueco en el orden
            int new_sibling_index = weapon_deck.childCount;

            // Actualizar hueco mientras se arrastra objeto
            for (int i = 0; i < weapon_deck.childCount; ++i)
            {
                if (this.transform.position.y > weapon_deck.GetChild(i).position.y)
                {
                    new_sibling_index = i;

                    if (weapon_slot.transform.GetSiblingIndex() < new_sibling_index)
                        new_sibling_index--;

                    break;
                }
            }

            // Actualizar posicion de hueco
            weapon_slot.transform.SetSiblingIndex(new_sibling_index);
        }
        else
            weapon_slot.transform.SetParent(canvas);
    }

    //bool IsAboveWeapon(Vector2 cursor)
    //{
    //    if (weapon_panel.rect.Contains(Input.mousePosition))// && cursor.x > weapon_panel.position.x - weapon_panel.sizeDelta.x)
    //    {
            
    //        //if (cursor.y < weapon_panel.position.y + weapon_panel.sizeDelta.y && cursor.y > weapon_panel.position.y - weapon_panel.sizeDelta.y)
    //        //{
    //            //Debug.Log("It's in. Cursor: " + cursor.x + " Panel left: " + (weapon_panel.position.x + weapon_panel.localScale.x));
    //            return true;
    //        //}
    //    }
        
    //    //Debug.Log("It's out. Cursor: " + cursor.x + " Panel left: " + (weapon_panel.position.x + weapon_panel.localScale.x));
    //    return false;
    //}
}
