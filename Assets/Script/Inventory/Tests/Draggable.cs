using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Transform org_parent = null;         // Area de retorno original
    public Transform temp_parent = null;        // Nueva area de retorno

    GameObject placeholder = null;              // Area hueco

    public void OnBeginDrag(PointerEventData p_event_data)
    {
        // Crear hueco de tamaño de objeto
        placeholder = new GameObject();
        placeholder.AddComponent<RectTransform>();
        placeholder.GetComponent<RectTransform>().sizeDelta = this.GetComponent<RectTransform>().sizeDelta;
        placeholder.transform.SetParent(this.transform.parent);
        LayoutElement le = placeholder.AddComponent<LayoutElement>();
        le.preferredHeight = this.GetComponent<LayoutElement>().preferredHeight;
        le.preferredWidth = this.GetComponent<LayoutElement>().preferredWidth;
        le.flexibleHeight = 0;
        le.flexibleWidth = 0;

        // Posicion de hueco
        placeholder.transform.SetSiblingIndex(this.transform.GetSiblingIndex());

        // Recoger datos de lugar de retorno
        org_parent = this.transform.parent;
        temp_parent = org_parent;
        this.transform.parent = this.transform.parent.parent;   // Sacar fuera de area al objeto

        GetComponent<CanvasGroup>().blocksRaycasts = false;     // Permitir hacer raycast de puntero
    }

    public void OnDrag(PointerEventData p_event_data)
    {
        // Actualizar posicion del objeto arrastrandose
        transform.position = p_event_data.position;

        // Actualizar lugar donde aparece el hueco
        if (placeholder.transform.parent != temp_parent)
            placeholder.transform.SetParent(temp_parent);

        // Posicion hueco en el orden
        int new_sibling_index = temp_parent.childCount;

        // Actualizar hueco mientras se arrastra objeto
        for(int i = 0; i < temp_parent.childCount; ++i)
        {
            if(this.transform.position.x < temp_parent.GetChild(i).position.x)
            {
                new_sibling_index = i;

                if (placeholder.transform.GetSiblingIndex() < new_sibling_index)
                    new_sibling_index--;

                break;
            }
        }

        // Actualizar posicion de hueco
        placeholder.transform.SetSiblingIndex(new_sibling_index);
    }

    public void OnEndDrag(PointerEventData p_event_data)
    {
        // Colocar objeto en zona de retorno
        this.transform.parent = org_parent;
        GetComponent<CanvasGroup>().blocksRaycasts = true;
        // Colocar objeto en hueco
        this.transform.SetSiblingIndex(placeholder.transform.GetSiblingIndex());
        Destroy(placeholder);
    }
}
