﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BestAIEnemy : MonoBehaviour
{
    //- Make Universal color system

    // Colors
    public enum Colors
    {
        Yellow,         // Yellow = 0
        Cyan,           // Cyan = 1
        Magenta         // Magenta = 2
    };

    public Colors cur_color = Colors.Yellow;                    // Current selected color

    string damaging_tag1;
    string damaging_tag2;
    int damaging_layer1;
    int damaging_layer2;

    public float rotacion;


    /////////////////////////////////////////////////////

    public bool AttackMovePlayer;
    public bool AttackStopPlayer;
    Animator anim;


    private NavMeshAgent theAgent;      // Navmesh object
    private GameObject target;          // Move towards objective
    public bool isMoving;               // Is enemy moving towards objective
    public ShotEnemy shot;              // Enemy's gun
    public Transform[] points;         // Patrol points (unfinished)
    public ParticleSystem DieEffect;    // Die particles
    public bool cercaMirar = false;     // Rotate enemy when going after player
    public GameObject Back;             // Enemy return point
    public bool Stop;                   // (Unused)
    public bool FuegoAmigo;             // Friendly fire

    // private int despoint;

    // Use this for initialization
    void Start()
    {
        anim = gameObject.GetComponent<Animator>();
        



        target = GameObject.Find("Player");
        theAgent = GetComponent<NavMeshAgent>();
        //DieEffect.Stop();
        EnemyColorData();
    }

    void EnemyColorData()
    {
        if (cur_color == Colors.Yellow)
        {
            damaging_tag1 = "Blue";
            damaging_tag2 = "Pink";
            damaging_layer1 = 12;
            damaging_layer2 = 13;
        }
        else if (cur_color == Colors.Magenta)
        {
            damaging_tag1 = "Blue";
            damaging_tag2 = "Yellow";
            damaging_layer1 = 12;
            damaging_layer2 = 11;
        }
        else if (cur_color == Colors.Cyan)
        {
            damaging_tag1 = "Pink";
            damaging_tag2 = "Yellow";
            damaging_layer1 = 13;
            damaging_layer2 = 11;
        }
    }

    // Update is called once per frame
    void Update()
    {

        // Go after player
        if (isMoving == true)
        {
            theAgent.SetDestination(target.transform.position);
            shot.isShooting = true;

            AttackMovePlayer = true;
            anim.SetBool("Attack", AttackMovePlayer);
            AttackStopPlayer = false;
            anim.SetBool("AttackStop", AttackStopPlayer);


            theAgent.isStopped = false;

        }
        // Go back home
        if (isMoving == false)
        {
            theAgent.SetDestination(Back.transform.position);

        }

        // Rotate towards player
        if (cercaMirar == true)
        {
            float look = LookAtAxis(target.transform.position);

            look = Mathf.LerpAngle(0, look, Time.deltaTime * rotacion);
            transform.Rotate(0, look, 0);
            theAgent.isStopped = true;
        }

        // Freeze in navmesh
        if (Stop == true)
        {
            theAgent.isStopped = true;
            AttackStopPlayer = true;
            anim.SetBool("AttackStop", AttackStopPlayer);
            AttackMovePlayer = false;
            anim.SetBool("Attack", AttackMovePlayer);
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        //  Keep distance from player
        if (col.gameObject.tag == "LimitEnemigo")
        {
            cercaMirar = true;
            Stop = true;
            shot.isShooting = true;

        }
        // Start attacking player
        if (col.gameObject.tag == "AttackPlayer")
        {

            // target = GameObject.Find("Player");
            isMoving = true;
            Stop = false;


        }

    }
    private void OnCollisionEnter(Collision col)
    {
        // Collided with player's bullet
        if ((col.gameObject.tag == damaging_tag1 && col.gameObject.layer == damaging_layer1) || (col.gameObject.tag == damaging_tag2 && col.gameObject.layer == damaging_layer2))
        {
            Instantiate(DieEffect.gameObject, transform.position, Quaternion.identity);
            Debug.Log("hOLA");
            Destroy(this.gameObject);
            //- Has to destroy enemy position too


            if (col.gameObject.layer == 16 && FuegoAmigo == true)
            {
                Destroy(col.gameObject);
            }

        }
    }
    private void OnTriggerExit(Collider col)
    {
        // Keep walking towards player
        if (col.gameObject.tag == "LimitEnemigo")
        {
            isMoving = true;
            Stop = false;
            cercaMirar = false;
            transform.LookAt(target.transform.position);

        }

        // Go back home
        if (col.gameObject.tag == "AttackPlayer")
        {
            isMoving = false;
            shot.isShooting = false;
            


        }
    }


    //////////////////////////////////////////

    // Get Enemy Color
    public int GetColor()
    {
        return (int)cur_color;
    }

    public float LookAtAxis(Vector3 look_at)
   {
        Vector3 projection = Vector3.ProjectOnPlane(transform.position - look_at, transform.up);
        {
            return Vector3.Angle(transform.forward, projection) - 180;
        }
  }
}
