using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelMenuManager : MonoBehaviour
{
    public LevelList levels_l;
    List<LevelMiniature> level_icons;

    private void Start()
    {
        level_icons = new List<LevelMiniature>();
        LoadLevelMenuData();
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
}
