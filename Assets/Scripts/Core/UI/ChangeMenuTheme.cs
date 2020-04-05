using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChangeMenuTheme : MonoBehaviour
{
    [Header("Theme Data")]
    public ListThemeData themeDatas;

    [Header("Home Background")]
    public SpriteRenderer walll;
    public SpriteRenderer furniture;

    public void OnClickChangeTheme()
    {
        var selected_object = EventSystem.current.currentSelectedGameObject;
        var themeID = selected_object.transform.GetSiblingIndex() + 1;
        var theme = Array.Find(this.themeDatas.Theme, t => t.ThemeID == themeID);
        this.walll.sprite = theme.WallSprite;
        this.furniture.sprite = theme.FurnitureSprite;
    }
}
