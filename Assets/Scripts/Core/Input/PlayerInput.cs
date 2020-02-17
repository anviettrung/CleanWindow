using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
	#region DATA
	public Window window;
	public bool lockInput = true;

	#endregion

	#region UNITY_CALLBACK
	void Update()
    {
		if (lockInput == false) {
			if (window != null && Input.GetMouseButton(0)) {
				if (window.GetCurrentTextureDrawer() != null)
					window.GetCurrentTextureDrawer().DrawAt(Input.mousePosition);
			}
		}
	}

	#endregion

	#region FUNCTION
	public void LockInput()
	{
		lockInput = true;
	}

	public void UnlockInput()
	{
		lockInput = false;
	}
	#endregion
}
