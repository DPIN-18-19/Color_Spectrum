using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishTutorial : MonoBehaviour {
    public AreaCondition lvl_trigger;
    // Use this for initialization
    void Start () {
        lvl_trigger.EnterArea += NextLevel;
    }
	
	// Update is called once per frame
	void NextLevel()
    {
        SceneMan1.Instance.LoadSceneByName("Tuto_Customizacion");
    }

}
