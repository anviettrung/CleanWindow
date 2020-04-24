using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MoreMountains.NiceVibrations;
using UnityEngine.UI;

public class VibrationManager : Singleton<VibrationManager>
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
        this.ContinuousIntensity = ConfigManager.Instance.vibrationConfig.Haptic_ContinuousIntensity;
        this.ContinuousSharpness = ConfigManager.Instance.vibrationConfig.Haptic_ContinuousSharpness;
        this.ContinuousDuration = ConfigManager.Instance.vibrationConfig.Haptic_ContinuousDuration;
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
            //Debug.Log("<b> Onclick: Click button </b>");
        }
        if (this.sound.isOn)
        {
            //AudioManager.Instance.audioSource.clip = AudioManager.Instance.clickButtonClip;
            //AudioManager.Instance.audioSource.loop = false;
            //AudioManager.Instance.audioSource.Play();
            AudioManager.Instance.audioSrcClickButton.Play();
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
            //Debug.Log("<b> Onclick: Glass Step </b>");
        }
        if (this.sound.isOn)
        {
            if (!AudioManager.Instance.audioSource.isPlaying)
            {
                var clip = AudioManager.Instance.cwSounds.Find(sound => sound.soundState == Window.State.DIRTY).clipAtState;
                if (AudioManager.Instance.audioSource.clip != clip)
                {
                    AudioManager.Instance.audioSource.clip = clip;
                }
                AudioManager.Instance.audioSource.loop = true;
                AudioManager.Instance.audioSource.PlayOneShot(AudioManager.Instance.audioSource.clip);
            }
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
            //Debug.Log("<b> Onclick: Clean Step </b>");
        }
        if (this.sound.isOn)
        {
            if (!AudioManager.Instance.audioSource.isPlaying)
            {
                var clip = AudioManager.Instance.cwSounds.Find(sound => sound.soundState == Window.State.WET).clipAtState;
                if (AudioManager.Instance.audioSource.clip != clip)
                {
                    AudioManager.Instance.audioSource.clip = clip;
                }
                AudioManager.Instance.audioSource.loop = true;
                AudioManager.Instance.audioSource.PlayOneShot(AudioManager.Instance.audioSource.clip);
            }
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
            //Debug.Log("<b> Onclick: Break Step </b>");
        }
        if (this.sound.isOn)
        {
            //AudioManager.Instance.audioSource.clip = AudioManager.Instance.cwSounds.Find(sound => sound.soundState == Window.State.BREAK_GLASS).clipAtState;
            //AudioManager.Instance.audioSource.loop = false;
            //AudioManager.Instance.audioSource.Play();
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
            //Debug.Log("<b> Onclick: Complete Level </b>");
        }
        if (this.sound.isOn)
        {
            AudioManager.Instance.audioSource.clip = AudioManager.Instance.cwSounds.Find(sound => sound.soundState == Window.State.COMPLETE).clipAtState;
            AudioManager.Instance.audioSource.loop = false;
            AudioManager.Instance.audioSource.Play();
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
            //Debug.Log("<b> Onclick: Take Photo </b>");
        }
        if (this.sound.isOn)
        {
            AudioManager.Instance.audioSource.clip = AudioManager.Instance.cwSounds.Find(sound => sound.soundState == Window.State.CAPTURE).clipAtState;
            AudioManager.Instance.audioSource.loop = false;
            AudioManager.Instance.audioSource.Play();
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
            //Debug.Log("<b> Onclick: Take photo done </b>");
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
