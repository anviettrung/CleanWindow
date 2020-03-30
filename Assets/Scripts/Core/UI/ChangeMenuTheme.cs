using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMenuTheme : MonoBehaviour
{
    [Header("Theme Data")]
    public ListThemeData themeDatas;

    [Header("Home Background")]
    public SpriteRenderer walll;
    public SpriteRenderer furniture;

    public void OnClickChangeTheme(int themeID)
    {
        this.walll.sprite = this.themeDatas.Theme[themeID - 1].WallSprite;
        this.furniture.sprite = this.themeDatas.Theme[themeID - 1].FurnitureSprite;
    }
}
