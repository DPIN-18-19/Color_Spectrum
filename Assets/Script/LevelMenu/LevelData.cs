using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelData
{
    public string id;               // Nombre identificador del nivel
    public string name;             // Nombre del nivel en el juego
    public string scene_name;       // Nombre de la escena del nivel
    public string brief;            // Descripción del nivel
    public bool unlocked;           // El nivel está bloqueado
    public bool complete;           // El nivel se ha jugado una vez
    public int highscore;           // Mayor puntuación obtenida
    public ScoreGrade grade;        // Mayor nota obtenida
}
