using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    public VibrationConfig vibrationConfig;
    public GameNumberConfig gameNumberConfig;
    public BonusConfig bonusConfig;
    public ArrayHashTagConfig arrayHashTagConfig;
    public AdmobConfig admobConfig;

    private void Awake()
    {
        this.LoadGameConfig();
    }

    /// <summary>
    /// Load all configs
    /// </summary>
    private void LoadGameConfig()
    {
        this.LoadVibrationConfig();
        this.LoadGameNumberConfig();
        this.LoadBonusConfig();
        this.LoadHashTagConfig();
        this.LoadAdmobConfig();
    }

    /// <summary>
    /// Setting config
    /// </summary>
    private void LoadVibrationConfig()
    {
        var jsonContent = Resources.Load<TextAsset>("Configs/VibrationConfig");
        this.vibrationConfig = JsonUtility.FromJson<VibrationConfig>(jsonContent.text);
    }

    /// <summary>
    /// Game number config
    /// </summary>
    private void LoadGameNumberConfig()
    {
        var jsonContent = Resources.Load<TextAsset>("Configs/GameNumberConfig");
        this.gameNumberConfig = JsonUtility.FromJson<GameNumberConfig>(jsonContent.text);
    }

    private void LoadBonusConfig()
    {
        var jsonContent = Resources.Load<TextAsset>("Configs/BonusConfig");
        this.bonusConfig = JsonUtility.FromJson<BonusConfig>(jsonContent.text);
    }

    private void LoadHashTagConfig()
    {
        var jsonContent = Resources.Load<TextAsset>("Configs/HashTagConfig");
        this.arrayHashTagConfig = JsonUtility.FromJson<ArrayHashTagConfig>(jsonContent.text);
    }

    private void LoadAdmobConfig()
    {
        var jsonContent = Resources.Load<TextAsset>("Configs/AdmobConfig");
        this.admobConfig = JsonUtility.FromJson<AdmobConfig>(jsonContent.text);
    }
}

public class VibrationConfig
{
    public float Haptic_ContinuousIntensity;
    public float Haptic_ContinuousSharpness;
    public float Haptic_ContinuousDuration;
}

public class GameNumberConfig
{
    public float MaxGameToGetGiftBox;
}

public class BonusConfig
{
    public int BonusPerLevel;
    public int PriceToUnlockItem;
}

public class ArrayHashTagConfig
{
    public HashTagConfig[] CityHashTag;
}

[System.Serializable]
public class HashTagConfig
{
    public int Level;
    public string CityName;
    public string HashTag;
}

public class AdmobConfig
{
    public string AppURL_Android;
    public string AppURL_iOS;
    public string AdmobAppID_Android;
    public string AdmobAppID_iOS;
    public string AdmobRewardVideoID_Android;
    public string AdmobRewardVideoID_iOS;
    public string AdmobBannerID_Android;
    public string AdmobBannerID_iOS;
    public string AdmobInterstitialID_Android;
    public string AdmobInterstitialID_iOS;
}
