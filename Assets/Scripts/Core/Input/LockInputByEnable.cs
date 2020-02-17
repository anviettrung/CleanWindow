using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LockInputByEnable : MonoBehaviour
{
	public string lockName;

	private void OnEnable()
	{
		PlayerInput.Instance.LockInput(lockName);
	}

	private void OnDisable()
	{
		PlayerInput.Instance.UnlockInput(lockName);
	}
}
