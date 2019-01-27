using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Info", menuName = "Ability Info", order = 0)]
public class AbilityList : ScriptableObject
{
    public List<AbilityData> abi_list;
}