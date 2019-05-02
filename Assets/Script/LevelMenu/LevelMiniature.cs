using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMiniature : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    LevelInfoPanel info_p;

    public LevelData data;

    Transform outline_i;
    //Transform locked_i;
    //Transform unlocked_i;
    bool selected;

    private void Start()
    {
        outline_i = transform.Find("Outline");
        // locked_i = transform.Find("Locked");
        // unlocked_i = transform.Find("Unlocked");

        if(!data.unlocked)
        {
            Darken dark = gameObject.AddComponent<Darken>();
            dark.DarkenColor(0.5f);
        }
    }

    public void OnPointerDown(PointerEventData p_data)
    {
        Debug.Log("OnPointerDown " + data.name);

        if (data.unlocked)
        {
            if (LevelMenuManager.Instance.MakeSelection(data))
            {
                outline_i.gameObject.SetActive(true);
                selected = true;
                LevelMenuManager.Instance.AllowDeselection(false);
            }
        }
    }

    public void OnPointerEnter(PointerEventData p_data)
    {
        if(selected)
        {
            LevelMenuManager.Instance.AllowDeselection(false);
        }
    }

    public void OnPointerExit(PointerEventData p_data)
    {
        if (selected)
        {
            LevelMenuManager.Instance.AllowDeselection(true);
        }
    }

    //void OnMouseDown()
    //{
    //    Debug.Log("OnMouseDown on " + data.name);

    //    if (data.unlocked)
    //    {
    //        if (LevelMenuManager.Instance.MakeSelection(data))
    //        {
    //            outline_i.gameObject.SetActive(true);
    //        }
    //    }
    //}

    public void Deselect()
    {
        Debug.Log("Making deselection " + data.name);
        outline_i.gameObject.SetActive(false);
        selected = false;
    }
}
