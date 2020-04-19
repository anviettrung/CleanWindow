﻿using System.Collections;
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
	//protected Tool currentTool;
	[HideInInspector] public Tool currentTool;
	public int usingToolIndex;
	private Tool createdTool;

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
		Tool tool = Instantiate(toolPrefab).GetComponent<Tool>();
		if (tool.Data.ToolType != ToolData.Type.BREAKER)
		{
			tool.transform.position = GameManager.Instance.toolTransform.spawnTransform.position;
		}
		else if (tool.Data.ToolType == ToolData.Type.BREAKER)
		{
			tool.transform.position = GameManager.Instance.toolTransform.spawnBreakerTransform.position;
		}
		createdTool = tool;
	}
	#endregion

	#region FUNCTION

	public void CreateTool(int x)
	{
		ToolData data = tools[x].data;
		usingToolIndex = x;

		//if (currentTool != null)
		//	Destroy(currentTool.gameObject);

		//Tool t = Instantiate(toolPrefab).GetComponent<Tool>();
		//if (t.Data.ToolType != ToolData.Type.BREAKER)
		//{
		//	t.transform.position = GameManager.Instance.toolTransform.spawnTransform.position;
		//}
		//else if (t.Data.ToolType == ToolData.Type.BREAKER)
		//{
		//	t.transform.position = GameManager.Instance.toolTransform.spawnBreakerTransform.position;
		//}

		createdTool.Data = data;
		currentTool = createdTool;

		if (currentTool.Data.ToolType != ToolData.Type.BREAKER)
		{
			this.MoveTool(GameManager.Instance.toolTransform.startTransform.position, 2f);
		}
		else
		{
			this.MoveTool(GameManager.Instance.toolTransform.startBreakerTransform.position, 2f);
		}
		PlayerInput.Instance.tool = currentTool;
		//t.Data = data; // Tool will automatic re-init

		//currentTool = t; // track
	}

	public void CreateTool()
	{
		CreateTool(usingToolIndex);
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

	public Tool GetTool()
	{
		return currentTool;
	}

	public ToolItem GetToolItem()
	{
		return tools[usingToolIndex];
	}


	#endregion

	#region SAVE/LOAD

	public void Save()
	{
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

	#region MOVE_TOOL
	public void MoveTool(Vector3 endPos, float time)
	{
		StartCoroutine(IEMoveTool(endPos, time));
	}

	private IEnumerator IEMoveTool(Vector3 endPos, float time)
	{
		float count = 0f;
		while (count < time)
		{
			count += Time.deltaTime;
			this.currentTool.transform.position = Vector3.Lerp(this.currentTool.transform.position, endPos, count/time);
			yield return null;
		}
	}

	public void StopCoroutineMoveTool()
	{
		StopAllCoroutines();
	}

	//public void ResetPosition()
	//{
	//	if (this.currentTool.Data.ToolType != ToolData.Type.BREAKER)
	//	{
	//		this.currentTool.transform.position = GameManager.Instance.toolTransform.spawnTransform.position;
	//	}
	//	else
	//	{
	//		this.currentTool.transform.position = GameManager.Instance.toolTransform.spawnBreakerTransform.position;
	//	}
	//}
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
