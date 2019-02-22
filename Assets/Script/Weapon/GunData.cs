using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunData
{
    public string name = "";

    public GameObject gun;
    public GameObject bullet;

    public float speed = 0.2f;
    public float damage = 5;
    public float range = 5;
    public float spray = 5;
    public float cadence = 0.5f;
    public float num_bullets = 1f;
    public bool automatic = true;

    public float weight = 10.0f; 
    public string ability = "";
    
    public AudioClip fx_shot;

    // Elelmentos de UI
    public Sprite display_icon;

    // Datos de tienda
    public float price;

    public void Clone(GunData other)
    {
        name = other.name;

        gun = other.gun;
        bullet = other.bullet;

        speed = other.speed;
        damage = other.damage;
        range = other.range;
        spray = other.spray;
        cadence = other.cadence;
        num_bullets = other.num_bullets;
        automatic = other.automatic;

        weight = other.weight;
        ability = other.ability;

        fx_shot = other.fx_shot;

        display_icon = other.display_icon;

        price = other.price;
    }
}
