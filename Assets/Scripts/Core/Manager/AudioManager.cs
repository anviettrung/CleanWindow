using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioManager : Singleton<AudioManager>
{
    [Header("UI Audio")]
    public AudioSource audioSrcClickButton;
    public AudioClip clickButtonClip;

    [Header("Gameplay Audio")]
    public AudioSource audioSource;
    public List<CWSound> cwSounds;
}

[System.Serializable]
public class CWSound
{
    public Window.State soundState;
    public AudioClip clipAtState;
}
