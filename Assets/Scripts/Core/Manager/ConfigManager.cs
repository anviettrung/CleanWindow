using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    public VibrationConfig vibrationConfig;
    public GameNumberConfig gameNumberConfig;

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
