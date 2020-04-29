using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIListAvatar : Singleton<UIListAvatar>
{
    public List<AvatarData> avatarDatas;

    public void SaveAvatar()
    {
        var level = LevelManager.Instance.currentLevel.Value;
        var slot_window = UIManager.Instance.windowShop.items[level - 1];
        slot_window.avatar.sprite = UIManager.Instance.avatarPlayer.sprite;
        slot_window.avatarID = this.avatarDatas.Find(ava => ava.Avatar == UIManager.Instance.avatarPlayer.sprite).ID;
        PlayerPrefs.SetInt(slot_window.levelText.text, slot_window.avatarID);
    }

    public Sprite GetAvatar(string key)
    {
        var ava_id = PlayerPrefs.GetInt(key);
        var ava_data = this.avatarDatas.Find(ava => ava.ID == ava_id);
        return ava_data.Avatar;
    }
}

[System.Serializable]
public class AvatarData
{
    public int ID;
    public Sprite Avatar;
}
