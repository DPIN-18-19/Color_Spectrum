using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    Transform dialogue_t;
    Transform upgrade_i;
    Transform weapon_i;
    Transform ability_i;

    private void Start()
    {
        dialogue_t = transform.Find("DialogueFill");
        upgrade_i = transform.Find("UpgradeInfo");
        weapon_i = transform.Find("WeaponInfo");
        ability_i = transform.Find("AbilityInfo");
    }

    public void ShowDialogue()
    {
        ClearInfo();
        dialogue_t.gameObject.SetActive(true);
    }

    public void ShowChip(SChipData to_show)
    {
        ClearInfo();
        switch (to_show.schip_type)
        {
            case SChipData.SChipType.Upgrade:
                ShowUpgrade(to_show.u_data);
                break;
            case SChipData.SChipType.Weapon:
                ShowGun(to_show.g_data);
                break;
            case SChipData.SChipType.Ability:
                ShowAbility(to_show.a_data);
                break;
        }
    }

    void ShowUpgrade(ChipData upgrade)
    {
        upgrade_i.gameObject.SetActive(true);

        upgrade_i.transform.Find("Name_t").GetComponent<TextMeshProUGUI>().text = upgrade.name;
        upgrade_i.transform.Find("Price_t").GetComponent<TextMeshProUGUI>().text = upgrade.price.ToString();
        upgrade_i.transform.Find("PlayerStat_t").GetComponent<TextMeshProUGUI>().text = "";
        upgrade_i.transform.Find("WeaponStat_t").GetComponent<TextMeshProUGUI>().text = "";

        for (int i = 0; i < upgrade.player_stats.Count; ++i)
        {
            upgrade_i.transform.Find("PlayerStat_t").GetComponent<TextMeshProUGUI>().text += upgrade.player_stats[i].stat_name + " " + upgrade.player_stats[i].stat_value + "\n";
        }

        for (int i = 0; i < upgrade.weapon_stats.Count; ++i)
        {
            upgrade_i.transform.Find("WeaponStat_t").GetComponent<TextMeshProUGUI>().text += upgrade.weapon_stats[i].stat_name + " " + upgrade.weapon_stats[i].stat_value + "\n";
        }
    }

    void ShowGun(GunData weapon)
    {
        weapon_i.gameObject.SetActive(true);

        weapon_i.transform.Find("Name_t").GetComponent<TextMeshProUGUI>().text = weapon.name;
        weapon_i.transform.Find("Price_t").GetComponent<TextMeshProUGUI>().text = weapon.price.ToString();
        
        weapon_i.transform.Find("Speed_t").GetComponent<TextMeshProUGUI>().text = weapon.speed.ToString();
        weapon_i.transform.Find("Damage_t").GetComponent<TextMeshProUGUI>().text = weapon.damage.ToString();
        weapon_i.transform.Find("Range_t").GetComponent<TextMeshProUGUI>().text = weapon.range.ToString();
    }

    void ShowAbility(AbilityData ability)
    {
        ability_i.gameObject.SetActive(true);

        ability_i.transform.Find("Name_t").GetComponent<TextMeshProUGUI>().text = ability.name;
        ability_i.transform.Find("Price_t").GetComponent<TextMeshProUGUI>().text = ability.price.ToString();

    }

    void ClearInfo()
    {
        dialogue_t.gameObject.SetActive(false);
        upgrade_i.gameObject.SetActive(false);
        weapon_i.gameObject.SetActive(false);
        ability_i.gameObject.SetActive(false);
    }
}
