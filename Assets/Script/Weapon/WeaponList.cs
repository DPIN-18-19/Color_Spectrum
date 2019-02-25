using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Info", menuName = "Weapon Info", order = 0)]
public class WeaponList : ScriptableObject
{
    public List<GunData> weapon_list;         // Lista de armas en el juego

    public GunData SearchChipByName(string name)
    {
        for (int i = 0; i < weapon_list.Count; ++i)
        {
            if (weapon_list[i].name == name)
                return weapon_list[i];
        }

        return null;
    }
}
