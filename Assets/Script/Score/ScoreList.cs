using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Score Info", menuName = "Score Info", order = 0)]
public class ScoreList : ScriptableObject
{
    public List<TimeMultiplier> times;  // Lista de tiempos en orden ascendente
    public List<HealthScore> health;    // Lista de daño en orden descendente
    public List<ScoreGrade> grades;     // Lista de notas en orden descendente
    public void Clone(ScoreList other)
    {
        times.Clear();
        health.Clear();
        grades.Clear();

        times.AddRange(other.times);
        health.AddRange(other.health);
        grades.AddRange(other.grades);
    }
}
