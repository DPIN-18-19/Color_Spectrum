using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenuManager : MonoBehaviour
{
    public static LevelMenuManager Instance { get; private set; }

    public LevelList levels_l;
    List<LevelMiniature> level_icons;

    LevelData selection;

    private void Awake()
    {
        if (Instance != null)
            Instance = this;
        else
            Destroy(gameObject);
        
        level_icons = new List<LevelMiniature>();
    }

    private void Start()
    {
        SceneManager.sceneLoaded += OnMenuLoaded;
    }

    void OnMenuLoaded(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "LevelSelection")
        {
            LoadLevelMenuData();
        }
    }

    void LoadLevelMenuData()
    {
        // Search all level objects in scene
        level_icons.AddRange(FindObjectsOfType<LevelMiniature>());

        // Insert level data
        for(int i = 0; i < level_icons.Count; ++i)
        {
            level_icons[i].data.Clone(SearchData(level_icons[i].name));
        }
    }

    LevelData SearchData(string id)
    {
        int index = levels_l.levels.FindIndex(x => x.id == id);
        return levels_l.levels[index];
    }

    public void MakeSelection(LevelData data)
    {
        selection.Clone(data);
    }

    public void Deselect()
    {
        selection.id = "";
    }
}
