using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour {
    
    public GameObject bullet;
    public Transform FirePos;
    public float timeBetweenShorts = 3;
    public float TimeShots;
    public bool isShooting;
    public GameObject EffectShot;
    public float FlashTime;
    public Transform Shell;
    public Transform ShellEjection;
    public AudioClip FXShotEnemy;
    private AudioSource source;

    void Awake()
    {
        source = GetComponent<AudioSource>();
    }
    // Use this for initialization
    void Start () {
        timeBetweenShorts = TimeShots;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timeBetweenShorts = timeBetweenShorts - Time.deltaTime;
		if(timeBetweenShorts < 0 && isShooting == true)
        {
            source.PlayOneShot(FXShotEnemy);
            EffectShot.SetActive(true);
            GameObject bullet_shot = Instantiate(bullet, FirePos.position, FirePos.rotation);
            bullet_shot.GetComponent<BulletController>().AddBulletInfo(0,5,5,5,false);
            Instantiate(Shell, ShellEjection.position, ShellEjection.rotation);
            timeBetweenShorts = TimeShots;
            Invoke("QuitarEfecto", FlashTime);
        }
	}
    void QuitarEfecto()
    {
        EffectShot.SetActive(false);
    }
}
