using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CharacterStatsPanel : MonoBehaviour
{
    public float base_health;
    public float base_armor;
    public float base_speed;

    public float health;
    public float armor;
    public float speed;

    Transform t_health;
    Transform t_speed;
    Transform t_armor;

    [SerializeField]
    ChipList player_chips;
    int num_chips = 0;

    // Use this for initialization
    void Start()
    {
        // This line is for testing purposes
        //player_chips.chips.Clear();

        health = base_health;
        armor = base_armor;
        speed = base_speed;

        t_health = transform.Find("H_val");
        t_armor = transform.Find("A_val");
        t_speed = transform.Find("S_val");

        LoadChips();
    }

    // Update is called once per frame
    void Update()
    {
        CheckReload();

        t_health.GetComponent<TextMeshProUGUI>().text = health.ToString();
        t_armor.GetComponent<TextMeshProUGUI>().text = armor.ToString();
        t_speed.GetComponent<TextMeshProUGUI>().text = speed.ToString();
    }

    void LoadChips()
    {
        for (int i = 0; i < player_chips.chips.Count; ++i)
        {
            Debug.Log(i + " out of " + player_chips.chips.Count);
            for (int j = 0; j < player_chips.chips[i].player_stats.Count; ++j)
            {
                switch (player_chips.chips[i].player_stats[j].stat_name)
                {
                    case "Health":
                        health += player_chips.chips[i].player_stats[j].stat_value;
                        break;
                    case "Armor":
                        armor += player_chips.chips[i].player_stats[j].stat_value;
                        break;
                    case "Speed":
                        speed += player_chips.chips[i].player_stats[j].stat_value;
                        break;
                    default:
                        Debug.Log("Unknown player stat was loaded");
                        break;
                }
            }

            ++num_chips;
        }
    }

    void ResetPlayer()
    {
        health = base_health;
        armor = base_armor;
        speed = base_speed;
        num_chips = 0;
    }

    void ReloadChips()
    {
        ResetPlayer();
        LoadChips();
    }

    void CheckReload()
    {
        if (player_chips.chips.Count != num_chips)
            ReloadChips();
    }
}