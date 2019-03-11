using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockMan : MonoBehaviour
{
    public static UnlockMan Instance { get; private set; }

    public UnlockList unlock_per_level;

    public ChipList all_upgrades;           // Todos los bonus del juego 
    public WeaponList all_weapons;          // Todas las armas del juego
    public AbilityList all_abilities;       // Todas las habilidades del juego

    public ChipList inv_upgrades;           // Bonus de inventario
    public IWeaponChipList inv_weapons;     // Armas de inventario
    public AbilityList inv_abilities;       // Habilidades de inventario

    public SChipList store_chips;           // Objetos de tienda

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    // Descbloqueo mediante puntuacion
    public void ScoreLevelUnlock(string level_name)
    {
        int index = GetUnlockIndex(level_name);

        if (!unlock_per_level.unlock_l[index].unlocked)
        {
            int it;     // Iterador
            unlock_per_level.unlock_l[index].unlocked = true;

            // Insertar chips bonus
            for (it = 0; it < unlock_per_level.unlock_l[index].upgrade_ids.Count; ++it)
            {
                ChipData data = new ChipData();
                data.Clone(all_upgrades.SearchChipById(unlock_per_level.unlock_l[index].upgrade_ids[it]));
                inv_upgrades.chips.Add(data);
            }

            // Insertar chips armas
            for (it = 0; it < unlock_per_level.unlock_l[index].weapon_ids.Count; ++it)
            {
                IWeaponData data = new IWeaponData();
                data.NewGun(all_weapons.SearchChipByName(unlock_per_level.unlock_l[index].weapon_ids[it]));
                inv_weapons.i_weapon_chips.Add(data);
            }

            // Insertar chips habilidades
            for (it = 0; it < unlock_per_level.unlock_l[index].ability_ids.Count; ++it)
            {
                AbilityData data = new AbilityData();
                data.Clone(all_abilities.SearchChipById(unlock_per_level.unlock_l[index].ability_ids[it]));
                inv_abilities.abi_list.Add(data);
            }
        }
    }

    // Desbloqueo de objetos de tienda
    public void StoreUnlock(string level_name)
    {
        int index = GetUnlockIndex(level_name);

        if (!unlock_per_level.unlock_l[index].s_unlocked)
        {
            int it;     // Iterador
            unlock_per_level.unlock_l[index].s_unlocked = true;

            // Insertar chips bonus
            for(it = 0; it < unlock_per_level.unlock_l[index].s_upgrade_ids.Count; ++it)
            {
                SChipData s_data = new SChipData();
                s_data.store_id = GenerateStoreId();
                s_data.u_data.Clone(all_upgrades.SearchChipById(unlock_per_level.unlock_l[index].s_upgrade_ids[it]));
                s_data.schip_type = SChipData.SChipType.Upgrade;
                store_chips.schip_l.Add(s_data);
            }

            // Insertar chips armas
            for (it = 0; it < unlock_per_level.unlock_l[index].s_weapon_ids.Count; ++it)
            {
                SChipData s_data = new SChipData();
                s_data.store_id = GenerateStoreId();
                s_data.g_data.Clone(all_weapons.SearchChipByName(unlock_per_level.unlock_l[index].s_weapon_ids[it]));
                s_data.schip_type = SChipData.SChipType.Weapon;
                store_chips.schip_l.Add(s_data);
            }

            // Insertar chips habilidades
            for (it = 0; it < unlock_per_level.unlock_l[index].s_ability_ids.Count; ++it)
            {
                SChipData s_data = new SChipData();
                s_data.store_id = GenerateStoreId();
                s_data.a_data.Clone(all_abilities.SearchChipById(unlock_per_level.unlock_l[index].s_ability_ids[it]));
                s_data.schip_type = SChipData.SChipType.Ability;
                store_chips.schip_l.Add(s_data);
            }
        }
    }

    // Buscar indice de desbloqueables por nombre de nivel
    int GetUnlockIndex(string level_name)
    {
        for(int i = 0; i < unlock_per_level.unlock_l.Count; ++i)
        {
            if (unlock_per_level.unlock_l[i].level_name == level_name)
                return i;
        }
        return -1;
    }

    // Generar identificador para objeto de tienda
    int GenerateStoreId()
    {
        bool valid_id = false;
        int store_id = 0;

        do
        {
            store_id = (int)(Random.value * 1000.0f);

            if (store_chips.SearchChipById(store_id) == null)
                valid_id = true;
        }
        while (!valid_id);

        return store_id;
    }
}
