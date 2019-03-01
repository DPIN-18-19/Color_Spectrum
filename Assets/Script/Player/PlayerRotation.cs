using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour
{
    public Camera cam;              // Camara del juego. Tratar de no
    
    private float weapon_height;    // Altura del arma

    // Use managers better at some point
    //public ShowScore score;
    //public Menu_Pausa pause;


    private void Start()
    {
        //cam = FindObjectOfType<Camera>();
        if (PlayerManager.Instance != null)
        {
            if (PlayerManager.Instance.GetNumWeapons() == 0)
            {
                Debug.Log("No weapons");
                weapon_height = transform.position.y;       // Asignar altura del jugador al inicio
            }
        }
    }

    private void LateUpdate()
    {
        Rotation();
    }

    // Prepare when animations come
    void Rotation()
    {
        if (!PauseMan.Instance.is_pause && !ScoreManager.Instance.is_result_active)
        {
            // Calculate ray from mouse position 
            Ray cam_ray = cam.ScreenPointToRay(Input.mousePosition);
            // Cam1.m_Lens
            Vector3 plane_pos = new Vector3(transform.position.x, weapon_height, transform.position.z);
            Plane ground_plane = new Plane(Vector3.up, plane_pos);
            float ray_length;

            Debug.DrawLine(cam_ray.origin, cam_ray.direction * 100, Color.red);

            if (ground_plane.Raycast(cam_ray, out ray_length))// && !pause.GameIsPaused && !ScoreManager.Instance.is_result_active)
            {
                Vector3 point_to_look = cam_ray.GetPoint(ray_length);
                Debug.DrawLine(cam_ray.origin, point_to_look, Color.blue);

                //transform.LookAt(point_to_look);
                float angle = LookAtAxis(new Vector3(point_to_look.x, transform.GetChild(0).position.y, point_to_look.z));
                transform.rotation = Quaternion.LookRotation(Quaternion.Euler(0, angle, 0) * transform.forward, Vector3.up);
            }
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

    public void SetNewGunHeight(Transform pos)
    {
        Debug.Log("Here");
        weapon_height = pos.position.y;
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