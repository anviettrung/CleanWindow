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
    public SpriteRenderer wall;

    public void OnClickChangeTheme()
    {
        var selected_object = EventSystem.current.currentSelectedGameObject;
        var themeID = selected_object.transform.GetSiblingIndex() + 1;
        var theme = Array.Find(this.themeDatas.Theme, t => t.ThemeID == themeID);
        this.wall.sprite = theme.WallSprite;

        LevelManager.Instance.currentWindow = theme.WindowPrefab.GetComponent<Window>();

        for (int i = 0; i < themeDatas.Theme.Length; i++)
        {
            if (theme.WindowPrefab != themeDatas.Theme[i].WindowPrefab)
            {
                theme.WindowPrefab.gameObject.SetActive(false);
            }
        }
    }
}
