using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : BulletController {
    public GameObject RaycastEffect;
    private GameObject MyEffect;
	// Use this for initialization
	void Start () {
        MyEffect = Instantiate(RaycastEffect, transform.position, transform.rotation);
      
    }
	
	// Update is called once per frame
	void Update () {
        MoveBullet();
        MoveEffect();


    }
    void MoveEffect()
    {
        Vector3 final_pos = MyEffect.transform.position + bullet_dir * -bullet_speed * Time.deltaTime;
        // Move only if no collision is found
        if (!PeekNextPosition(final_pos))
            MyEffect.transform.position += bullet_dir * -bullet_speed * Time.deltaTime;
    }
}
