using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeaconRenderer : MonoBehaviour {

    Renderer m_Material;
    public Material NormalMat;
    public Material DamageMat;
    Beacon Baliza;
    public float TimeMaterialDamage;
    private float MaxTimeMaterialDamage;
    // Use this for initialization
    void Start ()
    {
        Baliza = GetComponentInParent<Beacon>();
        m_Material = GetComponent<Renderer>();
        MaxTimeMaterialDamage = TimeMaterialDamage;
    }
	
	// Update is called once per frame
	void Update () {
		if (Baliza.isDamage)
        {
            Debug.Log("ChangeMaterial");
            m_Material.material = DamageMat;
            TimeMaterialDamage -= Time.deltaTime;
        }
        if(TimeMaterialDamage <= 0)
        {
            m_Material.material = NormalMat;
            TimeMaterialDamage = MaxTimeMaterialDamage;
            Baliza.isDamage = false;
        }

	}
}
