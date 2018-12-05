using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    public Camera cam;

    private void Start()
    {
        //cam = FindObjectOfType<Camera>();
    }

    private void LateUpdate()
    {
        Rotation();
    }

    // Prepare when animations come
    void Rotation()
    {
        // Calculate ray from mouse position 
        Ray cam_ray = cam.ScreenPointToRay(Input.mousePosition);
        // Cam1.m_Lens
        Plane ground_plane = new Plane(Vector3.up, transform.position);
        float ray_length;

        Debug.DrawLine(cam_ray.origin, cam_ray.direction * 100, Color.red);

        if (ground_plane.Raycast(cam_ray, out ray_length))
        {
            Vector3 point_to_look = cam_ray.GetPoint(ray_length);
            Debug.DrawLine(cam_ray.origin, point_to_look, Color.blue);

            //transform.LookAt(point_to_look);
            float angle = LookAtAxis(new Vector3(point_to_look.x, transform.GetChild(0).position.y, point_to_look.z));
            transform.rotation = Quaternion.LookRotation(Quaternion.Euler(0, angle, 0) * transform.forward, Vector3.up);

            //transform.rotation = Quaternion.LookRotation((point_to_look - transform.position).normalized,Vector3.up);



            //Debug.Log("Rota");
            //Debug.Log(point_to_look.x);
            //Debug.Log(transform.position.y);
            //Debug.Log(point_to_look.z);


            ////
        }
    }
    public float LookAtAxis(Vector3 look_at)
    {
        // Calculate point to look at

        //Vector3 to_target = transform.GetChild(0).position - look_at;
        Vector3 projection = Vector3.ProjectOnPlane(look_at - transform.position, Vector3.up);

        // Calculate Angle between current transform.front and object to look at
        return Vector3.SignedAngle(transform.forward, projection, Vector3.up);
    }
}
// Calculate ray from mouse position 


//Ray cam_ray = cam.ScreenPointToRay(Input.mousePosition);
//Plane ground_plane = new Plane(Vector3.up, Vector3.zero);
//float ray_length;

//        if (ground_plane.Raycast(cam_ray, out ray_length))
//        {
//            Vector3 point_to_look = cam_ray.GetPoint(ray_length);
////Debug.DrawLine(cam_ray.origin, point_to_look, Color.blue);
//transform.LookAt(new Vector3(point_to_look.x, transform.position.y, point_to_look.z));