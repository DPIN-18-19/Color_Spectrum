using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public Rigidbody myRB;
    public float moveSpeed;
    public float moveSpeed1;
    public bool isMoving;

    public PlayerController thePlayer;
    
    private void Start()
    {
       
        myRB = GetComponent<Rigidbody>();
        thePlayer = FindObjectOfType<PlayerController>();
        
    }
    private void FixedUpdate()
    {
        if (isMoving == true)
        {
            myRB.velocity = (transform.forward * moveSpeed);
        }
        if (isMoving == false)
        {
            myRB.velocity = (transform.forward * 0);
        }
    }
    private void Update()
    {
        transform.LookAt(thePlayer.transform.position);
        
    }
    private void OnTriggerEnter (Collider col)
    {
        if(col.gameObject.tag == "LimitEnemigo")
        {
            moveSpeed = 0;
        }
        if (col.gameObject.tag == "AttackPlayer")
        {
            isMoving = true;
            moveSpeed = moveSpeed1;
            
        }

    }
    private void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag == "LimitEnemigo")
        {
            moveSpeed = moveSpeed1;
        }
        if (col.gameObject.tag == "AttackPlayer")
        {
            isMoving = false;
            moveSpeed = 0;
        }

    }


}
