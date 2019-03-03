using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponList : ScriptableObject
{
    public List<GunData> weapon_list;         // Lista de armas en el juego
    public List<GunData> equipped_weapons;    // Lista de armas equipadas en el jugador
}
