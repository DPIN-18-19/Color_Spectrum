using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotEnemy : MonoBehaviour
{
    // Color
    int bullet_color;
    
    public Material yellow_mat;
    public Material cyan_mat;
    public Material magenta_mat;

    ////////////////////////////////////////////////

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

        if (timeBetweenShorts < 0 && isShooting == true)
        {
            AddaptColor();
            source.PlayOneShot(FXShotEnemy);
            EffectShot.SetActive(true);
            GameObject bullet_shot = Instantiate(bullet, FirePos.position, FirePos.rotation);
            //bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color,10, 5,20,false);   //- Create Gun Variables
            bullet_shot.GetComponent<BulletController>().AddBulletInfo(bullet_color, 10,transform.forward, 5, 20, false);   //- Create Gun Variables
            Instantiate(Shell, ShellEjection.position, ShellEjection.rotation);
            timeBetweenShorts = TimeShots;
            Invoke("QuitarEfecto", FlashTime);
        }
	}
    void QuitarEfecto()
    {
        EffectShot.SetActive(false);
    }

    // Change colors of bullet
    void AddaptColor()
    {
        bullet_color = gameObject.GetComponent<BestAIEnemy>().GetColor();

        switch (bullet_color)
        {
            case 0:
                bullet.GetComponent<Renderer>().material = yellow_mat;
                break;
            case 1:
                bullet.GetComponent<Renderer>().material = cyan_mat;
                break;
            case 2:
                bullet.GetComponent<Renderer>().material = magenta_mat;
                break;
            default:
                Debug.Log("Hello");
                break;
        }
    }
}
