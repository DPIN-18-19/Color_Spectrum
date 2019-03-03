using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Time_Manager : MonoBehaviour {
    public static Ability_Time_Manager Instance { get; private set; }
    //  Ralentizar Balas
    public float Slow_Bullet_Velocity; //
    public float Slow_Bullet_Destroy; //
    public float Slow_ShotgunBullet_Velocity; //

    //Ralentizar Enemigos
    public float Slow_Enemy_Speed; //
    public float Slow_Enemy_Rotation; //
    public float Slow_Enemy_Animation; //

    //Tiempo entre disparos los enemigos
    public float Slow_Enemy_Shoot; 

    //Efectos de particulas 
    public float RalentizarComprimir; //
    public float RalentizarExplosion; //

    public float MusicaRalentizado;
    public float FXRalentizado;

    // Use this for initialization
    void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
