using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuFlow : MonoBehaviour
{
    Transform prepare_b, level_b, store_b, custom_b;

	// Use this for initialization
	void Start ()
    {
        // Buscar botones en la escena
        prepare_b = transform.Find("Prepare_b");
        level_b = transform.Find("Level_b");
        store_b = transform.Find("Store_b");
        custom_b = transform.Find("Custom_b");

        // Comprobar si cada botón fue encontrado
        if (prepare_b)
            prepare_b.GetComponent<Button>().onClick.AddListener(ToPrepare);

        if (level_b)
            level_b.GetComponent<Button>().onClick.AddListener(ToLevel);

        if (store_b)
            store_b.GetComponent<Button>().onClick.AddListener(ToStore);

        if (custom_b)
            custom_b.GetComponent<Button>().onClick.AddListener(ToCustom);
    }

    void ToPrepare()
    {
        SceneMan1.Instance.LoadSceneByName("PreparationMenu");
    }
    void ToLevel()
    {
        SceneMan1.Instance.LoadSceneByName("LevelSelection");
    }
    void ToStore()
    {
        SceneMan1.Instance.LoadSceneByName("Store");
    }
    void ToCustom()
    {
        SceneMan1.Instance.LoadSceneByName("Customizacion");
    }
}
