using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GradeData
{
    public string grade;
    public Material mat;

    public void Clone(GradeData other)
    {
        grade = other.grade;
        mat = other.mat;
    }
}
