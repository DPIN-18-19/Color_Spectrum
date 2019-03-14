
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotgunWeapon : GunController
{
    bool is_ready = false;

    

    public Transform EffectTransform;

    private AudioSource audiosource;
    public AudioClip FXShotEffect;
    public GameObject ParticleFlash_Y;
    public GameObject ParticleFlash_C;
    public GameObject ParticleFlash_M;

    // Use this for initialization
    void Start ()
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
	void Update ()
    {
        ReadyToFire();
    }

    public override void FireBullet()
    {
        base.FireBullet();      // Disparo de bala normal
        if (!gun_automatic)
        {
            is_ready = false;       // Comportamiento adicional de escopeta
        }
    }

    void ReadyToFire()
    {
        if (!is_ready && cadence <= 0)
        {
            is_ready = true;
            if (weapon_color == 0)
            {
                Instantiate(ParticleFlash_Y, EffectTransform.position, EffectTransform.rotation);
                source.PlayOneShot(FXShotEffect);
            }
            if (weapon_color == 1)
            {
                Instantiate(ParticleFlash_C, EffectTransform.position, EffectTransform.rotation);
                source.PlayOneShot(FXShotEffect);
            }
            if (weapon_color == 2)
            {
                Instantiate(ParticleFlash_M, EffectTransform.position, EffectTransform.rotation);
                source.PlayOneShot(FXShotEffect);
            }
        }
    }

    
}
