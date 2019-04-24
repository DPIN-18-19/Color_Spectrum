using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class KamikazeBehaviour : EnemyBehaviour
{
    public AudioClip SonidoKi;
    public float FxSonidoKi = 1;
    private bool OnlyOne = true;

    private void Start()
    {
        // Inicializar variables componentes
        nav_agent = GetComponent<NavMeshAgent>();
        detect = GetComponent<DetectionController>();
        patrol = GetComponent<PatrolController>();
        anim = GetComponent<Animator>();
        a_source = GetComponent<AudioSource>();

        // Inicializar objetos objetivo
        target = GameObject.FindWithTag("Player").transform;
        home = transform.parent.Find("EnemyHome");
        detect.SetTarget(target);

        if (!can_move)
            nav_agent.speed = 0;

        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();
        // Habilidad ralentizar
        MaxRalentizarRotar = 1;
        //target = GameObject.FindWithTag("Player");
        MaxSpeedSlow = nav_agent.speed;
        MaxAnimSlow = anim.speed;
    }

    protected override void KeepDistance()
    {
        if (detect.IsPlayerNear(safe_distance))
        {
            is_looking = true;
            Invoke("DestroyEnemy", 1.5f);
            nav_agent.velocity = Vector3.zero;
            if (OnlyOne)
            {
                a_source.PlayOneShot(SonidoKi, FxSonidoKi);
                OnlyOne = false;
            }
        }
    }

    protected override void ShootTarget()
    {
    }

    public void DestroyEnemy()
    {
        //Efecto de particulas de explosion
        GetComponent <EnemyHealth>().GetDamage(1);
    }
}