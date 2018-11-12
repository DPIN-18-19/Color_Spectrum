using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    //Components
    private Rigidbody rb;
    public  CameraController cam1;
    private Camera cam;
    Animator anim;
    
    // Variables
    public float move_speed;            // PLayer's speed

    public Vector3 move_dir;           // Player direction
    private Vector3 move_velocity;      // Player new velocity vector

    // Use this for initialization
    void Start ()
    {
        // Initialize
        SetUpAnimation();

        rb = GetComponent<Rigidbody>();
        //cam = FindObjectOfType<Camera>();   // Consider changing it if more than one camera in the scene
    }
	
	// Update is called once per frame
	void Update ()
    {
        Movement();
        Rotation();
    }

    private void FixedUpdate()
    {
       
        rb.velocity = move_velocity;
    }

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal") ;
        float vertical = Input.GetAxisRaw("Vertical");

        move_dir = new Vector3(horizontal, 0f, vertical);
        move_dir.Normalize();
        move_velocity = move_dir * move_speed;

        anim.SetFloat("VelocidadX", horizontal);
        anim.SetFloat("VelocidadZ", vertical);
    }
    void SetUpAnimation ()
    {
        anim = GetComponent<Animator>();
        Debug.Log("Hola");

        foreach ( var childAnimator in GetComponentsInChildren<Animator>())
        {
            if(childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;
                
            }
        }
    }
   
    // Prepare when animations come
    void Rotation()
    {
        // Calculate ray from mouse position 
        //Ray cam_ray = cam.ScreenPointToRay(Input.mousePosition);
        //Plane ground_plane = new Plane(Vector3.up, Vector3.zero);
        //float ray_length;

        //if (ground_plane.Raycast(cam_ray, out ray_length))
        //{
        Vector3 point_to_look = cam1.GetMousePosInPlane(transform.position);
        Debug.DrawLine(point_to_look, transform.position, Color.blue);
        transform.LookAt(new Vector3(point_to_look.x, transform.position.y, point_to_look.z));
        //}
    }
}
// forward z
// right (turn) x
