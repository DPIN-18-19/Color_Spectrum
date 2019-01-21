using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Chip Info", menuName = "Weapon Chip Info", order = 0)]
public class InventoryWeaponList : ScriptableObject
{
    public List<InventoryWeaponData> i_weapon_chips;
}
