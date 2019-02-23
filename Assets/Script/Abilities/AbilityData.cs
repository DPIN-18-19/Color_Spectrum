using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class AbilityData
{
    // General Data
    public string id = "";
    public string name = "";
    public bool is_instant = true;

    public float cooldown_dur = 0.0f;
    public float active_dur = 0.0f;

    // UI Data
    public Sprite display_icon;

    // Inventory Data
    public float weight = 0.0f;
    public bool equipped;

    // Datos de tienda
    public float price;

    public void Clone(AbilityData other)
    {
        id = other.id;
        name = other.id;
        is_instant = other.is_instant;

        cooldown_dur = other.cooldown_dur;
        active_dur = other.active_dur;

        display_icon = other.display_icon;

        weight = other.weight;
        equipped = other.equipped;

        price = other.price;
    }
}
