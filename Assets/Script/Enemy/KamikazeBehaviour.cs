using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KamikazeBehaviour : EnemyBehaviour
{
    public AudioClip SonidoKi;

    protected override void KeepDistance()
    {
        if (detect.IsPlayerNear(safe_distance))
        {
            is_looking = true;
            Invoke("DestroyEnemy", 1.5f);
            nav_agent.velocity = Vector3.zero;
            a_source.PlayOneShot(SonidoKi);
        }
    }
}
