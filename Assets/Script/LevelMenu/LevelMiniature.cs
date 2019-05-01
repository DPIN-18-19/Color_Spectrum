using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMiniature : MonoBehaviour, IPointerDownHandler, IPointerEnterHandler, IPointerExitHandler
{
    public LevelData data;

    public Material MaterialColor;
     Color colour;
    private bool select;
    public float Intesity;
    Transform outline_i;
    //Transform locked_i;
    //Transform unlocked_i;

    private void Start()
    {
        colour = MaterialColor.GetColor("_EmissionColor");
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
        select = true;
        if (data.unlocked)
        {
            if (LevelMenuManager.Instance.MakeSelection(data))
            {
                outline_i.gameObject.SetActive(true);
            }
        }
    }
    

    public void OnPointerEnter(PointerEventData p_data)
    {

        if (select == false)
        {

            colour *= Intesity;
            MaterialColor.SetColor("_EmissionColor", colour);
        }
            
        
    }
    public void OnPointerExit(PointerEventData p_data)
    {
        if (select == false)
        {
            colour /= Intesity;
            MaterialColor.SetColor("_EmissionColor", colour);
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
        colour /= Intesity;
        MaterialColor.SetColor("_EmissionColor", colour);
        select = false;
        outline_i.gameObject.SetActive(false);
    }
}
