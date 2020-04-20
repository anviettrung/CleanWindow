using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPickAvatar : MonoBehaviour
{
    private void Start()
    {
        var button_select = this.GetComponent<Button>();
        button_select.onClick.AddListener(() => this.PickThisAvatar());
    }

    private void PickThisAvatar()
    {
        var this_avatar = this.GetComponent<Image>();
        UIManager.Instance.avatarPlayer.sprite = this_avatar.sprite;
        PlayerPrefs.SetString("last_selected_avatar", this_avatar.sprite.name);
    }
}
