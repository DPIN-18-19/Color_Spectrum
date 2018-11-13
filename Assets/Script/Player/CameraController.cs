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

    public Vector3 GetMousePos()
    {
        Ray cam_ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Plane ground_plane = new Plane(Vector3.up, Vector3.zero);
        float ray_length;
        ground_plane.Raycast(cam_ray, out ray_length);

        return cam_ray.GetPoint(ray_length);
    }

    public Vector3 GetMousePosInPlane(Vector3 point_in_plane)
    {
        Ray cam_ray = cam.ScreenPointToRay(new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0));
        Plane ground_plane = new Plane(Vector3.up, point_in_plane);
        float ray_length;
        ground_plane.Raycast(cam_ray, out ray_length);

        return cam_ray.GetPoint(ray_length);
    }

    public float CalculateOffset(Vector3 center, Vector3 point, float max_dist, float max_offset)
    {
        //Given the distance between player and mouse, make an offset

        Vector3 a_vector = center - point;
        float dist = a_vector.magnitude;

        return max_offset * (dist / max_dist);
    }

    public Vector3 CalculatePointInCircleFromCenter(Vector3 center, Vector3 perimeter_point, float dist)
    {
        Vector3 radius = perimeter_point - center;

        // Distance passed is too large
        if (radius.magnitude < dist)
            return perimeter_point;

        Vector3 new_point = radius.normalized * dist;

        Debug.DrawLine(center, center + new_point, Color.red);
        return center + new_point;
    }
}
