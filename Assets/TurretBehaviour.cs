using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class TurretBehaviour : EnemyBehaviour
{
    TurretWeapon shot_turret;
    protected Animator anim_turret;
    public GameObject turret;

    // Use this for initialization
    private void Start()
    {
        // Inicializar variables componentes
        nav_agent = GetComponent<NavMeshAgent>();
        detect = GetComponent<DetectionController>();
        shot = GetComponent<EnemyWeapon>();
        shot_turret = GetComponent<TurretWeapon>();
        patrol = GetComponent<PatrolController>();
        anim_turret = turret.GetComponent<Animator>(); 
        a_source = GetComponent<AudioSource>();

        // Inicializar objetos objetivo
        target = GameObject.Find("Player_Naomi");
        home = transform.parent.Find("EnemyHome");

        if (!can_move)
            nav_agent.speed = 0;

        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        // Habilidad ralentizar
        MaxRalentizarRotar = 1;
        //target = GameObject.FindWithTag("Player");
        MaxSpeedSlow = nav_agent.speed;
        MaxAnimSlow = anim_turret.speed;
    }

    // Update is called once per frame
    void Update () {
        
            // Cambiadores de estado
            DetectPlayer();
            KeepDistance();
            if (can_shoot)
                ShootTarget();

            // Estados de enemigo
            // Realizar "Perseguir"
            if (is_chasing)
                IsChasing();
            // Realizar "Regresar"
            if (is_retreat)
                IsRetreating();
            // Realizar "Patrullar"
            if (patrol != null && patrol.is_patrolling)
                IsPatrolling();
            // Realizar "Mirar"
            if (can_rotate && is_looking)
                IsLooking();
            // Realizar "EnCasa"
            if (in_home)
                IsInHome();

            UpdateAnimState();

            // Habilidad ralentizar
            if (Ralentizar.ActivateAbility == true)
            {
                a_source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
                nav_agent.speed = Ability_Time_Manager.Instance.Slow_Enemy_Speed;
                anim.speed = Ability_Time_Manager.Instance.Slow_Enemy_Animation;
                Slow_Rotation = Ability_Time_Manager.Instance.Slow_Enemy_Rotation;
            }
            if (Ralentizar.ActivateAbility == false)
            {
                a_source.pitch = 1;
                nav_agent.speed = MaxSpeedSlow;
                anim_turret.speed = MaxAnimSlow;
                Slow_Rotation = MaxRalentizarRotar;
            }
        }
    protected override void DetectPlayer()
    {
        // En estado "Perseguir", no se ve al jugador
        if (is_chasing && !detect.IsPlayerNear(lost_distance))
        {
            is_chasing = false;
            is_retreat = true;
        }

        if (can_see)
            // Comprobar si enemigo ve a jugador
            if (detect.IsPlayerInFront(sight_angle) && detect.IsPlayerNear(sight_distance) && detect.IsPlayerOnSight(sight_distance))
            {
                Debug.Log("I see");
                is_chasing = true;
                in_home = false;
                // True el booleano para que para que dispare por primera vez la torreta
            }

        if (!detect.IsPlayerNear(lost_distance) || !detect.IsPlayerInFront(sight_angle))
        {
            is_chasing = false;
            attack_moving = false;
            shot_turret.is_shooting = false;

            //Hacer condicion booleano para que dispare por primera vez la torreta
         //   if()
            //{
            //    shot_turret.ResetTurret();
            //    //False el booleano para que para que dispare por primera vez la torreta
            //}
        }
    }
    protected override void UpdateAnimState()
    {
        anim_turret.SetBool("Attack", attack_moving);
    }
}
