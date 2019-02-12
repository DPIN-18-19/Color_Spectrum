using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChipData
{
    // Datos del chips
    public string id;                      // Identificador
    public string name;                    // Nombre de chip
    public float weight;
    public List<BonusStat> player_stats;   // Estadisticas nuevas jugador
    public List<BonusStat> weapon_stats;   // Estadisticas nuevas arma
    
    // Datos de inventario
    public bool equipped;

    // Datos de tienda
    public float price;                     // Campo de tienda
}

