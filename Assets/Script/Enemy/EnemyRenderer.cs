using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRenderer : MonoBehaviour
{
    //Renderers to change
    [SerializeField]
    public List<Renderer> renderersToChangeColor;

    // Material de enemigo
   protected Material cur_mat;
    [HideInInspector]
    public int cur_color;

    // Material Neutro
    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;

    // Material Dano
    public Material DamageBlueMaterial;
    public Material DamageYellowMaterial;
    public Material DamagePinkMaterial;

    //Outline outline;
    private void Start()
    {
        cur_color = GetComponent<Enemy>().GetColor();
        NeutralColor();
    }

    // Aplicar color a todos los elementos del enemigo
    protected virtual void ApplyColor()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = cur_mat;    // Aplicar color correcto
        }
    }

    // Neutral
    public virtual void NeutralColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = Yellow_Material;      // Neutro Amarillo
                break;
            case 1:
                cur_mat = Blue_Material;        // Neutro Cyan
                break;
            case 2:
                cur_mat = Pink_Material;        // Neutro Magenta
                break;
        }
        ApplyColor();
    }

    // Dano
    public virtual void DamageColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = DamageYellowMaterial;     // Dano Amarillo
                break;
            case 1:
                cur_mat = DamageBlueMaterial;       // Dano Cyan
                break;
            case 2:
                cur_mat = DamagePinkMaterial;       // Dano Magenta
                break;
        }
        ApplyColor();
    }
}
