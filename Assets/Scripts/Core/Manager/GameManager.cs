using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameManager : Singleton<GameManager>
{
	#region DATA
	[Header("Resources")]
	[SerializeField] private GeneralResource generalResources = null;
	public GeneralResource GeneralResources { get { return generalResources; } }

	[Header("Tool Transform")]
	public ToolTransform toolTransform;

	[Header("Money")]
	public int totalMoney;
	private const string KEY_MONEY = "Total_Money";
	#endregion

	#region UNITY_CALLBACK
	protected void Awake()
	{
		this.LoadTotalMoney();
	}

	protected void Start()
	{
		// Default setting
		// Setting first time play
		LevelManager.Instance.levels[0].ChangeStatus(Level.Status.UNLOCK); // Unlock first level
		ToolManager.Instance.cleaner.tools[0].status = ToolItem.Status.UNLOCK; // Unlock first cleaner tool
		ToolManager.Instance.glasser.tools[0].status = ToolItem.Status.UNLOCK; // Unlock first glasser tool
		ToolManager.Instance.breaker.tools[0].status = ToolItem.Status.UNLOCK; // Unlock first breaker tool
																			   // Load game data
		if (!PlayerPrefs.HasKey("Old user")) {
			PlayerPrefs.SetInt("Old user", 1);
		} else {
			// Setting if not first time play
			LevelManager.Instance.Load();
			ToolManager.Instance.Load();
		}
		// Initialization

		// Game Intro

		// Open level
		LevelManager.Instance.OpenLastestLevel();
	}

	private void OnApplicationQuit()
	{
		UIManager.Instance.ShowAll(false);
		// Save game
		LevelManager.Instance.Save();
		ToolManager.Instance.Save();

		PlayerPrefs.Save();
	}
	#endregion

	#region FUNCTION
	public void LoadTotalMoney()
	{
		if (PlayerPrefs.HasKey(KEY_MONEY))
		{
			this.totalMoney = PlayerPrefs.GetInt(KEY_MONEY);
		}
	}

	public void SaveTotalMoney()
	{
		PlayerPrefs.SetInt(KEY_MONEY, this.totalMoney);
	}
	#endregion
}