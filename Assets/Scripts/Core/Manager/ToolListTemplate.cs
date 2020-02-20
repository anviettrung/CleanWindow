using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolListTemplate : MonoBehaviour
{
	#region DATA
	[Header("Resources")]
	public ToolListData toolsData;
	[Header("Working data generated from resources")]
	public List<ToolItem> tools;

	[Header("Prefab")]
	public GameObject toolPrefab;

	// tracking
	protected Tool currentTool;
	protected int usingToolIndex;

	#endregion

	#region UNITY_CALLBACK
	public void Awake()
	{
		Init();
	}
	#endregion

	#region INITIALIZATION
	public void Init()
	{
		tools = new List<ToolItem>();
		for (int i = 0; i < toolsData.Tools.Length; i++) {
			ToolItem t = new ToolItem(toolsData.Tools[i]);
			tools.Add(t);
		}

		//UIManager.Instance.windowShop.SetData(tools);
	}
	#endregion

	#region FUNCTION

	public void CreateTool(int x)
	{
		ToolData data = tools[x].data;
		usingToolIndex = x;

		if (currentTool != null)
			Destroy(currentTool.gameObject);

		Tool t = Instantiate(toolPrefab).GetComponent<Tool>();

		t.Data = data; // Tool will automatic re-init

		currentTool = t; // track
	}

	public void CreateTool()
	{
		CreateTool(usingToolIndex);
	}

	public Tool GetTool()
	{
		return currentTool;
	}

	public void UnlockLevel(string keyName)
	{

	}

	#endregion

	#region GET/SET
	public int GetToolIndex(ToolData data)
	{
		for (int i = 0; i < tools.Count; i++) {
			if (tools[i].data.KeyName == data.KeyName)
				return i;
		}

		return -1; // error 404
	}


	#endregion

	#region SAVE/LOAD

	public void Save()
	{
		Debug.Log("Save tool");
		for (int i = 0; i < tools.Count; i++) {

			PlayerPrefs.SetInt("tool_" + tools[i].data.KeyName, (int)tools[i].status);

		}
	}

	public void Load()
	{
		Debug.Log("Load Tool");
		for (int i = 0; i < tools.Count; i++) {

			string key = "tool_" + tools[i].data.KeyName;
			if (PlayerPrefs.HasKey(key)) {

				int s = PlayerPrefs.GetInt(key);
				tools[i].status = (ToolItem.Status)s;

			}

		}
	}
	#endregion
}

[System.Serializable]
public class ToolItem
{
	public ToolData data;
	public Status status;
	public enum Status
	{
		LOCK,
		UNLOCK
	}

	public ToolItem(ToolData d)
	{
		data = d;
	}
}
