using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRenderer : MonoBehaviour
{
    PlayerController player;

    //Renderers to change
    [SerializeField]
    public List<Renderer> renderersToChangeColor;

    // Player's material
    Material cur_mat;
    int cur_color;

    // Material Neutro
    public Material Yellow_Material;
    public Material Blue_Material;
    public Material Pink_Material;

    // Material Dano
    public Material DamageYellowMaterial;
    public Material DamageBlueMaterial;
    public Material DamagePinkMaterial;

    // Material Curar
    public Material HealthYellowMaterial;
    public Material HealthBlueMaterial;
    public Material HealthPinkMaterial;

    // Material Glitch
    public Material BlackGlitchYellowMaterial;
    public Material BlackGlitchBlueMaterial;
    public Material BlackGlitchPinkMaterial;

    public Material BlackYellowMaterial;
    public Material BlackBlueMaterial;
    public Material BlackPinkMaterial;

    // Use this for initialization
    void Start ()
    {
        // Subscribe to event
        ColorChangingController.Instance.ToYellow += ChangeToYellow;
        ColorChangingController.Instance.ToCyan += ChangeToCyan;
        ColorChangingController.Instance.ToMagenta += ChangeToMagenta;
    }

    void ApplyColor()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = cur_mat;    // Apply player material
        }
    }

    public void ChangeToYellow()
    {
        cur_mat = Yellow_Material;
        cur_color = 0;
        ApplyColor();
    }

    public void ChangeToCyan()
    {
        cur_mat = Blue_Material;      // Apply player material
        cur_color = 1;
        ApplyColor();
    }

    public void ChangeToMagenta()
    {
        cur_mat = Pink_Material;      // Apply player material
        cur_color = 2;
        ApplyColor();
    }
    
    // Jaime functions
    // Reset Colors
    public void ResetColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = Yellow_Material;    // Apply player material
                break;
            case 1:
                cur_mat = Blue_Material;    // Apply player material
                break;
            case 2:
                cur_mat = Pink_Material;    // Apply player material
                break;
        }
        ApplyColor();
    }

    // Damage
    public void DamageColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = DamageYellowMaterial;    // Apply player material
                break;
            case 1:
                cur_mat = DamageBlueMaterial;    // Apply player material
                break;
            case 2:
                cur_mat = DamagePinkMaterial;    // Apply player material
                break;
        }
        ApplyColor();
    }

    // Health Material
    public void HealthColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = HealthYellowMaterial;    // Apply player material
                break;
            case 1:
                cur_mat = HealthBlueMaterial;    // Apply player material
                break;
            case 2:
                cur_mat = HealthPinkMaterial;    // Apply player material
                break;
        }
        ApplyColor();
    }

    // Black Material Glitch
    public void BlackGlitchColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = BlackGlitchYellowMaterial;    // Apply player material
                break;
            case 1:
                cur_mat = BlackGlitchBlueMaterial;    // Apply player material
                break;
            case 2:
                cur_mat = BlackGlitchPinkMaterial;    // Apply player material
                break;
        }
        ApplyColor();
    }

    // Black material
    public void BlackColor()
    {
        switch (cur_color)
        {
            case 0:
                cur_mat = BlackYellowMaterial;    // Apply player material
                break;
            case 1:
                cur_mat = BlackBlueMaterial;    // Apply player material
                break;
            case 2:
                cur_mat = BlackPinkMaterial;    // Apply player material
                break;
        }
        ApplyColor();
    }

    public void UpdateColor()
    {
        GetComponent<ColorChangingController>().ReColor();
    }
}
