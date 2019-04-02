using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldPos : MonoBehaviour
{
    public bool occupied;
    bool is_positioning;
    bool is_following;

    EnemyBehaviour behaviour;

    Transform occupee;
    EnemyBehaviour o_behaviour;

    float is_empty_c = 0;
    float is_empty_dur = 3;

    private void Start()
    {
        behaviour = transform.parent.parent.GetComponent<EnemyBehaviour>();
    }

    // Update is called once per frame
    void Update ()
    {
        if (occupied)
        {
            CheckEmpty();

            if (is_positioning)
                IsPositioning();

            if (is_following)
                IsFollowing();
        }
	}

    public void Occupy(Transform n_occupee)
    {
        occupied = true;
        is_positioning = true;
        is_empty_c = is_empty_dur;
        occupee = n_occupee;
        o_behaviour = occupee.GetComponent<EnemyBehaviour>();
    }

    void CheckEmpty()
    {
        is_empty_c -= Time.deltaTime;

        if(is_empty_c <= 0)
        {
            if (occupee == null)
            {
                occupied = false;
            }

            is_empty_c = is_empty_dur;
        }
    }

    void IsPositioning()
    {
        Debug.Log("Potitioning");

        o_behaviour.IsProtected(transform);
        o_behaviour.IsShooting(false);

        if(Vector3.Distance(transform.position, occupee.position) < 0.1f)
        {
            is_positioning = false;
            is_following = true;
        }
    }

    void IsFollowing()
    {
        Debug.Log("Following");

        o_behaviour.IsShooting(true);

        FollowMov();

        if(!behaviour.is_looking)
            FollowRot();

        FollowActions();
    }

    void FollowMov()
    {
        o_behaviour.IsProtected(transform);
    }

    void FollowRot()
    {
        float correct_look = Vector3.SignedAngle(occupee.forward, transform.forward, Vector3.up);
        correct_look = Mathf.LerpAngle(0, correct_look, Time.deltaTime);
        occupee.Rotate(0, correct_look, 0);
    }

    void FollowActions()
    {
        o_behaviour.is_looking = behaviour.is_looking;
        o_behaviour.attack_moving = behaviour.attack_moving;
        o_behaviour.attack_in_place = behaviour.attack_in_place;
    }

    public bool SameOccupee(Transform n_occupee)
    {
        if (occupied)
            return false;

        if (occupee == n_occupee)
            return true;

        return false;
    }

    //protected virtual void IsLooking()
    //{
    //    correct_look = LookAtAxis(target.position);
    //    correct_look = Mathf.LerpAngle(0, correct_look, Time.deltaTime / Slow_Rotation * 15.5f);
    //    transform.Rotate(0, correct_look, 0);

    //    attack_in_place = true;

    //    nav_agent.velocity = Vector3.zero;
    //    nav_agent.isStopped = true; // Se para el enemigo
    //}

    //public float LookAtAxis(Vector3 look_at)
    //{
    //    // Calculate point to look at
    //    Vector3 projection = Vector3.ProjectOnPlane(transform.position - look_at, transform.up);

    //    // Calculate Angle between current transform.front and object to look at
    //    return Vector3.SignedAngle(transform.forward, projection, Vector3.up) - 180;
    //}
}
