using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IAbiChipData : IChipData
{
    public AbilityData abi_data;
    public string abi_name;

	// Use this for initialization
	void Start ()
    {
        canvas = GetComponentInParent<Canvas>().transform;          // Coger el canvas de la interfaz
    }
}
