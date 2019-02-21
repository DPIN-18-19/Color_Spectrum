using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField]
    private AbilityList i_abilities;
    [SerializeField]
    private ChipList i_upgrades;
    [SerializeField]
    private IWeaponChipList i_weapons;

    [SerializeField]
    private ChipList i_player;
    [SerializeField]
    private IWeaponChipList i_equipped_w;

    private void Awake()
    {
        Debug.Log("I was called");
        i_equipped_w.i_weapon_chips.Clear();
        i_equipped_w.i_weapon_chips.Add(i_weapons.i_weapon_chips[0]);
        i_equipped_w.i_weapon_chips.Add(i_weapons.i_weapon_chips[2]);
        i_equipped_w.i_weapon_chips.Add(i_weapons.i_weapon_chips[1]);
    }
}
