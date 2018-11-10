using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomCursorController : MonoBehaviour
{
    public CameraController cam;

    public Canvas my_canvas;

	// Use this for initialization
	void Start ()
    {
        my_canvas = GetComponent<Canvas>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        MoveCursor();
    }
    
    void MoveCursor()
    {
        Vector2 pos;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(my_canvas.transform as RectTransform,
                Input.mousePosition, my_canvas.worldCamera, out pos);
        transform.position = my_canvas.transform.TransformPoint(pos);
    }

    
}
