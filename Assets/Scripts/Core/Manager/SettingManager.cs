using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;

public class SettingManager : Singleton<SettingManager>
{
    #region CONST_VARIALBES
    private const string KEY_SOUND = "Setting_Sound";
    private const string KEY_VIBRATION = "Setting_Vibration";
    #endregion

    public Toggle sound;
    public Toggle vibration;

    #region HAPTIC_CONTINUOUS_PARAMETERS
    private float ContinuousIntensity;
    private float ContinuousSharpness;
    private float ContinuousDuration;
    #endregion

    #region HAPTIC_DRUMS_PARAMETERS
    [SerializeField] private PresetItem presetItem;
    #endregion

    #region UNITY_CALLBACK
    private void Awake()
    {
        this.LoadSetting();
    }

    private void Start()
    {
        this.ContinuousIntensity = ConfigManager.Instance.settingConfig.Haptic_ContinuousIntensity;
        this.ContinuousSharpness = ConfigManager.Instance.settingConfig.Haptic_ContinuousSharpness;
        this.ContinuousDuration = ConfigManager.Instance.settingConfig.Haptic_ContinuousDuration;
    }
    #endregion

    #region SAVE/LOAD
    public void SaveSetting(UIToggle uiToggle)
    {
        switch (uiToggle.toggleType)
        {
            case UIToggle.EToggleType.SOUND:
                PlayerPrefs.SetString(KEY_SOUND, this.sound.isOn.ToString());
                break;
            case UIToggle.EToggleType.VIBRATION:
                PlayerPrefs.SetString(KEY_VIBRATION, this.vibration.isOn.ToString());
                break;
        }
    }

    public void LoadSetting()
    {
        if (PlayerPrefs.HasKey(KEY_SOUND))
        {
            this.sound.isOn = bool.Parse(PlayerPrefs.GetString(KEY_SOUND));
        }
        if (PlayerPrefs.HasKey(KEY_VIBRATION))
        {
            this.vibration.isOn = bool.Parse(PlayerPrefs.GetString(KEY_VIBRATION));
        }
    }
    #endregion

    #region FUNCTION
    /// <summary>
    /// click UI button
    /// </summary>
    public void OnClickButton()
    {
        if (this.vibration.isOn)
        {
            //Haptic: heavy
            MMVibrationManager.Haptic(HapticTypes.HeavyImpact, false, true, this);
            Debug.Log("<b> Onclick: Click button </b>");
        }
        if (this.sound.isOn)
        {

        }
    }

    /// <summary>
    /// glass step in game play
    /// </summary>
    public void OnGlassStep()
    {
        if (this.vibration.isOn)
        {
            //Haptic: soft
            MMVibrationManager.Haptic(HapticTypes.SoftImpact, false, true, this);
            Debug.Log("<b> Onclick: Glass Step </b>");
        }
        if (this.sound.isOn)
        {

        }
    }

    /// <summary>
    /// clean step in game play
    /// </summary>
    public void OnCleanStep()
    {
        if (this.vibration.isOn)
        {
            //Haptic: continuous
            MMVibrationManager.ContinuousHaptic(this.ContinuousIntensity, this.ContinuousSharpness, this.ContinuousDuration, HapticTypes.LightImpact, this, true);
            Debug.Log("<b> Onclick: Clean Step </b>");
        }
        if (this.sound.isOn)
        {

        }
    }

    /// <summary>
    /// break glass step in game play
    /// </summary>
    public void OnBreakStep()
    {
        if (this.vibration.isOn)
        {
            //Haptic: medium
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
            Debug.Log("<b> Onclick: Break Step </b>");
        }
        if (this.sound.isOn)
        {

        }
    }

    /// <summary>
    /// all steps have been completed
    /// </summary>
    public void OnCompleteLevel()
    {
        if (this.vibration.isOn)
        {
            //Haptic: success
            MMVibrationManager.Haptic(HapticTypes.Success, false, true, this);
            Debug.Log("<b> Onclick: Complete Level </b>");
        }
        if (this.sound.isOn)
        {

        }
    }

    /// <summary>
    /// step capture image
    /// </summary>
    public void OnTakePhoto()
    {
        if (this.vibration.isOn)
        {
            //Haptic: medium
            MMVibrationManager.Haptic(HapticTypes.MediumImpact, false, true, this);
            Debug.Log("<b> Onclick: Take Photo </b>");
        }
        if (this.sound.isOn)
        {

        }
    }

    /// <summary>
    /// after taking the photo
    /// </summary>
    public void OnAfterTakePhoto()
    {
        if (this.vibration.isOn)
        {
            MMVibrationManager.AdvancedHapticPattern(this.presetItem.AHAPFile.text, this.presetItem.WaveFormAsset.WaveForm.Pattern
                , this.presetItem.WaveFormAsset.WaveForm.Amplitudes, -1, this.presetItem.RumbleWaveFormAsset.WaveForm.Pattern,
                this.presetItem.RumbleWaveFormAsset.WaveForm.LowFrequencyAmplitudes, this.presetItem.RumbleWaveFormAsset.WaveForm.HighFrequencyAmplitudes, -1,
                HapticTypes.LightImpact, this);
            Debug.Log("<b> Onclick: Take photo done </b>");
        }
        if (this.sound.isOn)
        {

        }
    }
    #endregion
}

/// <summary>
/// Setting for Haptic: Drums
/// </summary>
[System.Serializable]
public class PresetItem
{
    public string Name;
    public TextAsset AHAPFile;
    public MMNVAndroidWaveFormAsset WaveFormAsset;
    public MMNVRumbleWaveFormAsset RumbleWaveFormAsset;
}
