using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public enum EnemyType
    {
        Pistol,
        Rifle,
        Shotgun,
        Kamikaze,
        Shield,
        Turret
    }
    public EnemyType enemy_type;
    
    // Colors
    public enum Colors
    {
        Yellow,         // Yellow = 0
        Blue,           // Cyan = 1
        Pink         // Magenta = 2
    };
    public Colors cur_color;        // Current selected color
    
    // Moving variables
    public float chase_speed = 7;
    
    // Get Enemy Color
    public int GetColor()
    {
        return (int)cur_color;
    }
}
