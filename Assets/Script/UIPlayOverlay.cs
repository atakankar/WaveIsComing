using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPlayOverlay : MonoBehaviour
{

    public string levelToLoad = "levelSelect";
    public string InfinityLevel = "InfinityLevel";
    public SceneFader sceneFader;

    public void OnlinePlay()
    {

    }

    public void InfinityPlay()
    {
        sceneFader.FadeTo(InfinityLevel);
    }

    public void ChapterPlay()
    {
        sceneFader.FadeTo(levelToLoad);
    }
}

    

