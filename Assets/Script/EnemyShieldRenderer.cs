using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShieldRenderer : EnemyRenderer
{

    [Header("Escudo exclusivo")]
    public List<Renderer> renderersToChangeColor_Chest;
    public Material Blue_Material_Chest;
    public Material Yellow_Material_Chest;
    public Material Pink_Material_Chest;
    Material cur_mat_Chest;


   
    [SerializeField]

    // Use this for initialization
    void Start()
    {
        cur_color = GetComponent<Enemy>().GetColor();
        NeutralColor();
    }


    public override void NeutralColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = Yellow_Material;      // Neutro Amarillo
               
                cur_mat_Chest = Yellow_Material_Chest;

                break;
            case 1:
                cur_mat = Blue_Material;        // Neutro Cyan
               
                cur_mat_Chest = Blue_Material_Chest;
                break;
            case 2:
                cur_mat = Pink_Material;        // Neutro Magenta
              
                cur_mat_Chest = Pink_Material_Chest;
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
        foreach (Renderer r in renderersToChangeColor_Chest)
        {
            r.material = cur_mat_Chest;    // Aplicar color correcto

        }
    }
    public override void DamageColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = DamageYellowMaterial;     // Dano Amarillo
                cur_mat_Chest = DamageYellowMaterial;
                break;
            case 1:
                cur_mat = DamageBlueMaterial;       // Dano Cyan
                cur_mat_Chest = DamageBlueMaterial;
                break;
            case 2:
                cur_mat = DamagePinkMaterial;       // Dano Magenta
                cur_mat_Chest = DamagePinkMaterial;
                break;
        }
        ApplyColor();
    }
}
