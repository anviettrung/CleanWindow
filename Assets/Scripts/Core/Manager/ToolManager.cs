using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : Singleton<ToolManager>
{
	public CleanerManager cleaner;
	public GlasserManager glasser;

	private void Start()
	{
		Init();
	}

	public void Init()
	{
		UIManager.Instance.toolShop.SetData(cleaner.tools);
	}

	public bool IsSelectingTool(ToolItem t)
	{
		if (t.data.ToolType == ToolData.Type.CLEANER)
			return t.data.KeyName == cleaner.GetToolItem().data.KeyName;
		else
			return t.data.KeyName == glasser.GetToolItem().data.KeyName;
	}

	public void SelectThisTool(ToolItem t)
	{
		if (t.data.ToolType == ToolData.Type.CLEANER)
			cleaner.usingToolIndex = cleaner.GetToolIndex(t.data);
		else
			glasser.usingToolIndex = glasser.GetToolIndex(t.data);
	}

	#region SAVE/LOAD

	public void Save()
	{
		cleaner.Save();
		glasser.Save();

		PlayerPrefs.SetInt("using_cleaner", cleaner.usingToolIndex);
		PlayerPrefs.SetInt("using_glasser", glasser.usingToolIndex);
	}

	public void Load()
	{
		cleaner.Load();
		glasser.Load();

		if (PlayerPrefs.HasKey("using_cleaner"))
			cleaner.usingToolIndex = PlayerPrefs.GetInt("using_cleaner");
		if (PlayerPrefs.HasKey("using_glasser"))
			glasser.usingToolIndex = PlayerPrefs.GetInt("using_glasser");
	}
			
	#endregion
}
