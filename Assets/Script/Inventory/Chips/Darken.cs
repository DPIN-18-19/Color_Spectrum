using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Darken : MonoBehaviour
{
    List<Image> render_children;
    public float dark_percent = 0.4f;

    private void Awake()
    {
        render_children = new List<Image>();
        SearchRenderers();
    }

    // Use this for initialization
    void Start ()
    {
        //DarkenColor();
    }
	
    void SearchRenderers()
    {
        render_children.Clear();

        // Buscar todos los componentes de un arma que tengan "Renderer"
        if (GetComponentsInChildren<Image>().Length != 0)
            render_children.AddRange(GetComponentsInChildren<Image>());
    }

    public void DarkenColor(float dark)
    {
        dark_percent = dark;
        Debug.Log("Children " + render_children.Count);
        for (int i = 0; i < render_children.Count; ++i)
        {
            Debug.Log("hehe");
            render_children[i].color = new Color(render_children[i].color.r * (1 - dark_percent),
                                                render_children[i].color.g * (1 - dark_percent),
                                                render_children[i].color.b * (1 - dark_percent), render_children[i].color.a);
        }
    }
}
