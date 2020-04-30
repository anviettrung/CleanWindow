using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;


public class UIToolShopItem : MonoBehaviour
{
	#region DATA
	public ToolItem toolData;

	[Header("All Button states")]
	public GameObject selectingVisual;
	public Button unlockButton;
	public Button lockButton;

	[Header("Unlock button elements")]
	public Image toolImage;

	[Header("Breaker tool")]
	public Sprite iconLockBreaker;
	public Image imgButtonLock;

	//[Header("Lock button elements")]

	#endregion

	#region EVENT

	#endregion

	#region UNITY_CALLBACK
	private void Start()
	{
		this.unlockButton.onClick.AddListener(() => VibrationManager.Instance.OnClickButton());
		this.lockButton.onClick.AddListener(() => VibrationManager.Instance.OnClickButton());
		this.imgButtonLock = this.lockButton.transform.GetChild(0).GetComponent<Image>();
		if (this.toolData.data.ToolType == ToolData.Type.BREAKER)
		{
			this.imgButtonLock.sprite = this.iconLockBreaker;
			this.imgButtonLock.SetNativeSize();
		}
	}
	#endregion

	#region FUNCTION
	public void SelectItem()
	{
		ToolManager.Instance.SelectThisTool(toolData);
	}

	public void UnlockItem()
	{
		//toolData.status = ToolItem.Status.UNLOCK;
	}

	#endregion

	#region SHOW/HIDE
	public virtual void UpdateUI()
	{
		if (toolData == null) {
			gameObject.SetActive(false);
			return;
		}

		gameObject.SetActive(true);

		HideAllButton();

		//for testing
		//if (toolData.data.ToolType != ToolData.Type.BREAKER)
		//{
		//	toolData.status = ToolItem.Status.UNLOCK;
		//}

		switch (toolData.status) {
			case ToolItem.Status.UNLOCK:
				if (ToolManager.Instance.IsSelectingTool(toolData))
					selectingVisual.SetActive(true);
				unlockButton.gameObject.SetActive(true);
				toolImage.sprite = toolData.data.Art;
				break;

			case ToolItem.Status.LOCK:
				lockButton.gameObject.SetActive(true);
				if (this.toolData.data.ToolType == ToolData.Type.BREAKER)
				{
					this.imgButtonLock.sprite = this.iconLockBreaker;
					this.imgButtonLock.SetNativeSize();
				}
				break;
		}
	}

	public void HideAllButton()
	{
		selectingVisual.gameObject.SetActive(false);
		unlockButton.gameObject.SetActive(false);
		lockButton.gameObject.SetActive(false);
	}

	#endregion
}
