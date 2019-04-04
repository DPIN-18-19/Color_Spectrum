using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyShieldBehaviur : EnemyBehaviour
{
    public float Rotation;
    public GameObject Shield;
	// Use this for initialization
	void Start () {
        Shield.SetActive(false);

        nav_agent = GetComponent<NavMeshAgent>();
        detect = GetComponent<DetectionController>();
        //shot = GetComponent<EnemyWeapon>();
        patrol = GetComponent<PatrolController>();
        anim = GetComponent<Animator>();
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
        MaxAnimSlow = anim.speed;

    }
	
	// Update is called once per frame
	void Update () {
        DetectPlayer();
        KeepDistance();
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
            anim.speed = MaxAnimSlow;
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
            Debug.Log("Retreat2");
            Shield.SetActive(false);
        }

        if (can_see)
            // Comprobar si enemigo ve a jugador
            if (detect.IsPlayerInFront(sight_angle) && detect.IsPlayerNear(sight_distance) && detect.IsPlayerOnSight(sight_distance))
            {
                Debug.Log("I see");
                is_chasing = true;
                Shield.SetActive(true);
                in_home = false;
            }

        if (can_detect_near)
            // Comprobar si enemigo detecta jugador cerca. No detecta si hay obstáculo en medio.
            if (detect.IsPlayerNear(alert_distance) && detect.IsPlayerOnSight(sight_distance))
            {
                Shield.SetActive(true);
                is_chasing = true;
                in_home = false;
            }
    }
    protected override void IsChasing()
    {
        nav_agent.SetDestination(target.transform.position);
        //nav_agent.speed = chase_speed;
        attack_moving = true;
        attack_in_place = false;

        nav_agent.isStopped = false;

        if (patrol != null)
            patrol.is_patrolling = false;
    }
    protected override void IsRetreating()
    {
        nav_agent.SetDestination(home.transform.position);

        float dist = nav_agent.remainingDistance; // Distancia restante hasta objetivo
        if (dist != Mathf.Infinity && nav_agent.remainingDistance <= 5.0f /*&& nav_agent.pathStatus == NavMeshPathStatus.PathComplete*/)
        {
            is_retreat = false;
            in_home = true;

            // Retornar a patrulla si debe
            if (patrol != null)
            {
                patrol.is_patrolling = true;
                patrol.ResetPatrol();
            }
        }
    }
    protected override void IsLooking()
    {
        correct_look = LookAtAxis(target.transform.position);
        correct_look = Mathf.LerpAngle(0, correct_look, Time.deltaTime /Rotation * (Slow_Rotation * 15.5f));
        transform.Rotate(0, correct_look, 0);

        attack_in_place = true;

        nav_agent.velocity = Vector3.zero;
        nav_agent.isStopped = true; // Se para el enemigo
    }
}
