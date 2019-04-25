using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconRenderer : MonoBehaviour {

    Material m_Material;
    public Material NormalMat;
    public Material DamageMat;
    public Beacon Baliza;
    public float TimeMaterialDamage;
    private float MaxTimeMaterialDamage;
    // Use this for initialization
    void Start () {
        m_Material = GetComponent<Renderer>().material;
        MaxTimeMaterialDamage = TimeMaterialDamage;
    }
	
	// Update is called once per frame
	void Update () {
		if (Baliza.isDamage)
        {
            m_Material = DamageMat;
            TimeMaterialDamage -= TimeMaterialDamage;
        }
        if(TimeMaterialDamage >= 0)
        {
            m_Material = NormalMat;
            TimeMaterialDamage = MaxTimeMaterialDamage;
            Baliza.isDamage = false
        }

	}
}
