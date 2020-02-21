using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CWPanelEvent : UnityEvent<CWPanel> { }

public class CWPanel : MonoBehaviour
{
	public CWPanelEvent onEnablePanel = new CWPanelEvent();
	public CWPanelEvent onDisablePanel = new CWPanelEvent();

	public void OnEnable()
	{
		onEnablePanel.Invoke(this);
	}

	public void OnDisable()
	{
		onDisablePanel.Invoke(this);
	}
}
