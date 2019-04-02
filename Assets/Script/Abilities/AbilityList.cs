using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Ability Info", menuName = "Ability Info", order = 0)]
public class AbilityList : ScriptableObject
{
    public List<AbilityData> abi_list;

    public AbilityData SearchChipById(string id)
    {
        for (int i = 0; i < abi_list.Count; ++i)
        {
            if (abi_list[i].id == id)
                return abi_list[i];
        }

        return null;
    }
}