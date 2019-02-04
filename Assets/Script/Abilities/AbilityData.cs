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
}
