using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowShopItem : MonoBehaviour
{
	#region DATA
	public Level data;

	[Header("All Button states")]
	public Button completeButton;
	public Button unlockButton;
	public Button lockButton;

	[Header("Complete button elements")]
	public Image mainPic;
	public Text labelWindowName;

	[Header("Unlock button elements")]
	public Image unlockPic;

	//[Header("Lock button elements")]

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

		HideAllButton();

		switch (data.status) {
			case Level.Status.COMPLETE:
				completeButton.gameObject.SetActive(true);
				mainPic.sprite = data.data.Picture;
				labelWindowName.text = data.data.WindowName;
				break;
			case Level.Status.UNLOCK:
				unlockButton.gameObject.SetActive(true);
				// get from data unlock pic
				break;
			case Level.Status.LOCK:
				lockButton.gameObject.SetActive(true);

				break;
		}
	}

	public void HideAllButton()
	{
		completeButton.gameObject.SetActive(false);
		unlockButton.gameObject.SetActive(false);
		lockButton.gameObject.SetActive(false);
	}

	#endregion
}
