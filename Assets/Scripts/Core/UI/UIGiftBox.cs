using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIGiftBox : MonoBehaviour
{
    #region CONST
    private const string KEY_PROGRESS_GIFBOX = "Percent GiftBox";
    #endregion

    #region PUBLIC FIELDS
    public UIProgressbar uIProgressbar;
    public GameObject layoutGiftBox;
    public Text textPercent;
    #endregion

    #region UNITY_CALLBACK
    private void Awake()
    {
        this.LoadProgressGiftBox();
    }

    private void OnEnable()
    {
        this.UpdateProgressGiftBox();
    }
    #endregion

    #region UI CLICK BUTTON
    public void OnClickGiftBox()
    {
        if (GameCounter.Instance.TotalGamePlayed == GameCounter.Instance.maxGameToGetGift)
        {
            this.layoutGiftBox.SetActive(true);
        }
    }
    #endregion

    #region FUNCTION
    public void UpdateProgressGiftBox()
    {
        //count number of game played
        GameCounter.Instance.TotalGamePlayed++;
        GameCounter.Instance.SaveTotalGamePlayed(GameCounter.Instance.TotalGamePlayed);

        this.StartCoroutine(IEUpdateProgressGiftBox(2f));
        if (this.uIProgressbar.ValueProgress >= 1f)
        {
            this.uIProgressbar.ValueProgress = 0f;
        }
        this.SaveProgressGiftBox();
    }

    private IEnumerator IEUpdateProgressGiftBox(float time)
    {
        var max_game_number = GameCounter.Instance.maxGameToGetGift;
        var game_played = GameCounter.Instance.TotalGamePlayed;
        this.textPercent.text = (int)((game_played / max_game_number) * 100f) + "%";
        if (this.uIProgressbar.ValueProgress <= 1f)
        {
            float counter = 0f;
            while (counter < time)
            {
                counter += Time.deltaTime;
                this.uIProgressbar.ValueProgress = Mathf.Lerp(this.uIProgressbar.ValueProgress, game_played / max_game_number, counter / time);
                yield return null;
            }
        }
    }
    #endregion

    #region SAVE/LOAD PROGRESS
    private void LoadProgressGiftBox()
    {
        GameCounter.Instance.LoadTotalGamePlayed();
        if (PlayerPrefs.HasKey(KEY_PROGRESS_GIFBOX))
        {
            GameCounter.Instance.TotalGamePlayed = PlayerPrefs.GetInt(KEY_PROGRESS_GIFBOX);
            this.uIProgressbar.ValueProgress = GameCounter.Instance.TotalGamePlayed * ((float)1 / GameCounter.Instance.maxGameToGetGift);
        }
    }

    private void SaveProgressGiftBox()
    {
        PlayerPrefs.SetInt(KEY_PROGRESS_GIFBOX, (int)GameCounter.Instance.TotalGamePlayed / (int)GameCounter.Instance.maxGameToGetGift);
    }
    #endregion
}
