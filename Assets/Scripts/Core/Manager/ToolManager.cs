using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolManager : Singleton<ToolManager>
{
    public CleanerManager cleaner;
    public GlasserManager glasser;
    public BreakerManager breaker;

    private void Start()
    {
        Init();
    }

    public void Init()
    {
        UIManager.Instance.toolShop.SetData(glasser.tools);
    }

    public bool IsSelectingTool(ToolItem t)
    {
        if (t.data.ToolType == ToolData.Type.CLEANER)
            return t.data.KeyName == cleaner.GetToolItem().data.KeyName;
        else if (t.data.ToolType == ToolData.Type.GLASSER)
            return t.data.KeyName == glasser.GetToolItem().data.KeyName;
        else
            return t.data.KeyName == breaker.GetToolItem().data.KeyName;
    }

    public void SelectThisTool(ToolItem t)
    {
        if (t.data.ToolType == ToolData.Type.CLEANER)
        {
            cleaner.usingToolIndex = cleaner.GetToolIndex(t.data);
        }
        else if (t.data.ToolType == ToolData.Type.GLASSER)
        {
            glasser.usingToolIndex = glasser.GetToolIndex(t.data);
            //if (glasser.tools[glasser.usingToolIndex].data.KeyName.Equals("cleaner7"))
            //{
            //    this.glasser.GetTool().transform.GetChild(0).transform.localScale = new Vector2(1.4f, 1.4f);
            //}
        }
        else
        {
            breaker.usingToolIndex = breaker.GetToolIndex(t.data);
        }
    }

    #region SAVE/LOAD

    public void Save()
    {
        cleaner.Save();
        glasser.Save();
        breaker.Save();

        PlayerPrefs.SetInt("using_cleaner", cleaner.usingToolIndex);
        PlayerPrefs.SetInt("using_glasser", glasser.usingToolIndex);
        PlayerPrefs.SetInt("using_breaker", breaker.usingToolIndex);
    }

    public void Load()
    {
        cleaner.Load();
        glasser.Load();
        breaker.Load();

        if (PlayerPrefs.HasKey("using_cleaner"))
            cleaner.usingToolIndex = PlayerPrefs.GetInt("using_cleaner");
        if (PlayerPrefs.HasKey("using_glasser"))
            glasser.usingToolIndex = PlayerPrefs.GetInt("using_glasser");
        if (PlayerPrefs.HasKey("using_breaker"))
            breaker.usingToolIndex = PlayerPrefs.GetInt("using_breaker");
    }

    #endregion
}
