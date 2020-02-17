using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShop<T> : MonoBehaviour where T: class
{
	#region DATA
	public List<UIShopItem<T>> items;
	public T[] allData;
	#endregion

	#region EVENT

	#endregion

	#region UNITY_CALLBACK
	protected void OnEnable()
	{
		UpdateUI();
	}

	#endregion

	#region FUNCTION
	public virtual void SetData(T[] datas)
	{
		allData = datas;
		UpdateItemData();
	}

	public virtual void UpdateItemData(int from, int to)
	{
		ResetItemData();
		for (int i = 0, j = from; i < items.Count || j == to; i++, j++) {
			items[i].data = allData[j];
		}
	}

	// all
	public void UpdateItemData()
	{
		UpdateItemData(0, allData.Length - 1);
	}

	protected void ResetItemData()
	{
		for (int i = 0; i < items.Count; i++)
			items[i].data = null;
	}

	public virtual void UpdateUI()
	{
		foreach (UIShopItem<T> item in items) {
			item.UpdateUI();
		}
	}

	#endregion
}
