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
    private void OnEnable()
    {
        this.UpdateProgressGiftBox();
    }
    #endregion

    #region UI CLICK BUTTON
    public void OnFullProgressGiftBox()
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
        if (GameCounter.Instance.TotalGamePlayed > GameCounter.Instance.maxGameToGetGift)
        {
            GameCounter.Instance.TotalGamePlayed = 1;
            this.uIProgressbar.ValueProgress = 0f;
        }
        GameCounter.Instance.SaveTotalGamePlayed(GameCounter.Instance.TotalGamePlayed);

        this.StartCoroutine(IEUpdateProgressGiftBox(2f));
    }

    private IEnumerator IEUpdateProgressGiftBox(float time)
    {
        var max_game_number = GameCounter.Instance.maxGameToGetGift;
        var game_played = GameCounter.Instance.TotalGamePlayed;
        this.textPercent.text = (int)((game_played / max_game_number) * 100f) + "%";
        if (this.uIProgressbar.ValueProgress < 1f)
        {
            float counter = 0f;
            while (counter < time)
            {
                counter += Time.deltaTime;
                this.uIProgressbar.ValueProgress = Mathf.Lerp(this.uIProgressbar.ValueProgress, game_played / max_game_number, counter / time);
                yield return null;
            }
        }

        this.OnFullProgressGiftBox();
    }
    #endregion
}
