using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Tool : MonoBehaviour
{
	#region DATA
	[Header("Data")]
	[SerializeField] protected ToolData data;

	[Header("Tracking")]
	[HideInInspector] public bool readyToUse = false;
	public ToolData Data {
		get {
			return data;
		}
		set {
			data = value;
			Init();
		}
	}

	//	[Header("General setting")]


	[Header("Sprite Renderers")]
	public SpriteRenderer srMain;

	[Header("Glass Effect")]
	public GlasserEffect glasserEffect;

	[Header("Shaking Effect")]
	public ShakeTransformS shakeTransform;
	public ParticleSystem shakeEffect;

	#endregion

	#region EVENT
	//[Header("Events")]
	//public UnityEvent onComplete = new UnityEvent();

	#endregion

	#region UNITY_CALLBACK
	private void Start()
	{
		Init();
	}

	#endregion

	#region INITIALIZATION
	protected void Init()
	{
		if (data == null) return;

		srMain.sprite = data.Art;
	}

	#endregion

	#region FUNCTION
	public void Move(Vector3 deltaPosition)
	{
		transform.position += deltaPosition;
	}

	#endregion

	#region GET/SET
	public Vector3 GetTargetPosition()
	{
		return Camera.main.WorldToScreenPoint(transform.position);
	}

	public Vector3 ClampPosition(Vector3 originalPosition)
	{
		Vector3 worldPosition = Camera.main.WorldToScreenPoint(originalPosition);

		worldPosition.x = Mathf.Clamp(worldPosition.x, 0, Screen.width);
		worldPosition.y = Mathf.Clamp(worldPosition.y, 0, Screen.height / 2);
		worldPosition = Camera.main.ScreenToWorldPoint(worldPosition);

		worldPosition.z = originalPosition.z;

		return worldPosition;
	}

	#endregion
}
