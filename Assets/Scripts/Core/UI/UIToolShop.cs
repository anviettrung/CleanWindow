using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIToolShop : MonoBehaviour
{
	#region DATA
	public List<UIToolShopItem> items;
	protected List<ToolItem> allData;
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
	public virtual void SetData(List<ToolItem> datas)
	{
		allData = datas;
		UpdateItemData();
	}

	public virtual void UpdateItemData(int from, int to)
	{
		ResetItemData();
		for (int i = 0, j = from; i < items.Count && j <= to; i++, j++) {
			items[i].toolData = allData[j];
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
			items[i].toolData = null;
	}

	public virtual void UpdateUI()
	{
		//this.UnlockAllItemForTesting();
		foreach (UIToolShopItem item in items) {
			item.UpdateUI();
		}
	}

	#endregion

	#region Testing
	private void UnlockAllItemForTesting()
	{
		foreach (UIToolShopItem item in items)
		{
			item.toolData.status = ToolItem.Status.UNLOCK;
		}
	}
	#endregion
}
