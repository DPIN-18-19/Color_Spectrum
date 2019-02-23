using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SChipData
{
    public int store_id;

    public enum SChipType
    {
        Upgrade,        // Normal
        Weapon,         // Chip de arma
        Ability         // Chip de habilidad
    }
    public SChipType schip_type;      // Clase de chip
    public ChipData u_data;         // Datos de upgrade
    public GunData g_data;          // Datos de arma
    public AbilityData a_data;      // Datos de habilidad

    public float price;

    public SChipData()
    {
        store_id = 0;

        u_data = new ChipData();
        g_data = new GunData();
        a_data = new AbilityData();

        price = 0;
    }

    public void Clone(SChipData other)
    {
        store_id = other.store_id;
        schip_type = other.schip_type;

        switch (schip_type)
        {
            case SChipType.Upgrade:
                u_data.Clone(other.u_data);
                price = u_data.price;
                break;
            case SChipType.Weapon:
                g_data.Clone(other.g_data);
                price = g_data.price;
                break;
            case SChipType.Ability:
                //a_data.Clone(other.a_data);
                //price = a_data.price;
                break;
        }
    }

    //void SetChipData(ChipData other)
    //{
    //    schip_type = SChipType.Upgrade;
    //    u_data.Clone(other);
    //}
    //void SetChipData(GunData other)
    //{
    //    schip_type = SChipType.Weapon;
    //    g_data.Clone(other);
    //}
    //void SetChipData(AbilityData other)
    //{
    //    schip_type = SChipType.Ability;
    //    a_data.Clone(other);
    //}
}
