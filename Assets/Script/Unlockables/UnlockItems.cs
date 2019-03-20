using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class UnlockItems
{
    public string level_name;           // Nivel donde se desbloquea el contenido

    // Desbloqueo por puntos
    public List<string> upgrade_ids;
    public List<string> weapon_ids;
    public List<string> ability_ids;

    // Desbloqueo en tienda
    public List<string> s_upgrade_ids;
    public List<string> s_weapon_ids;
    public List<string> s_ability_ids;

    public bool unlocked = false;       // El item de nivel fue desbloqueado
    public bool s_unlocked = false;     // El contenido de tienda fue desbloqueado
}
