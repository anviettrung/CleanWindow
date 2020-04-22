using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToggle : MonoBehaviour
{
    public EToggleType toggleType;

    public enum EToggleType
    {
        SOUND,
        VIBRATION
    }
}
