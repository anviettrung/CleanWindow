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

    private void Start()
    {
        this.Init();
    }

    private void Update()
    {
        if (GameManager.Instance.totalMoney >= this.priceOfAnItem)
        {
            this.unlockRandomItem.interactable = true;
        }
        else
        {
            this.unlockRandomItem.interactable = false;
        }
    }

    private void OnEnable()
    {
        this.GetItem();
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
        this.GetItem();

        if (this.uIToolShopItems.Count == 1)
        {
            this.SubtractMoney();
            this.unlockRandomItem.enabled = false;
            this.DeSelectItems();
            this.lastItemRandom = this.uIToolShopItems[0];
            ToolManager.Instance.SelectThisTool(this.lastItemRandom.toolData);
            this.lastItemRandom.toolData.status = ToolItem.Status.UNLOCK;
            this.lastItemRandom.UpdateUI();
            this.lastItemRandom.selectingVisual.SetActive(true);
            ToolManager.Instance.Save();
            if (GameManager.Instance.totalMoney >= this.priceOfAnItem)
            {
                this.unlockRandomItem.interactable = true;
            }
            else
            {
                this.unlockRandomItem.interactable = false;
            }
        }
        else if (this.uIToolShopItems.Count > 1)
        {
            this.SubtractMoney();
            this.unlockRandomItem.enabled = false;
            this.DeSelectItems();
            StartCoroutine(IEStartSelectRandomItem(1.2f));
            if (GameManager.Instance.totalMoney >= this.priceOfAnItem)
            {
                this.unlockRandomItem.interactable = true;
            }
            else
            {
                this.unlockRandomItem.interactable = false;
            }
        }
        else
        {
            //do nothing
            return;
        }
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

        ToolManager.Instance.SelectThisTool(this.lastItemRandom.toolData);
        this.lastItemRandom.toolData.status = ToolItem.Status.UNLOCK;
        this.lastItemRandom.UpdateUI();
        this.lastItemRandom.selectingVisual.SetActive(true);
        ToolManager.Instance.Save();
        this.unlockRandomItem.enabled = true;
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

    /// <summary>
    /// DeSelected items not in use
    /// </summary>
    private void DeSelectItems()
    {
        if (this.uIToolShop != null || this.uIToolShop.items.Count > 0)
        {
            foreach (var item in this.uIToolShop.items)
            {
                if (item.gameObject.activeInHierarchy == true)
                {
                    item.selectingVisual.SetActive(false);
                }
            }
        }
    }

    /// <summary>
    /// Subtract money when unlock an item
    /// </summary>
    private void SubtractMoney()
    {
        if (GameManager.Instance.totalMoney >= ConfigManager.Instance.bonusConfig.PriceToUnlockItem)
        {
            GameManager.Instance.totalMoney -= ConfigManager.Instance.bonusConfig.PriceToUnlockItem;
            GameManager.Instance.SaveTotalMoney();
            UIManager.Instance.textMoneyNumber.text = ConvertNumber.Instance.ConvertLargeNumber(GameManager.Instance.totalMoney);
        }
    }

    private void GetItem()
    {
        if (this.uIToolShopItems != null || this.uIToolShopItems.Count > 0)
        {
            this.uIToolShopItems.Clear();
        }

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
