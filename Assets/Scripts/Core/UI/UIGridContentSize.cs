using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGridContentSize : MonoBehaviour
{
    private const float SCREEN_SIZE_PORTRAIT_4_3 = 4f / 3f;
    [SerializeField] private GridLayoutGroup gridLayoutGroup;

    private void OnEnable()
    {
        this.FitScreenSize();
    }

    private void FitScreenSize()
    {
        var screen_ratio = (float)Screen.height / (float)Screen.width;
        if (Math.Round(screen_ratio, 1) == Math.Round(SCREEN_SIZE_PORTRAIT_4_3, 1))
        {
            this.gridLayoutGroup.cellSize = new Vector2(450f, 450f);
            this.gridLayoutGroup.spacing = new Vector2(150f, 150f);
        }
        else
        {
            this.gridLayoutGroup.cellSize = new Vector2(400f, 400f);
            this.gridLayoutGroup.spacing = new Vector2(100f, 100f);
        }
    }
}
