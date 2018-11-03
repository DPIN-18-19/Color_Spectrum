using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BestAIEnemyBlue : MonoBehaviour {

    private NavMeshAgent theAgent;
    private GameObject target;
    public bool isMoving;
    public ShotEnemy shot;
    public ParticleSystem DieEffect;
    public bool cercaMirar = false;

    public GameObject Back;

    public bool Stop;
    public bool FuegoAmigo;

    // private int despoint;
    // Use this for initialization
    void Start()
    {
        target = GameObject.Find("Player");
        theAgent = GetComponent<NavMeshAgent>();
        DieEffect.Stop();
    }

    // Update is called once per frame
    void Update()
    {
        
        if (isMoving == true)
        {
            theAgent.SetDestination(target.transform.position);
            shot.isShooting = true;
        }
        theAgent.isStopped = false;
        if (isMoving == false)
        {
            theAgent.SetDestination(Back.transform.position);
            //theAgent.isStopped = true;
        }
        if(Stop == true)
        {
            theAgent.isStopped = true;
        }
        if (cercaMirar == true)
        {
            transform.LookAt(target.transform.position);
        }
    }

    private void OnTriggerEnter(Collider col)
    {

        if (col.gameObject.tag == "LimitEnemigo")
        {
            cercaMirar = true;
            Stop = true;
            shot.isShooting = true;

        }
        if (col.gameObject.tag == "AttackPlayer")
        {

            // target = GameObject.Find("Player");
            isMoving = true;
            Stop = false;


        }

    }
    private void OnCollisionEnter(Collision col)
    {
        if ((col.gameObject.tag == "Pink" && col.gameObject.layer == 13) || (col.gameObject.tag == "Yellow" && col.gameObject.layer == 11))
        {
            Instantiate(DieEffect.gameObject, transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
        //Evitar Fuego Amigo
        if (col.gameObject.layer == 16 && FuegoAmigo == true)
        {
            Destroy(col.gameObject);
        }
    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "LimitEnemigo")
        {
            isMoving = true;
            Stop = false;
            cercaMirar = false;
            transform.LookAt(target.transform.position);

        }
        if (col.gameObject.tag == "AttackPlayer")
        {
            isMoving = false;
            shot.isShooting = false;
            


        }

    }

}
