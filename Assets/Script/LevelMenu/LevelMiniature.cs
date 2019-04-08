using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMiniature : MonoBehaviour, IPointerDownHandler
{
    public LevelData data;

    Transform outline_i;
    //Transform locked_i;
    //Transform unlocked_i;

    private void Start()
    {
        outline_i = transform.Find("Outline");
       // locked_i = transform.Find("Locked");
     //   unlocked_i = transform.Find("Unlocked");

        if(!data.unlocked)
        {
            Darken dark = gameObject.AddComponent<Darken>();
            dark.DarkenColor(0.5f);
        }
    }

    public void OnPointerDown(PointerEventData p_data)
    {
        Debug.Log("OnPointerDown");

        if (data.unlocked)
        {
            if (LevelMenuManager.Instance.MakeSelection(data))
            {
                outline_i.gameObject.SetActive(true);
            }
        }
    }

    void OnMouseDown()
    {
        Debug.Log("OnMouseDown");

        if (data.unlocked)
        {
            if (LevelMenuManager.Instance.MakeSelection(data))
            {
                outline_i.gameObject.SetActive(true);
            }
        }
    }

    public void Deselect()
    {
        outline_i.gameObject.SetActive(false);
    }
}
