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
    public bool item_got;           // El objeto desbloqueable del nivel se ha obtenido

    public int highscore;           // Mayor puntuación obtenida
    public ScoreGrade grade;        // Mayor nota obtenida

    public List<string> unlock_require; // Nombres de niveles necesarios de completar para desbloquear

    public void Clone(LevelData other)
    {
        id = other.id;
        name = other.name;
        scene_name = other.scene_name;
        brief = other.brief;

        unlocked = other.unlocked;
        complete = other.complete;
        item_got = other.item_got;

        highscore = other.highscore;
        grade = other.grade;
    }
}
