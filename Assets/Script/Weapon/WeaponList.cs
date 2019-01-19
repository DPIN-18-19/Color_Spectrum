using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "New Weapon Info", menuName = "Weapon Info", order = 0)]
public class WeaponList : ScriptableObject
{
    public List<GunData> weapon_list;         // Lista de armas en el juego
}
