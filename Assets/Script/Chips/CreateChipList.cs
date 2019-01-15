using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Chip Info", menuName = "Chip Info", order = 0)]
public class CreateChipList : ScriptableObject
{
    public List<ChipData> chips;
}
