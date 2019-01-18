using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Hoverable : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    Transform canvas;

    public GameObject hover_tooltip;
    GameObject my_hover_tooltip;

    void Start()
    {
        canvas = GetComponentInParent<Canvas>().transform;
    }

    public void OnPointerEnter(PointerEventData p_event_data)
    {
        my_hover_tooltip = Instantiate(hover_tooltip, p_event_data.position, Quaternion.identity);
        my_hover_tooltip.transform.SetParent(canvas);
    }

    public void OnPointerExit(PointerEventData p_event_data)
    {
        Destroy(my_hover_tooltip);
    }
}
