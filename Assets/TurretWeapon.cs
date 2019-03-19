using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretWeapon : EnemyWeapon
{
    [Header("Torreta exclusivo")]
    Transform fire_pos_1;
    public Transform weapon_1;
    private float shot_c_1;
    [SerializeField]
    protected float shot_dur_1;
    [SerializeField]
    private float shot_dur_max_1;
    [SerializeField]
    float late_shot = 0.1f;
    [SerializeField]
    float late_shot_1 = 0.1f;

    public bool shot_right;
    public bool FirstShot;

    
    // Use this for initialization
    void  Start()
    {
        a_source = GetComponent<AudioSource>();

        fire_pos = weapon.Find("FirePos");
        fire_pos_1 = weapon_1.Find("FirePos_1");
        //effect_pos = weapon.Find("ShotEffect");
        // effect_pos = weapon.Find("ShotEffect");
      

        shot_c = shot_dur;
        shot_c_1 = shot_dur_1;
        bullet_color = gameObject.GetComponent<Enemy>().GetColor();

        RalentizarDisparos = 1f;
        Ralentizar = GameObject.Find("Player_Naomi").GetComponent<Slow_Motion>();

    }

    // Update is called once per frame
    void Update ()
    {
        if (Ralentizar.ActivateAbility == true)
            a_source.pitch = Ability_Time_Manager.Instance.FXRalentizado;
        if (Ralentizar.ActivateAbility == false)
            a_source.pitch = 1;

        // Realizar disparo

        //if(!FirstShot)
        // {
        //     shot_c = late_shot;
        //     shot_c_1 = late_shot_1;
        // }
        //if (FirstShot)
        //{
        //    ResetTurret();
        //}
       // Debug.Log(shot_c);
       
            if (!shot_right)
            {
           
            shot_c -= Time.deltaTime;

                if (shot_c < 0 && is_shooting == true)
                {
                    AddaptColor();
                    a_source.PlayOneShot(FXShotEnemy);

                    GameObject bullet_shot = Instantiate(bullet, fire_pos.position, fire_pos.rotation);
                    Vector3 bullet_dir = transform.forward;
                    bullet_shot.GetComponent<BulletController>().
                                AddBulletInfo(bullet_color, -bullet_speed,
                                bullet_dir, bullet_dmg, bullet_range, bullet_friend);   //- Create Gun Variables

                    // Calculo de tiempo siguiente disparo
                    shot_c_1 = NextShotTime_1() * RalentizarDisparos;
               
                shot_right = true;
                }
            }
            else
            {
           
                shot_c_1 -= Time.deltaTime;

                if (shot_c_1 < 0 && is_shooting == true)
                {
                    AddaptColor();
                    a_source.PlayOneShot(FXShotEnemy);

                    GameObject bullet_shot = Instantiate(bullet, fire_pos_1.position, fire_pos_1.rotation);
                    Vector3 bullet_dir = transform.forward;
                    bullet_shot.GetComponent<BulletController>().
                                AddBulletInfo(bullet_color, -bullet_speed,
                                bullet_dir, bullet_dmg, bullet_range, bullet_friend);   //- Create Gun Variables

                    // Calculo de tiempo siguiente disparo
                    shot_c = NextShotTime() * RalentizarDisparos;
                    shot_right = false;
                }
            }
        



        if (Ralentizar.ActivateAbility == true)
            RalentizarDisparos = Ability_Time_Manager.Instance.Slow_Enemy_Shoot;
        if (Ralentizar.ActivateAbility == false)
            RalentizarDisparos = 1f;
    }
    protected float NextShotTime_1()
    {
        if (!random)
            return shot_dur_1;
        else
            return Random.Range(shot_dur_1, shot_dur_max_1);
    }
    protected override void AddaptColor()
    {
       // Debug.Log("AddaptColorTurret");
        //bullet_color = gameObject.GetComponent<EnemyController>().GetColor();

        switch (bullet_color)
        {
            // Amarillo
            case 0:
                if (bullet.GetComponent<Renderer>() != null)
                    bullet.GetComponent<Renderer>().material = yellow_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)
                    bullet.GetComponent<TrailRenderer>().material = yellow_mat;

                // Crear efectos adicionales de disparo
                //Instantiate(shot_effect_y.gameObject, effect_pos.position, effect_pos.rotation);
                
                break;
            // Cyan
            case 1:
                if (bullet.GetComponent<Renderer>() != null)
                    bullet.GetComponent<Renderer>().material = cyan_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)
                    bullet.GetComponent<TrailRenderer>().material = cyan_mat;

                // Crear efectos adicionales de disparo
                //Instantiate(shot_effect_c.gameObject, effect_pos.position, effect_pos.rotation);
                
                break;
            // Magenta
            case 2:
                if (bullet.GetComponent<Renderer>() != null)
                    bullet.GetComponent<Renderer>().material = magenta_mat;

                if (bullet.GetComponent<TrailRenderer>() != null)
                    bullet.GetComponent<TrailRenderer>().material = magenta_mat;

                // Crear efectos adicionales de disparo
                //Instantiate(shot_effect_m.gameObject, effect_pos.position, effect_pos.rotation);
               
                break;
            default:
                break;
        }
    }

    public void ResetTurret()
    {
        //shot_c = shot_dur;
        //shot_c_1 = shot_dur_1;
        shot_c = late_shot;
        shot_c_1 = late_shot_1;
        shot_right = false;
    }

}
