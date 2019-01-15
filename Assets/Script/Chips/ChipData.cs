﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ChipData
{
    public string id;                      // Identificador
    public string name;                    // Nombre de chip
    public List<BonusStat> player_stats;   // Estadisticas nuevas jugador
    public List<BonusStat> weapon_stats;   // Estadisticas nuevas arma
}

