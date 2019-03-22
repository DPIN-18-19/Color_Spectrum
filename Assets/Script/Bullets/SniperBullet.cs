using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : BulletController {
    public GameObject RaycastEffect_Yellow;
    public GameObject RaycastEffect_Cian;
    public GameObject RaycastEffect_Magenta;
    private GameObject MyEffectYellow;
    private GameObject MyEffectCian;
    private GameObject MyEffectMagenta;
    
    
    // Use this for initialization
    void Start () {

        m_collider = GetComponent<Collider>();
       



    }
	
	// Update is called once per frame
	void Update () {
        MoveBullet();
        if (bullet_color == 0)
            MoveEffectYellow();
        if (bullet_color == 1)
            MoveEffectCian();
        if (bullet_color == 2)
            MoveEffectMagenta();
        Invoke("DestroyBullet", 5f);
        Debug.Log(bullet_color);
    }
    public override void AddBulletInfo(int n_color, float n_increase_time, Vector3 n_dir, float n_damage, float n_range, bool n_friend)
    {
        base.AddBulletInfo(n_color, n_increase_time, n_dir, n_damage, n_range, n_friend);
        if (bullet_color == 0)
            MyEffectYellow = Instantiate(RaycastEffect_Yellow, transform.position, transform.rotation);
        if (bullet_color == 1)
            MyEffectCian = Instantiate(RaycastEffect_Cian, transform.position, transform.rotation);
        if (bullet_color == 2)
            MyEffectMagenta = Instantiate(RaycastEffect_Magenta, transform.position, transform.rotation);
        if (n_color == 0)
        {
            this.gameObject.tag = "Yellow";
            this.gameObject.layer = 8;
           
        }
        else if (n_color == 1)
        {
            this.gameObject.tag = "Blue";
            this.gameObject.layer = 9;
           
        }
        else if (n_color == 2)
        {
            this.gameObject.tag = "Pink";
            this.gameObject.layer = 10;
            
        }

    }
    void MoveEffectYellow()
    {
        Debug.Log("EffectYellow");
        Vector3 final_pos = MyEffectYellow.transform.position + bullet_dir * -bullet_speed * Time.deltaTime;
        // Move only if no collision is found
        if (!PeekNextPosition(final_pos))
            MyEffectYellow.transform.position += bullet_dir * -bullet_speed * Time.deltaTime;
        
    }
    void MoveEffectCian()
    {

        Vector3 final_pos = MyEffectCian.transform.position + bullet_dir * -bullet_speed * Time.deltaTime;
        // Move only if no collision is found
        if (!PeekNextPosition(final_pos))
            MyEffectCian.transform.position += bullet_dir * -bullet_speed * Time.deltaTime;
    }
    void MoveEffectMagenta()
    {

        Vector3 final_pos = MyEffectMagenta.transform.position + bullet_dir * -bullet_speed * Time.deltaTime;
        // Move only if no collision is found
        if (!PeekNextPosition(final_pos))
            MyEffectMagenta.transform.position += bullet_dir * -bullet_speed * Time.deltaTime;
    }
    void DestroyBullet()
    {
        Destroy(gameObject);
    }

}
    
