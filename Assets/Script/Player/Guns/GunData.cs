using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GunData
{
    public string name = "";

    public GameObject gun;

    public float speed = 0.2f;
    public float damage = 5;
    public float range = 5;
    public float spray = 5;
    public float cadency = 0.5f;
    public float num_disparos = 1f;

    public string ability = "";
    
    public AudioClip fx_shot;
}
