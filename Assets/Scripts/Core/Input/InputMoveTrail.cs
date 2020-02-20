using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputMoveTrail : Singleton<InputMoveTrail>
{
	public float sensitive = 1;

	protected Vector3 lastPosition;
	protected Vector3 deltaPosition;

	// For ios
	[Header("iOS setting")]
	protected bool isTouching;
	protected int curFingerId;
	public float speedModifier = 0.007f;
	protected Vector2 ratio;
	[SerializeField] protected float defaultWidthScreen = 1080; // iPhone X's screen
	[SerializeField] protected float defaultHeightScreen = 1920; // iPhone X's screen


	private void Awake()
	{
#if UNITY_IOS || UNITY_ANDROID
		ratio = new Vector2(
			defaultWidthScreen / Screen.width,
			defaultHeightScreen / Screen.height
		);
#endif
	}

	private void Update()
	{
#if UNITY_IOS || UNITY_ANDROID
#if UNITY_EDITOR
		if (UnityEditor.EditorApplication.isRemoteConnected) {
#endif
			if (!(Input.touchCount > 0)) {
				isTouching = false;
				return;
			}

			Touch touch = Input.GetTouch(0);

			if (isTouching == false) {

				curFingerId = touch.fingerId;

				deltaPosition = Vector2.zero;

				isTouching = true;

			}


			if (touch.fingerId == curFingerId) {

				deltaPosition = touch.deltaPosition;

			} else {

				isTouching = false;
			}

#if UNITY_EDITOR
		}
#endif
#endif

#if UNITY_EDITOR
		if (!UnityEditor.EditorApplication.isRemoteConnected) {
			deltaPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition) - lastPosition;
			lastPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}
#endif
	}

	public Vector3 GetDeltaPosition(bool usingSensitive)
	{
		float sens = usingSensitive ? sensitive : 1;

#if UNITY_EDITOR
		if (!UnityEditor.EditorApplication.isRemoteConnected)
			return deltaPosition * sens;
#endif

#if UNITY_IOS || UNITY_ANDROID
		if (isTouching) {
			Vector2 newDelta = new Vector2(
				deltaPosition.x * ratio.x * speedModifier * sens,
				deltaPosition.y * ratio.y * speedModifier * sens);
			return newDelta;
		} else {
			return Vector3.zero;
		}
#endif
	}
				
}
