using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class InfoPanel : MonoBehaviour
{
    Transform dialogue_t;
    Transform upgrade_i;

    private void Start()
    {
        dialogue_t = transform.Find("DialogueFill");
        upgrade_i = transform.Find("UpgradeInfo");
    }

    public void ShowDialogue()
    {
        dialogue_t.gameObject.active = true;
        upgrade_i.gameObject.active = false;
    }

    public void ShowUpgrade(ChipData upgrade)
    {
        dialogue_t.gameObject.active = false;
        upgrade_i.gameObject.active = true;

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
}
