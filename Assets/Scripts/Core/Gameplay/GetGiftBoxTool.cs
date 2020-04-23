using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class GetGiftBoxTool : MonoBehaviour
{
    public Image toolCustomize;

    public void OnClickButtonGiftBox()
    {
        var lock_tool = ToolManager.Instance.breaker.tools.FirstOrDefault(tool => tool.status == ToolItem.Status.LOCK);
        lock_tool.status = ToolItem.Status.UNLOCK;
        var tool_index = ToolManager.Instance.breaker.tools.IndexOf(lock_tool);
        this.toolCustomize.sprite = ToolManager.Instance.breaker.toolsData.Tools[tool_index].Art;
        this.toolCustomize.SetNativeSize();

        ToolManager.Instance.Save();
    }
}
