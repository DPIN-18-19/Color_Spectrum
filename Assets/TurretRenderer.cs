using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretRenderer : EnemyRenderer
{
    [Header("Torreta exclusivo")]
    public List<Renderer> renderersToChangeColor_Up;
    public Material Blue_Material_Up;
    public Material Yellow_Material_Up;
    public Material Pink_Material_Up;
    Material cur_mat_Up;
    [SerializeField]
  
    // Use this for initialization
    void Start () {
        cur_color = GetComponent<Enemy>().GetColor();
        NeutralColor();
    }
	

    public override void NeutralColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = Yellow_Material;      // Neutro Amarillo
                cur_mat_Up = Yellow_Material_Up;

                break;
            case 1:
                cur_mat = Blue_Material;        // Neutro Cyan
                cur_mat_Up = Blue_Material_Up;
                break;
            case 2:
                cur_mat = Pink_Material;        // Neutro Magenta
                cur_mat_Up = Pink_Material_Up;
                break;
        }
        ApplyColor();
    }
    protected override void ApplyColor()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = cur_mat;    // Aplicar color correcto

        }
        foreach (Renderer r in renderersToChangeColor_Up)
        {
            r.material = cur_mat_Up;    // Aplicar color correcto

        }
    }
    public override void DamageColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = DamageYellowMaterial;     // Dano Amarillo
                cur_mat_Up = DamageYellowMaterial;
                break;
            case 1:
                cur_mat = DamageBlueMaterial;       // Dano Cyan
                cur_mat_Up = DamageBlueMaterial;
                break;
            case 2:
                cur_mat = DamagePinkMaterial;       // Dano Magenta
                cur_mat_Up = DamagePinkMaterial;
                break;
        }
        ApplyColor();
    }
}
