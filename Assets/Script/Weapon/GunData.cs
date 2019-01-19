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
}
