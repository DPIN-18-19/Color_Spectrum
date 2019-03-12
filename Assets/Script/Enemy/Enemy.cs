using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Colors
    public enum Colors
    {
        Yellow,         // Yellow = 0
        Cyan,           // Cyan = 1
        Magenta         // Magenta = 2
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
