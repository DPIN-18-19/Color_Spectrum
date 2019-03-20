using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Chip Info", menuName = "Chip Info", order = 0)]
public class ChipList : ScriptableObject
{
    public List<ChipData> chips;

    public ChipData SearchChipById(string id)
    {
        for (int i = 0; i < chips.Count; ++i)
        {
            if (chips[i].id == id)
                return chips[i];
        }

        return null;
    }
}
