using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TooltipInfo : MonoBehaviour
{
    public void ShowUpgrade(ChipData data)
    {
        transform.Find("Name_t").GetComponent<TextMeshProUGUI>().text = data.name;

        TextMeshProUGUI player_stats = transform.Find("Player_t").GetComponent<TextMeshProUGUI>();
        player_stats.text = "";
        TextMeshProUGUI weapon_stats = transform.Find("Weapon_t").GetComponent<TextMeshProUGUI>();
        weapon_stats.text = "";

        for (int i = 0; i < data.player_stats.Count; ++i)
        {
            player_stats.text += data.player_stats[i].stat_name + " " + data.player_stats[i].stat_value + "\n";
        }
        for (int i = 0; i < data.weapon_stats.Count; ++i)
        {
            weapon_stats.text += data.weapon_stats[i].stat_name + " " + data.weapon_stats[i].stat_value + "\n";
        }
    }

    public void ShowWeapon(IWeaponData data)
    {
        // Text data
        transform.Find("Name_t").GetComponent<TextMeshProUGUI>().text = data.base_gun.name;
        transform.Find("Damage_t").GetComponent<TextMeshProUGUI>().text = data.base_gun.damage.ToString();
        transform.Find("Range_t").GetComponent<TextMeshProUGUI>().text = data.base_gun.range.ToString();
        transform.Find("Speed_t").GetComponent<TextMeshProUGUI>().text = data.base_gun.speed.ToString();
        transform.Find("Weapon_i").GetComponent<Image>().sprite = data.base_gun.display_icon;
    }

    public void ShowAbility(AbilityData data)
    {
        transform.Find("Name_t").GetComponent<TextMeshProUGUI>().text = data.name;
        transform.Find("Description_t").GetComponent<TextMeshProUGUI>().text = data.tooltip_brief;
        transform.Find("Buffer_t").GetComponent<TextMeshProUGUI>().text = data.cooldown_dur.ToString();

        if (data.is_instant)
        {
            transform.Find("Duration").gameObject.SetActive(false);
        }
        else
        {
            transform.Find("Duration_t").GetComponent<TextMeshProUGUI>().text = data.active_dur.ToString();
        }
    }
}
