using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerJaneMoveController : MonoBehaviour {
    //Components
    private Rigidbody rb;
    private Camera cam;
    Animator anim;
    public bool Move;
    public CinemachineVirtualCamera Cam1;
    public bool CanMove = true;
    //public float TimeStopGrenade;

    // Variables
    public float move_speed;            // PLayer's speed

    public Vector3 move_dir;           // Player direction
    private Vector3 move_velocity;
    // Player new velocity vector
    //public LensSettings m_Lens;

    // Use this for initialization
    void Start()
    {
        move_speed = GetComponent<PlayerStats>().speed;
        // Initialize
        SetUpAnimation();
        anim.SetBool("Move", Move);

        rb = GetComponent<Rigidbody>();
        cam = FindObjectOfType<Camera>();   // Consider changing it if more than one camera in the scene
    }

    // Update is called once per frame
    void Update()
    {
        if(CanMove)
       Movement();
       // TimeStopGrenade += Time.deltaTime; 

        //Rotation();
    }

    private void FixedUpdate()
    {

        rb.velocity = move_velocity;
        
    }

    float animHorizontal = 0;
    float animVertical = 0;

    [SerializeField]
    float animInterpolation = 1;

    void Movement()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");

        move_dir = new Vector3(horizontal, 0f, vertical);
        move_dir.Normalize();
        move_velocity = move_dir * move_speed;

        Vector3 relativevelocity = transform.InverseTransformDirection(new Vector3(horizontal, 0, vertical));
        animHorizontal = Mathf.Lerp(animHorizontal, relativevelocity.x, animInterpolation * Time.deltaTime);
        animVertical = Mathf.Lerp(animVertical, relativevelocity.z, animInterpolation * Time.deltaTime);
       
        Move = true;
        if (horizontal == 0 && vertical == 0)
        {
            Move = false;
        }
       // if (TimeStopGrenade < 1)
       // {
       //     move_velocity = move_dir * 0;
       // }

        anim.SetFloat("VelocidadX", animHorizontal);
        anim.SetFloat("VelocidadZ", animVertical);
        anim.SetBool("Move", Move);

        
    }
    void SetUpAnimation()
    {
        anim = GetComponent<Animator>();

        foreach (var childAnimator in GetComponentsInChildren<Animator>())
        {
            if (childAnimator != anim)
            {
                anim.avatar = childAnimator.avatar;
                Destroy(childAnimator);
                break;

            }
        }
    }

    //// Prepare when animations come
    //void Rotation()
    //{
    //    // Calculate ray from mouse position 
    //    Ray cam_ray = cam.ScreenPointToRay(Input.mousePosition);
    //   // Cam1.m_Lens
    //    Plane ground_plane = new Plane(Vector3.up, Vector3.zero);
    //    float ray_length;

    //    if (ground_plane.Raycast(cam_ray, out ray_length))
    //    {
    //        Vector3 point_to_look = cam_ray.GetPoint(ray_length);
    //        Debug.DrawLine(cam_ray.origin, point_to_look, Color.blue);
            

    //        //LookAtAxis(new Vector3(point_to_look.x, transform.GetChild(0).position.y, point_to_look.z));

    //        transform.GetChild(0).Rotate(0, LookAtAxis(point_to_look), 0);

    //        Debug.Log("Rota");
    //        Debug.Log(point_to_look.x);
    //        Debug.Log(transform.GetChild(0).position.y);
    //        Debug.Log(point_to_look.z);


    //        ////
    //    }
    //}
    //public float LookAtAxis(Vector3 look_at)
    //{
    //    // Calculate point to look at

    //    //Vector3 to_target = transform.GetChild(0).position - look_at;
    //    Vector3 projection = Vector3.ProjectOnPlane(look_at - transform.GetChild(0).position, Vector3.up);

    //    // Calculate Angle between current transform.front and object to look at
    //    return Vector3.SignedAngle(transform.GetChild(0).forward, projection, Vector3.up) - 180;
    //}
}
// forward z
// right (turn) x
