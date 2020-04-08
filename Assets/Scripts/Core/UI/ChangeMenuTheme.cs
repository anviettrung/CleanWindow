using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System.Linq;
using UnityEngine.UI;

public class ChangeMenuTheme : Singleton<ChangeMenuTheme>
{
    [Header("Theme Data")]
    public ListThemeData themeDatas;

    [Header("Home Background")]
    //public SpriteRenderer wall;
    public Image wall;

    [Header("List Window")]
    public List<Window> windows = new List<Window>();

    [Header("Transform Button")]
    public Transform changeThemeTransform;

    private void OnEnable()
    {
        windows.Clear();
        windows = FindObjectsOfType<Window>().ToList();
        for (int i = 0; i < themeDatas.Theme.Length; i++)
        {
            var window_clone = Instantiate(themeDatas.Theme[i].windowPrefab);
            //window_clone.GetComponent<Window>().enabled = false;
            window_clone.SetActive(false);
            if (windows.Find(w => w.name == window_clone.name) == null)
            {
                windows.Add(window_clone.GetComponent<Window>());
            }
            else
            {
                Destroy(window_clone.gameObject);
            }
        }
        this.EnableDefaultTheme();
    }

    private void EnableDefaultTheme()
    {
        //Default theme = Blue theme
        var default_theme = Array.Find(themeDatas.Theme, theme => theme.ThemeID == 5);
        for (int i = 0; i < windows.Count; i++)
        {
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

    public void OnClickChangeTheme()
    {
        var selected_object = EventSystem.current.currentSelectedGameObject;
        var themeID = selected_object.transform.GetSiblingIndex() + 1;
        var theme = Array.Find(this.themeDatas.Theme, t => t.ThemeID == themeID);
        var window_clone = windows.ElementAt(themeID - 1);

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
    }
}
