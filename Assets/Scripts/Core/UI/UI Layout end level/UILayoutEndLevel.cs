using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UILayoutEndLevel : MonoBehaviour
{
    public Text numberOfLikes;
    public Text hashTag;
    public ParticleSystem confettiEffect;

    private void OnEnable()
    {
        this.StartCoroutine(IEAutoWriteHashTag());
        this.StartCoroutine(IEUpdateLikes(1f));
    }

    private IEnumerator IEUpdateLikes(float time)
    {
        this.confettiEffect.Play();
        var bonus = ConfigManager.Instance.bonusConfig.BonusPerLevel;
        var level = LevelManager.Instance.currentLevel.Value;
        float count = 0f;
        while (count < time)
        {
            count += Time.deltaTime;
            int likes = (int)Mathf.Lerp(0, bonus * level, count / time);
            this.numberOfLikes.text = likes.ToString();
            yield return null;
        }
        GameManager.Instance.totalMoney += bonus * level;
        UIManager.Instance.textMoneyNumber.text = ConvertNumber.Instance.ConvertLargeNumber(GameManager.Instance.totalMoney);
        GameManager.Instance.SaveTotalMoney();
    }

    private IEnumerator IEAutoWriteHashTag()
    {
        this.hashTag.text = "";
        var hashtag = this.GetCityHashTag();
        for (int i = 0; i < hashtag.ToCharArray().Length; i++)
        {
            this.hashTag.text += hashtag.ToCharArray()[i];
            yield return new WaitForSeconds(0.05f);
        }
    }

    private string GetCityHashTag()
    {
        var array_hashtags = ConfigManager.Instance.arrayHashTagConfig.CityHashTag;
        var current_level = LevelManager.Instance.currentLevel.Value;
        for (int i = 0; i < array_hashtags.Length; i++)
        {
            if (array_hashtags[i].CityName == LevelManager.Instance.levels[current_level].data.WindowName)
            {
                return array_hashtags[i].HashTag;
            }
        }
        return null;
    }
}
