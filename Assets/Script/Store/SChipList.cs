using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Store Chip Info", menuName = "Store Chip Info", order = 0)]
public class SChipList : ScriptableObject
{
    public List<SChipData> schip_l;
}
