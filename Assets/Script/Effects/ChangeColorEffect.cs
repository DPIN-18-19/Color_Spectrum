using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeColorEffect : MonoBehaviour
{
    GameObject player;


	// Use this for initialization
	void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
	}
	
	// Update is called once per frame
	void Update ()
    {
        UpdateColor();
    }

    void UpdateColor()
    {
        int color = player.GetComponent<ColorChangingController>().GetColor();
        ParticleSystemRenderer ma = GetComponent<ParticleSystemRenderer>();
        SpriteRenderer spr = GetComponentInChildren<SpriteRenderer>();

        switch (color)
        {
            case 0:
                ma.material.color = Color.yellow;
                spr.material.color = Color.yellow;
                break;
            case 1:
                ma.material.color = Color.cyan;
                spr.material.color = Color.cyan;
                break;
            case 2:
                ma.material.color = Color.magenta;
                spr.material.color = Color.magenta;
                break;
            default:
                Debug.Log("Error in color particles");
                break;
        }
    }
}
