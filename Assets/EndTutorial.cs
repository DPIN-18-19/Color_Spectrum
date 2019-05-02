using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndTutorial : MonoBehaviour
{
    public KillCondition Enemy_Arena;
    public GameObject ParedNegra;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Enemy_Arena.kill_enemies.Count == 0)
        {
            ParedNegra.SetActive(false);
        }
    }
}
