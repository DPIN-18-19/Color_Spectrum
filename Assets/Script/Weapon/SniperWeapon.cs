using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperWeapon : GunController
{
    bool is_ready = false;
    public GameObject ready_flash;

    // Use this for initialization
    void Start()
    {
        //base.Start();
        source = GetComponent<AudioSource>();

        ColorChangingController.Instance.ToYellow += BulletToYellow;
        ColorChangingController.Instance.ToCyan += BulletToCyan;
        ColorChangingController.Instance.ToMagenta += BulletToMagenta;

        spawn = transform.Find("FirePos");
        shell = transform.Find("SpawnShell");
    }

    // Update is called once per frame
    void Update()
    {
        ReadyToFire();
    }

    public override void FireBullet()
    {
        base.FireBullet();
        is_ready = false;
    }

    void ReadyToFire()
    {
        if (!is_ready && cadence <= 0)
        {
            is_ready = true;
            Instantiate(ready_flash, spawn.position, spawn.rotation);
        }
    }

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
