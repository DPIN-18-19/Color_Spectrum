using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventoryWeaponData
{
    public GunData base_gun;
    float org_cdc, org_dmg, org_rng;
    public List<ChipData> added_chips;
    public int spaces;

    private void Start()
    {
        org_cdc = base_gun.cadence;
        org_dmg = base_gun.damage;
        org_rng = base_gun.range;
    }

    void ApplyChips()
    {
        for(int i = 0; i < added_chips.Capacity; ++i)
        {
            for(int j = 0; j < added_chips[i].weapon_stats.Capacity; ++j)
            {
                switch(added_chips[i].weapon_stats[j].stat_name)
                {
                    case "Cadence":
                        base_gun.cadence += added_chips[i].weapon_stats[j].stat_value;
                        break;
                    case "Damage":
                        base_gun.damage += added_chips[i].weapon_stats[j].stat_value;
                        break;
                    case "Range":
                        base_gun.range += added_chips[i].weapon_stats[j].stat_value;
                        break;
                    default:
                        break;
                }
            }
        }
    }

    void ResetGun()
    {
        base_gun.cadence = org_cdc;
        base_gun.damage = org_dmg;
        base_gun.range = org_rng;
    }

    void AddChip(ChipData n_chip)
    {
        added_chips.Add(n_chip);

        for (int i = 0; i < n_chip.weapon_stats.Capacity; ++i)
        {
            switch (n_chip.weapon_stats[i].stat_name)
            {
                case "Cadence":
                    base_gun.cadence += n_chip.weapon_stats[i].stat_value;
                    break;
                case "Damage":
                    base_gun.damage += n_chip.weapon_stats[i].stat_value;
                    break;
                case "Range":
                    base_gun.range += n_chip.weapon_stats[i].stat_value;
                    break;
                default:
                    break;
            }
        }
    }

    void TakeOutChip(ChipData o_chip)
    {
        int take_out = added_chips.IndexOf(o_chip);

        if (take_out < added_chips.Capacity)
        {
            for (int i = 0; i < o_chip.weapon_stats.Capacity; ++i)
            {
                switch (o_chip.weapon_stats[i].stat_name)
                {
                    case "Cadence":
                        base_gun.cadence -= o_chip.weapon_stats[i].stat_value;
                        break;
                    case "Damage":
                        base_gun.damage -= o_chip.weapon_stats[i].stat_value;
                        break;
                    case "Range":
                        base_gun.range -= o_chip.weapon_stats[i].stat_value;
                        break;
                    default:
                        break;
                }
            }

            added_chips.Remove(o_chip);
        }
    }

    void GetWeight()
    {

    }
}
