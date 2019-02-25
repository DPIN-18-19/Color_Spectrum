using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Unlock Info", menuName = "Unlock Info", order = 0)]
public class UnlockList : ScriptableObject
{
    public List<UnlockItems> unlock_l;
}
