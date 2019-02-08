using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperWeapon : GunController
{
    bool is_ready = false;
   
    

    public Transform EffectTransform;

    private AudioSource audiosource;
    public AudioClip FXShotEffect;
    public GameObject ParticleFlash_Y;
    public GameObject ParticleFlash_C;
    public GameObject ParticleFlash_M;

    // Use this for initialization
    void Start()
    {
        audiosource = GetComponent<AudioSource>();
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
        if (!gun_automatic)
        {
            is_ready = false;
        }
    }

    void ReadyToFire()
    {
        if (!is_ready && cadence <= 0 && Input.GetButtonDown("Fire1"))
        {
            source.PlayOneShot(FXShotEffect);
            is_ready = true;
            if (weapon_color == 0)
            {
                Instantiate(ParticleFlash_Y, EffectTransform.position, EffectTransform.rotation);  
            }
            if (weapon_color == 1)
            {
                Instantiate(ParticleFlash_C, EffectTransform.position, EffectTransform.rotation); 
            }
            if (weapon_color == 2)
            {
                Instantiate(ParticleFlash_M, EffectTransform.position, EffectTransform.rotation);   
            }
           
        }
    }

    //protected override void BulletToYellow()
    //{
    //    weapon_color = 0;
    //    if (bullet.GetComponent<Renderer>() != null)
    //        bullet.GetComponent<Renderer>().material = yellow_mat;
    //}


    //Changing to cyan
    //protected override void BulletToCyan()
    //{
    //    weapon_color = 1;
    //    if (bullet.GetComponent<Renderer>() != null)
    //        bullet.GetComponent<Renderer>().material = cyan_mat;
    //}

    //Changing to magenta
    //protected override void BulletToMagenta()
    //{
    //    weapon_color = 2;
    //    if (bullet.GetComponent<Renderer>() != null)
    //        bullet.GetComponent<Renderer>().material = magenta_mat;
    //}
}
