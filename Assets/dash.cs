using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dash : MonoBehaviour {
    public bool UsarHabilidad;
    public float dashspeed;
    private float dashTime;
    public float Cooldown;
    private float Max_Cooldown;
    public PlayerMoveController MovePlayer;




    // Use this for initialization
    void Start () {
        Max_Cooldown = Cooldown;
        //Max_Duracion = DuracionHabilidad;

    }
	
	// Update is called once per frame
	void Update () {
        Cooldown -= Time.deltaTime;

        if (Input.GetButtonDown("Dash") && Cooldown <= 0)
        {
            UsarHabilidad = true;
            //Debug.Break();
        }
        if (UsarHabilidad)
        {
           // Debug.Break();
            Vector3 final_pos = transform.position + (MovePlayer.move_dir * (dashspeed *10) * Time.deltaTime);
            Debug.Log("dash");
            UsarHabilidad = false;
            Cooldown = Max_Cooldown;
            if (!PeekNextPosition(final_pos))
            {
                //- Usar rotacion en vez de direccion;
                transform.position += MovePlayer.move_dir * (dashspeed * 10) * Time.deltaTime;
                Debug.Log("asda");
            }
        }
    }


    bool PeekNextPosition(Vector3 f_pos)
    {
        RaycastHit hit;

        Vector3 ray_dir = transform.position - f_pos;
        float dist = Vector3.Distance(transform.position, f_pos);
        Debug.Log(dist);
        Debug.DrawLine(transform.position, f_pos, Color.blue);
      //  Debug.Break();

        //Debug.Break();

        if (Physics.Raycast(transform.position, ray_dir.normalized, out hit, dist))
        {
            Debug.Log("Raycast");
            //- Take out LimitEnemigo and Attack enemy
            // Move to collision point
            if (hit.transform.gameObject.tag != "Player")
            {
                transform.position = hit.point;
                return true;
            }
            return false;
        }
        else
            return false;
    }


}


//Instantiate(DashEffectYellow.gameObject, transform.position, Quaternion.identity);
// moveInput = new Vector3(Input.GetAxisRaw("Horizontal") * dashspeed, 0f, Input.GetAxisRaw("Vertical") * dashspeed);
// dashTime = startDashTime;




//void MoveBullet()
//{
//    Vector3 final_pos = transform.position + bullet_dir * -bullet_speed * Time.deltaTime;
//    // Move only if no collision is found


//// Check next position the bullet will move to
//// Return true: Bullet will collide with something
//// Return false: Bullet will not collide


