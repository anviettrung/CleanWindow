using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CWPanelManager : Singleton<CWPanelManager>
{
	public List<CWPanel> cwPanels;
	public GameObject panelCover;

	private void Awake()
	{
		foreach (CWPanel pan in cwPanels) {
			pan.onEnablePanel.AddListener(OnOpenPanel);
			pan.onDisablePanel.AddListener(OnClosePanel);
		}
	}

	public void OnOpenPanel(CWPanel panel)
	{
		HideAllPanelsExcept(panel);
		panelCover.SetActive(true);
	}

	public void OnClosePanel(CWPanel panel)
	{
		panelCover.SetActive(false);
	}

	protected void HideAllPanelsExcept(CWPanel panel)
	{
		foreach (CWPanel pan in cwPanels) {
			if (panel != pan)
				pan.gameObject.SetActive(false);
		}
	}
}
