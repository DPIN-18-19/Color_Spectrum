using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : WeaponController {

	// Use this for initialization
	void Start ()
    {
        // Subscribe to Event
        //ColorChangingController.Instance.ToYellow += BulletToYellow;
        //ColorChangingController.Instance.ToCyan += BulletToCyan;
        //ColorChangingController.Instance.ToMagenta += BulletToMagenta;

        GetNewWeapon(activated_weapon);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
