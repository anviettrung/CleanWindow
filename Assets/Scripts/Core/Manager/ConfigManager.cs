using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigManager : Singleton<ConfigManager>
{
    public SettingConfig settingConfig;

    private void Awake()
    {
        this.LoadGameConfig();
    }

    private void LoadGameConfig()
    {
        var jsonContent = Resources.Load<TextAsset>("Configs/SettingConfig");
        this.settingConfig = JsonUtility.FromJson<SettingConfig>(jsonContent.text);
    }
}

public class SettingConfig
{
    public float Haptic_ContinuousIntensity;
    public float Haptic_ContinuousSharpness;
    public float Haptic_ContinuousDuration;
}
