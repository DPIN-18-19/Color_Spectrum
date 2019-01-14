using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : GunController {

	// Use this for initialization
	void Start ()
    {
        // Subscribe to Event
        //ColorChangingController.Instance.ToYellow += BulletToYellow;
        //ColorChangingController.Instance.ToCyan += BulletToCyan;
        //ColorChangingController.Instance.ToMagenta += BulletToMagenta;
        
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // Changing to yellow
    //protected override void BulletToYellow()
    //{
    //    weapon_color = 0;
    //}

    //// Changing to cyan
    //protected override void BulletToCyan()
    //{
    //    weapon_color = 1;
    //}

    //// Changing to magenta
    //protected override void BulletToMagenta()
    //{
    //    weapon_color = 2;
    //}
}
