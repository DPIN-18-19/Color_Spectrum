using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {

    Camera cam;

	// Use this for initialization
	void Start () {
        cam = GetComponent<Camera>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public Vector3 GetMousePosInPlane(Vector3 point_in_plane)
    {
        Ray cam_ray = cam.ScreenPointToRay(Input.mousePosition);
        Plane ground_plane = new Plane(Vector3.up, point_in_plane);
        float ray_length;
        ground_plane.Raycast(cam_ray, out ray_length);

        return cam_ray.GetPoint(ray_length);
        
    }
}
