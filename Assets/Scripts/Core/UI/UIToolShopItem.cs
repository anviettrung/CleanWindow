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

	//[Header("Lock button elements")]

	#endregion

	#region EVENT

	#endregion

	#region UNITY_CALLBACK
	private void Start()
	{
		this.unlockButton.onClick.AddListener(() => VibrationManager.Instance.OnClickButton());
		this.lockButton.onClick.AddListener(() => VibrationManager.Instance.OnClickButton());
	}
	#endregion

	#region FUNCTION
	public void SelectItem()
	{
		ToolManager.Instance.SelectThisTool(toolData);
	}

	public void UnlockItem()
	{
		toolData.status = ToolItem.Status.UNLOCK;
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

		switch (toolData.status) {
			case ToolItem.Status.UNLOCK:
				if (ToolManager.Instance.IsSelectingTool(toolData))
					selectingVisual.SetActive(true);
				unlockButton.gameObject.SetActive(true);
				toolImage.sprite = toolData.data.Art;
				break;

			case ToolItem.Status.LOCK:
				lockButton.gameObject.SetActive(true);

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
