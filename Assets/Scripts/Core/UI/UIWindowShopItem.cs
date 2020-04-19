using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIWindowShopItem : MonoBehaviour
{
	#region DATA
	public Level levelData;

	[Header("All Button states")]
	public Button completeButton;
	public Button unlockButton;
	public Button lockButton;

	[Header("Complete button elements")]
	public Image mainPic;
	public Text labelWindowName;

	[Header("Unlock button elements")]
	public Image unlockPic;

	[Header("Lock button elements")]
	public Text levelText;

	#endregion

	#region EVENT

	#endregion

	#region FUNCTION
	public void PlayLevel()
	{
		if (levelData != null)
		{
			LevelManager.Instance.OpenLevel(levelData.data);
			LevelManager.Instance.PlayLevel();
		}

		//if (levelData != null)
		//	LevelManager.Instance.OpenLevel(levelData.data);
	}

	public void UnlockLevel()
	{
		LevelManager.Instance.UnlockLevel(levelData.data.KeyName);
	}

	#endregion

	#region SHOW/HIDE
	public virtual void UpdateUI()
	{
		if (levelData == null) {
			gameObject.SetActive(false);
			return;
		}

		gameObject.SetActive(true);

		HideAllButton();

		switch (levelData.status) {
			case Level.Status.COMPLETE:
				completeButton.gameObject.SetActive(true);
				mainPic.sprite = levelData.data.Picture;
				labelWindowName.text = levelData.data.WindowName;
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
