using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIProgressbar : MonoBehaviour
{
    [SerializeField] private Image progressImage;

    private float valueProgress;
    public float ValueProgress
    {
        get
        {
            return this.valueProgress;
        }
        set
        {
            this.valueProgress = value;
            this.progressImage.fillAmount = this.valueProgress;
        }
    }
}
