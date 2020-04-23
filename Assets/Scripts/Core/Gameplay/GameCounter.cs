﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Count the number of games played
/// </summary>
public class GameCounter : Singleton<GameCounter>
{
    #region CONST
    private const string KEY_GAMECOUNT = "GameCount"; 
    #endregion

    #region FIELDS
    [HideInInspector] public float maxGameToGetGift;
    private int totalGamePlayed; 
    #endregion

    #region PROPERTIES
    public int TotalGamePlayed
    {
        get
        {
            if (this.totalGamePlayed > this.maxGameToGetGift)
            {
                return 0;
            }
            else
            {
                return this.totalGamePlayed;
            }
        }
        set
        {
            this.totalGamePlayed = value;
        }
    }
    #endregion

    #region UNITY_CALLBACK
    private void Start()
    {
        this.maxGameToGetGift = ConfigManager.Instance.gameNumberConfig.MaxGameToGetGiftBox;
    }
    #endregion

    #region SAVE/LOAD DATA
    public void SaveTotalGamePlayed(int totalGame)
    {
        PlayerPrefs.SetInt(KEY_GAMECOUNT, totalGame);
    }

    public void LoadTotalGamePlayed()
    {
        if (PlayerPrefs.HasKey(KEY_GAMECOUNT))
        {
            this.TotalGamePlayed = PlayerPrefs.GetInt(KEY_GAMECOUNT);
        }
    } 
    #endregion
}
