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

    private bool isFullProgressBar = true;
    #endregion

    #region UNITY_CALLBACK
    private void OnEnable()
    {
        this.UpdateProgressGiftBox();
    }

    private void OnDisable()
    {
        this.isFullProgressBar = true;
        StopAllCoroutines();
    }

    private void Update()
    {
        if (this.uIProgressbar.ValueProgress == 1 && this.isFullProgressBar == true)
        {
            this.OnFullProgressGiftBox();
            this.isFullProgressBar = false;
        }
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

        if (this.CheckUnlockAllToolBreaker() == true)
        {
            this.StartCoroutine(IEUpdateProgressGiftBox(2f));
        }

        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            UIManager.Instance.nextButtonForTest.gameObject.SetActive(true);
        }, 1f));

        //StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        //{
        //    UIManager.Instance.watchAdsButton.gameObject.SetActive(true);
        //}, 1f));
        //StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        //{
        //    UIManager.Instance.nextButton.gameObject.SetActive(true);
        //}, 1.5f));
    }

    public void ResetProgressGiftBox()
    {
        StopCoroutine("IEUpdateProgressGiftBox");
        this.uIProgressbar.ValueProgress = 0f;
        this.textPercent.text = "0%";

        //Show interstitial ad
        AdmobManager.Instance.ShowInterstitialAd();
    }

    private IEnumerator IEUpdateProgressGiftBox(float time)
    {
        var max_game_number = GameCounter.Instance.maxGameToGetGift;
        var game_played = GameCounter.Instance.TotalGamePlayed;
        this.textPercent.text = (int)((game_played / max_game_number) * 100f) + "%";
        if (this.uIProgressbar.ValueProgress < 1f)
        {
            float count = 0f;
            while (count < time)
            {
                count += Time.deltaTime;
                this.uIProgressbar.ValueProgress = Mathf.Lerp(this.uIProgressbar.ValueProgress, game_played / max_game_number, count / time);
                yield return null;
            }
            //this.OnFullProgressGiftBox();
        }
    }

    private IEnumerator IEResetProgressGiftBox(float time)
    {
        float count = 0f;
        while (count < time)
        {
            count += Time.deltaTime;
            this.uIProgressbar.ValueProgress = Mathf.Lerp(1f, 0f, count / time);
            yield return null;
        }
        this.textPercent.text = "0%";
    }

    private bool CheckUnlockAllToolBreaker()
    {
        var check_unlock_all = ToolManager.Instance.breaker.tools.FindAll(tool => tool.status == ToolItem.Status.LOCK).Count > 0;
        return check_unlock_all;
    }
    #endregion
}
