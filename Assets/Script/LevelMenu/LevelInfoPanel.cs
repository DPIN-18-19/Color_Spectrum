using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class LevelInfoPanel : MonoBehaviour
{
    // Text data
    TextMeshProUGUI name_t;
    TextMeshProUGUI brief_t;
    TextMeshProUGUI highscore_t;
    TextMeshProUGUI grade_t;

    // Go Button
    Button go_b;
    string scene_name;

    // Cancel button
    Button cancel_b;

    private void Start()
    {
        Init();

        go_b = transform.Find("Go_b").GetComponent<Button>();
        go_b.onClick.AddListener(LoadSelectedLevel);

        cancel_b = transform.Find("Cancel_b").GetComponent<Button>();
        cancel_b.onClick.AddListener(CancelSelected);
    }

    // Función con visibilidad public para poder ser usada en la carga del nivel
    // Incluir solamente datos que vayan a ser usados previo al start del objeto
    public void Init()
    {
        name_t = transform.Find("Name_t").GetComponent<TextMeshProUGUI>();
        brief_t = transform.Find("Brief_t").GetComponent<TextMeshProUGUI>();
        highscore_t = transform.Find("HighScore_t").GetComponent<TextMeshProUGUI>();
        grade_t = transform.Find("Grade_t").GetComponent<TextMeshProUGUI>();
    }

    public void UpdateInfo(LevelData data)
    {
        name_t.text = data.name;
        brief_t.text = data.brief;
        highscore_t.text = data.highscore.ToString();
        
        if (!data.complete)
            grade_t.text = "-";
        else
        {
            grade_t.text = data.grade.grade;
            grade_t.material = data.grade.mat;
        }

        scene_name = data.scene_name;
    }

    void LoadSelectedLevel()
    {
        if (scene_name != "")
            SceneMan1.Instance.LoadSceneByName(scene_name);
        else
            Debug.Log("Scene name is missing");
    }

    void CancelSelected()
    {
        LevelMenuManager.Instance.Deselect();
    }
}
