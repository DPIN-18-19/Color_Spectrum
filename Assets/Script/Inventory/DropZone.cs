using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData p_event_data)
    {
        if (p_event_data.pointerDrag == null)
            return;

        // Nueva posible zona de retorno
        Draggable d = p_event_data.pointerDrag.GetComponent<Draggable>();
        if(d != null)
        {
            d.temp_parent = this.transform;
        }
    }

    public void OnPointerExit(PointerEventData p_event_data)
    {
        if (p_event_data.pointerDrag == null)
            return;

        // Recuperar zona anterior de retorno
        Draggable d = p_event_data.pointerDrag.GetComponent<Draggable>();
        if (d != null && d.temp_parent == this.transform)
        {
            d.temp_parent = d.org_parent;
        }
    }

    public void OnDrop(PointerEventData p_event_data)
    {
        // Nueva posicion de retorno es lugar donde se suelta
        Draggable d = p_event_data.pointerDrag.GetComponent<Draggable>();
        if(d != null)
        {
            d.org_parent = this.transform;
        }
    }
}
