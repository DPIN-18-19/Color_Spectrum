using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LevelMiniature : MonoBehaviour, IPointerDownHandler
{
    public LevelData data;

    private void Start()
    {
        Debug.Log("Scene 6 was loaded");   
    }

    public void OnPointerDown(PointerEventData p_data)
    {
        LevelMenuManager.Instance.MakeSelection(data);
    }
}
