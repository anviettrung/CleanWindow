using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Theme", menuName = "Theme", order = 1)]
public class Theme : ScriptableObject
{
    [SerializeField] int themeID;
    [SerializeField] string themeName;
    [SerializeField] Sprite wallSprite;
    [SerializeField] Sprite avatarPlayer;
    [SerializeField] public GameObject windowPrefab;

    public int ThemeID { get { return this.themeID; } }
    public string ThemeName { get { return this.themeName; } }
    public Sprite WallSprite { get { return this.wallSprite; } }
    public Sprite AvatarPlayer { get { return this.avatarPlayer; } }
    public GameObject WindowPrefab { get { return this.windowPrefab; } }
}
