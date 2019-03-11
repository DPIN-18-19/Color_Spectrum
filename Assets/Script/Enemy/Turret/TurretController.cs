using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretController : MonoBehaviour
{
    public Material Blue_Material;
    public Material Yellow_Material;
    public Material Pink_Material;

    public Material DamageBlueMaterial;
    public Material DamageYellowMaterial;
    public Material DamagePinkMaterial;

    [SerializeField]
    public List<Renderer> renderersToChangeColor;


    public float alert_distance;                // Distance at which the enemy detects the player by getting near
    public float sight_distance;                // Distance at which the enemy detects the player by sight
    public enum Colors
    {
        Yellow,         // Yellow = 0
        Cyan,           // Cyan = 1
        Magenta         // Magenta = 2
    };
    public Colors cur_color = Colors.Yellow;

    public int GetColor()
    {
        return (int)cur_color;
    }
    void EnemyColorData()
    {
        if (cur_color == Colors.Yellow)
        {
            GetComponent<EnemyHealthController>().ChangeToYellow();
        }
        else if (cur_color == Colors.Magenta)
        {
            GetComponent<EnemyHealthController>().ChangeToMagenta();
        }
        else if (cur_color == Colors.Cyan)
        {
            GetComponent<EnemyHealthController>().ChangeToCyan();
        }
    }

    public void RestoreChangeToYellow()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = Yellow_Material;    // Apply player material
            //Instantiate(Change_effectYellow.gameObject, transform.position, Quaternion.identity);
            //   Debug.Log("Change to yellow");
        }

        // gameObject.layer = 8;

        // Yellow Layer
    }
    public void RestoreChangeToMagenta()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = Pink_Material;      // Apply player material
                                             //  Debug.Log("Change to magenta");
                                             //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        //  gameObject.layer = 10;

    }
    public void RestoreChangeToCyan()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            r.material = Blue_Material;      // Apply player material
                                             //  Debug.Log("Change to cyan");
                                             //Instantiate(Change_effectBlue.gameObject, transform.position, Quaternion.identity);
        }

        //  gameObject.layer = 9;

    }




    public void ChangeToDamageYellow()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = DamageYellowMaterial;      // Apply player material
                                                    //  Debug.Log("Change to magenta");
                                                    //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        // gameObject.layer = 8;

    }
    public void ChangeToDamageBlue()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = DamageBlueMaterial;      // Apply player material
                                                  //  Debug.Log("Change to magenta");
                                                  //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        // gameObject.layer = 9;

    }
    public void ChangeToDamagePink()
    {
        foreach (Renderer r in renderersToChangeColor)
        {
            // Magenta Layer
            r.material = DamagePinkMaterial;      // Apply player material
                                                  //  Debug.Log("Change to magenta");
                                                  //  Instantiate(Change_effectPink.gameObject, transform.position, Quaternion.identity);
        }

        //  gameObject.layer = 10;

    }
}
