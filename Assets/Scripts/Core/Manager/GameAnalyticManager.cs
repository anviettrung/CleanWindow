using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GameAnalyticManager : Singleton<GameAnalyticManager>
{
    private void Awake()
    {
        GameAnalytics.Initialize();
    }

    public string Level
    {
        get
        {
            return "Level " + LevelManager.Instance.currentLevel.ToString();
        }
    }

    public void StartLevel()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, this.Level);
        //Debug.Log("<color=red>GameAnalytic: Start</color>");
    }

    public void EndLevel()
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, this.Level);
        //Debug.Log("<color=cyan>GameAnalytic: Complete</color>");
    }
}
