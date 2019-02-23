using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    public static LevelMenuManager Instance { get; private set; }

    public LevelList levels_l;
    List<LevelMiniature> level_icons;
    Transform info_p;

    public LevelData selection;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);

        level_icons = new List<LevelMiniature>();
        selection.id = "";
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnMenuLoaded;
    }

    void OnMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "LevelSelection")
        {
            Debug.Log("Home");
            LoadLevelMenuData();
            info_p = FindObjectOfType<LevelInfoPanel>().transform;
            info_p.GetComponent<LevelInfoPanel>().Init();
            info_p.gameObject.SetActive(false);
        }
    }

    void LoadLevelMenuData()
    {
        level_icons.Clear();

        // Search all level objects in scene
        level_icons.AddRange(FindObjectsOfType<LevelMiniature>());

        // Insert level data
        for(int i = 0; i < level_icons.Count; ++i)
        {
            level_icons[i].data.Clone(SearchData(level_icons[i].name));
        }
        selection.id = "";
    }

    LevelData SearchData(string id)
    {
        int index = levels_l.levels.FindIndex(x => x.id == id);
        return levels_l.levels[index];
    }

    public bool MakeSelection(LevelData data)
    {
        if (selection.id != "")
            return false;

        selection.Clone(data);
        info_p.gameObject.SetActive(true);
        info_p.GetComponent<LevelInfoPanel>().UpdateInfo(selection);
        return true;
    }

    public void Deselect()
    {
        // Deseleccionar objeto específico
        for(int i = 0; i < level_icons.Count; ++i)
        {
            if (level_icons[i].data.id == selection.id)
                level_icons[i].Deselect();
        }

        selection.id = "";
        info_p.gameObject.SetActive(false);
    }

    public void UpdateScoreInfo()
    {
        for(int i = 0; i < levels_l.levels.Count; ++i)
        {
            if(levels_l.levels[i].id == selection.id)
            {
                if (levels_l.levels[i].highscore < ScoreManager.Instance.GetFinalScore())
                {
                    levels_l.levels[i].highscore = ScoreManager.Instance.GetFinalScore();
                    levels_l.levels[i].grade.Clone(ScoreManager.Instance.GetGrade());

                    levels_l.levels[i].complete = true;
                }

                return;
            }
        }
    }
}
