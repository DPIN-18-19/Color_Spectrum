using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class LevelInfoPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    // Text data
    TextMeshProUGUI name_t;
    TextMeshProUGUI brief_t;
    TextMeshProUGUI highscore_t;
    TextMeshProUGUI grade_t;
    TextMeshProUGUI difficulty_t;

    // Other text
    Transform highscore;
    Transform grade;

    // Go Button
    Button go_b;
    string scene_name;

    // Cancel button
    Button cancel_b;
    bool is_selected = false;
    [HideInInspector]
    public bool allow_cancel = true;

    private void Start()
    {
        Init();

        go_b.onClick.AddListener(LoadSelectedLevel);

        //cancel_b = transform.Find("Cancel_b").GetComponent<Button>();
        //cancel_b.onClick.AddListener(CancelSelected);
    }

    // Función con visibilidad public para poder ser usada en la carga del nivel
    // Incluir solamente datos que vayan a ser usados previo al start del objeto
    public void Init()
    {
        name_t = transform.Find("Name_t").GetComponent<TextMeshProUGUI>();
        //brief_t = transform.Find("Brief_t").GetComponent<TextMeshProUGUI>();
        highscore_t = transform.Find("HighScore_t").GetComponent<TextMeshProUGUI>();
        grade_t = transform.Find("Grade_t").GetComponent<TextMeshProUGUI>();
        difficulty_t = transform.Find("Difficulty_t").GetComponent<TextMeshProUGUI>();

        highscore = transform.Find("HighScore");
        grade = transform.Find("Grade");

        go_b = transform.Find("Go_b").GetComponent<Button>();
    }

    private void Update()
    {
        if(is_selected && allow_cancel)
        {
            Debug.Log("Allowed");
            if(Input.GetMouseButtonDown(0))
            {
                CancelSelected();
            }
        }
    }

    public void TextAppear()
    {
        name_t.transform.gameObject.SetActive(true);
        highscore_t.transform.gameObject.SetActive(true);
        grade_t.transform.gameObject.SetActive(true);
        highscore.transform.gameObject.SetActive(true);
        grade.transform.gameObject.SetActive(true);
        go_b.transform.gameObject.SetActive(true);
        difficulty_t.transform.gameObject.SetActive(true);
    }

    public void TextDisappear()
    {
        name_t.transform.gameObject.SetActive(false);
        highscore_t.transform.gameObject.SetActive(false);
        grade_t.transform.gameObject.SetActive(false);
        highscore.transform.gameObject.SetActive(false);
        grade.transform.gameObject.SetActive(false);
        go_b.transform.gameObject.SetActive(false);
        difficulty_t.transform.gameObject.SetActive(false);
    }

    public void UpdateInfo(LevelData data)
    {
        TextAppear();

        name_t.text = data.name;
        //brief_t.text = data.brief;
        highscore_t.text = data.highscore.ToString();
        difficulty_t.text = data.difficulty;
        
        if (!data.complete)
            grade_t.text = "-";
        else
        {
            grade_t.text = data.grade.grade;
            grade_t.material = data.grade.mat;
        }

        scene_name = data.scene_name;

        // Evitar que se deseleccione en el mismo frame
        StartCoroutine(FrameSkip());
    }

    public IEnumerator FrameSkip()
    {
        allow_cancel = false;

        yield return new WaitForEndOfFrame();
        is_selected = true;
        //allow_cancel = true;
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
        Debug.Log("Deselection");
        TextDisappear();
        LevelMenuManager.Instance.Deselect();
        is_selected = false;
    }

    public void OnPointerEnter(PointerEventData p_data)
    {
        Debug.Log("In button");
        allow_cancel = false;
    }
    public void OnPointerExit(PointerEventData p_data)
    {
        Debug.Log("Out button");
        allow_cancel = true;
    }
}
