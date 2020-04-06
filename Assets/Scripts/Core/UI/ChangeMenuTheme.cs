using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class ChangeMenuTheme : MonoBehaviour
{
    [Header("Theme Data")]
    public ListThemeData themeDatas;

    [Header("Home Background")]
    public SpriteRenderer wall;

    [Header("List Window")]
    public List<Window> windows = new List<Window>();

    private void Awake()
    {
        for (int i = 0; i < themeDatas.Theme.Length; i++)
        {
            var window_clone = Instantiate(themeDatas.Theme[i].windowPrefab);
            window_clone.GetComponent<Window>().enabled = false;
            window_clone.SetActive(false);
            if (windows.Contains(window_clone.GetComponent<Window>()) == false)
            {
                windows.Add(window_clone.GetComponent<Window>());
            }
            if (i == 0)
            {
                LevelManager.Instance.currentWindow = window_clone.GetComponent<Window>();
                this.wall.sprite = themeDatas.Theme[0].WallSprite;
            }
        }
    }

    public void OnClickChangeTheme()
    {
        var selected_object = EventSystem.current.currentSelectedGameObject;
        var themeID = selected_object.transform.GetSiblingIndex() + 1;
        var theme = Array.Find(this.themeDatas.Theme, t => t.ThemeID == themeID);
        var window_clone = windows.ElementAt(themeID - 1);
        this.wall.sprite = theme.WallSprite;

        window_clone.gameObject.SetActive(true);

        LevelManager.Instance.currentWindow = window_clone.GetComponent<Window>();

        foreach (var w in windows)
        {
            if (w != window_clone)
            {
                w.gameObject.SetActive(false);
            }
        }
    }
}
