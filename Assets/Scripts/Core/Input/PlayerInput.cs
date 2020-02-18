using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
	#region DATA
	public Window window;
	public bool IsLockInput {
		get {
			return lockChain.Count > 0;
		}
	}
	protected HashSet<string> lockChain;

	#endregion

	#region UNITY_CALLBACK
	private void Awake()
	{
		lockChain = new HashSet<string>();
	}

	void Update()
    {
		if (IsLockInput == false) {
			if (window != null && Input.GetMouseButton(0)) {
				if (window.GetCurrentTextureDrawer() != null)
					window.GetCurrentTextureDrawer().DrawAt(Input.mousePosition);
			}
		}
	}

	#endregion

	#region FUNCTION
	public void LockInput(string key)
	{
		if (lockChain.Contains(key) == false)
			lockChain.Add(key);
	}

	public void UnlockInput(string key)
	{
		lockChain.Remove(key);
	}
	#endregion
}
