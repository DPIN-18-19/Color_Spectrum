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
       MyEffectYellow = Instantiate(RaycastEffect_Yellow, transform.position, transform.rotation);
       MyEffectCian = Instantiate(RaycastEffect_Cian, transform.position, transform.rotation);
       MyEffectMagenta = Instantiate(RaycastEffect_Magenta, transform.position, transform.rotation);
       


    }
	
	// Update is called once per frame
	void Update () {
        MoveBullet();
        if(bullet_color == 0)
        MoveEffectYellow();
        if (bullet_color == 1)
            MoveEffectCian();
        if (bullet_color == 2)
            MoveEffectMagenta();


    }
    void MoveEffectYellow()
    {
        
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
}
