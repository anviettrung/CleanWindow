using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIUnlockRandomItem : MonoBehaviour
{
    public UIToolShop uIToolShop;
    public Button unlockRandomItem;

    private List<UIToolShopItem> uIToolShopItems = new List<UIToolShopItem>();
    private UIToolShopItem lastItemRandom;

    private int prevNumber;
    private int priceOfAnItem;

    private void Awake()
    {
        this.Init();
    }

    private void OnEnable()
    {
        //this.uIToolShopItems.AddRange(this.uIToolShop.items.FindAll(item => item.toolData.status == ToolItem.Status.LOCK));
        this.GetItem();
        if (GameManager.Instance.totalMoney >= this.priceOfAnItem)
        {
            this.unlockRandomItem.interactable = true;
        }
        else
        {
            this.unlockRandomItem.interactable = false;
        }

    }

    private void OnDisable()
    {
        this.uIToolShopItems.Clear();
        this.StopAllCoroutines();
    }

    private void Init()
    {
        this.prevNumber = -1;
        this.priceOfAnItem = ConfigManager.Instance.bonusConfig.PriceToUnlockItem;
    }

    public void StartUnlockRandom()
    {
        StartCoroutine(IEStartSelectRandomItem(2f));
    }

    private IEnumerator IEStartSelectRandomItem(float time)
    {
        for (float count = 0; count < time; count += Time.deltaTime)
        {
            int random_index = GetRandomNumber(this.uIToolShopItems.Count);
            this.uIToolShopItems[random_index].selectingVisual.SetActive(true);
            foreach (var item in this.uIToolShopItems)
            {
                if (item != this.uIToolShopItems[random_index])
                {
                    item.selectingVisual.SetActive(false);
                }
            }
            this.lastItemRandom = this.uIToolShopItems[random_index];
            yield return new WaitForSeconds(0.1f);
            continue;
        }

        this.lastItemRandom.toolData.status = ToolItem.Status.UNLOCK;
        this.lastItemRandom.UpdateUI();
    }

    private int GetRandomNumber(int max_range)
    {
        int random_number = Random.Range(0, max_range);
        while (random_number == prevNumber)
        {
            random_number = Random.Range(0, max_range);
        }
        prevNumber = random_number;
        return random_number;
    }

    private void GetItem()
    {
        for (int i = 0; i < this.uIToolShop.items.Count; i++)
        {
            if (this.uIToolShop.items[i].gameObject.activeInHierarchy == true)
            {
                if (this.uIToolShop.items[i].toolData.status == ToolItem.Status.LOCK &&
                    this.uIToolShop.items[i].toolData.data.ToolType != ToolData.Type.BREAKER)
                {
                    this.uIToolShopItems.Add(this.uIToolShop.items[i]);
                }
            }
        }
    }
}
