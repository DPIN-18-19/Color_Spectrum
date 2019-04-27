using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockItems
{
    [Header("Nombre de nivel")]
    public string level_name;           // Nivel donde se desbloquea el contenido

    // Desbloqueo por puntos
    [Header("Desbloqueables por puntos")]
    public List<string> upgrade_ids;
    public List<string> weapon_ids;
    public List<string> ability_ids;
    public int weight;

    // Desbloqueo en tienda
    [Header("Desbloqueables para tienda")]
    public List<string> s_upgrade_ids;
    public List<string> s_weapon_ids;
    public List<string> s_ability_ids;

    [Header("Estado de desbloqueable")]
    public bool unlocked = false;       // El item de nivel fue desbloqueado
    public bool s_unlocked = false;     // El contenido de tienda fue desbloqueado
}
