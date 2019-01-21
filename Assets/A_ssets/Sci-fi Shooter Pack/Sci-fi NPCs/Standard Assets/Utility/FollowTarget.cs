using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowTarget : MonoBehaviour
{
    public enum ToFollow
    {
        Player,
        Enemy,
        FirePos,
        BackShotEffect,
        Custom
    };
    public ToFollow who;

    public GameObject custom_target;
    GameObject target = null;
    public Vector3 Offset = Vector3.zero;

    // Use this for initialization
    void Start()
    {
        switch (who)
        {
            case ToFollow.Player:
                TargetIsPlayer();
                break;
            case ToFollow.Enemy:
                TargetIsEnenmy();
                break;
            case ToFollow.Custom:
                TargetIsCustom();
                break;
            case ToFollow.FirePos:
                TargetIsFirePos();
                break;
            case ToFollow.BackShotEffect:
                TargetIsBackShotEffect();
                break;
            default:
                Debug.Log("No Target To Follow Assign");
                break;
        }
    }
    //BackShotEffect

    // Update is called once per frame
    void Update()
    {
        Follow();
    }

    void Follow()
    {
        if (target != null)
            transform.position = target.transform.position - new Vector3(0, 0, 0) + Offset;
    }

    // Seguir a jugador
    void TargetIsPlayer()
    {
        target = GameObject.FindGameObjectWithTag("Player");
    }

    // Seguir a enemigo (asegurarse de que el objeto que sigue está dentro del enemigo, sino usar Custom)
    void TargetIsEnenmy()
    {
        target = transform.parent.GetComponentInChildren<EnemyController>().transform.Find("HeadBob").gameObject;
    }

    // Seguir a elemento escogido
    void TargetIsCustom()
    {
        target = custom_target;
    }
    void TargetIsFirePos()
    {
        target = GameObject.FindGameObjectWithTag("FirePos");
    }
    void TargetIsBackShotEffect()
    {
        target = GameObject.FindGameObjectWithTag("BackShotEffect");
    }
}
