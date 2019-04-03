using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MenuLight : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler
{
    public GameObject Light;
    public GameObject GeneralLight;
    
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnPointerEnter(PointerEventData pdata)
    {
        Light.SetActive(true);
        GeneralLight.SetActive(false);
    }
    public void OnPointerExit(PointerEventData pdata)
    {
        Light.SetActive(false);
        GeneralLight.SetActive(true);
    }
}
