using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveController : MonoBehaviour
{
    //Components
    private Rigidbody rb;
    private Camera cam;
    
    // Variables
    public float move_speed;            // PLayer's speed

    private Vector3 move_dir;           // Player direction
    private Vector3 move_velocity;      // Player new velocity vector

    // Use this for initialization
    void Start ()
    {
        // Initialize
        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();   // Consider changing it if more than one camera in the scene
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
        move_dir = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
        move_dir.Normalize();
        move_velocity = move_dir * move_speed;
    }

    // Prepare when animations come
    void Rotation()
    {
        // Calculate ray from mouse position 
        Ray cam_ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground_plane = new Plane(Vector3.up, Vector3.zero);
        float ray_length;

        if (ground_plane.Raycast(cam_ray, out ray_length))
        {
            Vector3 point_to_look = cam_ray.GetPoint(ray_length);
            //Debug.DrawLine(cam_ray.origin, point_to_look, Color.blue);
            transform.LookAt(new Vector3(point_to_look.x, transform.position.y, point_to_look.z));
        }
    }
}
