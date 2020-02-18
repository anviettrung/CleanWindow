using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIShopItem<T>: MonoBehaviour where T: class
{
	#region DATA
	public T data;
	#endregion

	#region EVENT

	#endregion

	#region FUNCTION
	public virtual void UpdateUI()
	{
		if (data == null) {
			gameObject.SetActive(false);
			return;
		}

		gameObject.SetActive(true);
	}
	#endregion
}
