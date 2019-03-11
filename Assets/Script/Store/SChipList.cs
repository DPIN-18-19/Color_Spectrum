using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Store Chip Info", menuName = "Store Chip Info", order = 0)]
public class SChipList : ScriptableObject
{
    public List<SChipData> schip_l;

    public SChipData SearchChipById(int id)
    {
        for (int i = 0; i < schip_l.Count; ++i)
        {
            if (schip_l[i].store_id == id)
                return schip_l[i];
        }

        return null;
    }
}
