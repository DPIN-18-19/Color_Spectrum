using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Info", menuName = "Weapon Info", order = 0)]
public class WeaponList : ScriptableObject
{
    public List<GunData> weapons_l;         // Lista de armas en el juego

    public GunData SearchChipByName(string name)
    {
        for (int i = 0; i < weapons_l.Count; ++i)
        {
            if (weapons_l[i].name == name)
                return weapons_l[i];
        }

        return null;
    }
}
