using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EffectController : MonoBehaviour
{
    public float life_time = 0.1f;

	// Use this for initialization
	void Start ()
    {
        StartCoroutine(DestroyEffect());
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    IEnumerator DestroyEffect()
    {
        yield return new WaitForSeconds(life_time);
        Destroy(this.gameObject);

    }
}
