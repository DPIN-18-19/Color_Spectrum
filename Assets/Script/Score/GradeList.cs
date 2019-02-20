using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

// Don't allow more grade data to be created
//[CreateAssetMenu(fileName = "New Grade Info", menuName = "Grade Info", order = 0)]
public class GradeList : ScriptableObject
{
    public List<GradeData> grades_l;

    public GradeData GetGradeByName(string name)
    {
        for(int i = 0; i < grades_l.Count; ++i)
        {
            if (grades_l[i].name == name)
                return grades_l[i];
        }

        return null;
    }
}
