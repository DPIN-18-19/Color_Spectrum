using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimPruebas : MonoBehaviour {
    protected Animator anim;
   
    public bool attack_moving;             // Pose de atacar desplazandose
    
    public bool attack_in_place;           // Pose de atacar quieto
    protected float correct_look;             // Aplicar animacion de rotacion
    public GameObject Shield;
    // Use this for initialization
    void Start () {
        anim = GetComponent<Animator>();
        Shield.SetActive(false);
    }
	
	// Update is called once per frame
	void Update () {
        anim.SetBool("Attack", attack_moving);
        anim.SetBool("AttackStop", attack_in_place);
        if (attack_moving == false )
        {
            Shield.SetActive(false);
        }
        if (attack_moving == true)
        {
            Shield.SetActive(true);
        }
    }
}
