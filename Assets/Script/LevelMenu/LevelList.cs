using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CreateAssetMenu(fileName = "New Level Info", menuName = "Level Info", order = 0)]
public class LevelList : ScriptableObject
{
    public List<LevelData> levels;      //Lista de niveles en el juego
}
