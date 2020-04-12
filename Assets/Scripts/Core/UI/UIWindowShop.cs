using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIWindowShop : MonoBehaviour
{
	#region DATA
	public List<UIWindowShopItem> items;
	protected List<Level> allData;
	#endregion

	#region EVENT

	#endregion

	#region UNITY_CALLBACK
	protected void OnEnable()
	{
		UpdateUI();
	}

	protected void Update()
	{
		UpdateUI();
	}

	#endregion

	#region FUNCTION
	public virtual void SetData(List<Level> datas)
	{
		allData = datas;
		UpdateItemData();
	}

	public virtual void UpdateItemData(int from, int to)
	{
		ResetItemData();
		for (int i = 0, j = from; i < items.Count && j <= to; i++, j++) {
			items[i].levelData = allData[j];
		}
	}

	// all
	public void UpdateItemData()
	{
		UpdateItemData(0, allData.Count - 1);
	}

	protected void ResetItemData()
	{
		for (int i = 0; i < items.Count; i++)
			items[i].levelData = null;
	}

	public virtual void UpdateUI()
	{
		this.UnlockAllItemForTesting();
		foreach (UIWindowShopItem item in items) {
			item.UpdateUI();
		}
	}

	#endregion

	#region Testing
	private void UnlockAllItemForTesting()
	{
		foreach (UIWindowShopItem item in items)
		{
			item.levelData.status = Level.Status.COMPLETE;
		}
	}
	#endregion
}
