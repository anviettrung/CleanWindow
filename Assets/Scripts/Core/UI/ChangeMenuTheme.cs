using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;

public class ChangeMenuTheme : Singleton<ChangeMenuTheme>
{
    [Header("Theme Data")]
    public ListThemeData themeDatas;

    [Header("Home Background")]
    public SpriteRenderer wall;

    [Header("List Window")]
    public List<Window> windows = new List<Window>();

    [Header("Transform Button")]
    public Transform changeThemeTransform;

    private void Awake()
    {
        if (PlayerPrefs.HasKey("lasted_theme"))
        {
            PlayerPrefs.DeleteKey("lasted_theme");
        }
    }

    public void CreateNewWindow()
    {
        windows.Clear();
        for (int i = 0; i < themeDatas.Theme.Length; i++)
        {
            var window_clone = Instantiate(themeDatas.Theme[i].windowPrefab);
            window_clone.SetActive(false);
            if (PlayerPrefs.HasKey("lastest_lvl"))
            {
                var level = PlayerPrefs.GetInt("lastest_lvl");
                window_clone.GetComponent<Window>().Data = LevelManager.Instance.levels[level].data;
            }
            if (windows.Find(w => w.name == window_clone.name) == null)
            {
                windows.Add(window_clone.GetComponent<Window>());
            }
            else
            {
                Destroy(window_clone.gameObject);
            }
        }
        this.EnableTheme();
    }

    private void EnableTheme()
    {
        if (!PlayerPrefs.HasKey("lasted_theme"))
        {
            //Enable default theme
            //Default theme = Blue theme
            var default_theme = Array.Find(themeDatas.Theme, theme => theme.ThemeID == 5);
            PlayerPrefs.SetInt("lasted_theme", default_theme.ThemeID);
            for (int i = 0; i < windows.Count; i++)
            {
                if (PlayerPrefs.HasKey("lastest_lvl"))
                {
                    var level = PlayerPrefs.GetInt("lastest_lvl");
                    windows[i].Data = LevelManager.Instance.levels[level].data;
                }
                if (windows[i].name.Contains(default_theme.WindowPrefab.name))
                {
                    LevelManager.Instance.currentWindow = windows[i];
                    this.wall.sprite = default_theme.WallSprite;
                    foreach (Transform child in changeThemeTransform)
                    {
                        if (child.name.Contains("Blue"))
                        {
                            child.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
        else
        {
            var lasted_theme_id = PlayerPrefs.GetInt("lasted_theme");
            var lasted_theme = Array.Find(themeDatas.Theme, theme => theme.ThemeID == lasted_theme_id);
            for (int i = 0; i < windows.Count; i++)
            {
                if (PlayerPrefs.HasKey("lastest_lvl"))
                {
                    var level = PlayerPrefs.GetInt("lastest_lvl");
                    windows[i].Data = LevelManager.Instance.levels[level].data;
                }
                if (windows[i].name.Contains(lasted_theme.WindowPrefab.name))
                {
                    LevelManager.Instance.currentWindow = windows[i];
                    this.wall.sprite = lasted_theme.WallSprite;
                    foreach (Transform child in changeThemeTransform)
                    {
                        if (child.name.Contains(lasted_theme.name))
                        {
                            child.transform.GetChild(0).gameObject.SetActive(true);
                        }
                    }
                    return;
                }
                else
                {
                    continue;
                }
            }
        }
        //windows.Clear();
    }

    public void OnClickChangeTheme()
    {
        var selected_object = EventSystem.current.currentSelectedGameObject;
        var themeID = selected_object.transform.GetSiblingIndex() + 1;
        var theme = Array.Find(this.themeDatas.Theme, t => t.ThemeID == themeID);
        var window_clone = windows.ElementAt(themeID - 1);

        window_clone.GetComponent<Window>().Data = LevelManager.Instance.currentWindow.Data;

        selected_object.transform.GetChild(0).gameObject.SetActive(true);
        foreach (Transform child in selected_object.transform.parent)
        {
            if (child.name != selected_object.name)
            {
                child.transform.GetChild(0).gameObject.SetActive(false);
            }
        }

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

        PlayerPrefs.SetInt("lasted_theme", themeID);
    }
}
