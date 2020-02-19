using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Window : MonoBehaviour
{
	#region DATA
	[Header("Data")]
	[SerializeField] protected WindowData data;
	public WindowData Data {
		get {
			return data;
		}
		set {
			data = value;
			Init();
		}
	}

	[Header("General setting")]
	public float targetProgress = 0.95f;

	[Header("Sprite Renderers")]
	public SpriteRenderer srMainPicture;
	public SpriteRenderer srWet;
	public SpriteRenderer srDirty;
	public SpriteRenderer srWindow;
	public SpriteRenderer srBackground;

	[Header("Texture Drawer")]
	public TextureDrawer tdDirty;
	public TextureDrawer tdWet;
	public Window.State state;
	public enum State
	{
		DIRTY,
		WET,
		COMPLETE,
		NONE
	}

	#endregion

	#region EVENT
	[Header("Events")]
	public UnityEvent onComplete = new UnityEvent();

	#endregion

	#region UNITY_CALLBACK
	private void Start()
	{
		Init();
	}

	private void Update()
	{
		float progress = GetDrawerProcess(); // get current progress
		if (progress > targetProgress) {
			TextureDrawer drawer = GetCurrentTextureDrawer();
			if (drawer != null)
				drawer.SetMaskTexture(GameManager.Instance.GeneralResources.TransparentPixel);
			NextState();
			progress = GetDrawerProcess(); // get new progress
		}

		UIManager.Instance.SetProgressBar(progress);
	}

	#endregion

	#region INITIALIZATION
	protected void Init()
	{
		if (data == null) return;

		srMainPicture.sprite = data.Picture;
		srWindow.sprite = GameManager.Instance.GeneralResources.CloseWindowSprite;
		tdDirty.sr.material.SetColor("_MaskColor", data.DirtyColor);

		tdDirty.Init();
		tdWet.Init();

		state = State.DIRTY;

		UpdateUI();
	}

	public void Reset()
	{
		Init();
	}

	#endregion

	#region FUNCTION
	public void NextState()
	{
		switch (state) {
			case State.DIRTY:
				state = State.WET;
				break;
			case State.WET:
				state = State.COMPLETE;
				break;
			case State.COMPLETE:
				LevelManager.Instance.LevelCompleted(data.KeyName);
				PlayEndGameAnimation();
				state = State.NONE;
				break;
			case State.NONE:
				break;
		}
	}

	protected void UpdateUI()
	{
		UIManager.Instance.SetLabelLevelName(data.WindowName);
		UIManager.Instance.SetProgressBar(GetDrawerProcess());
	}

	public void PlayEndGameAnimation()
	{
		//PlayerInput.Instance.LockInput("window");
		srWindow.sprite = GameManager.Instance.GeneralResources.OpenWindowSprite;
		// Congrat
		UIManager.Instance.CallLayout("End Game");
	}
	#endregion

	#region GET/SET
	public TextureDrawer GetCurrentTextureDrawer()
	{
		switch (state) {
			case State.DIRTY:
				return tdDirty;
			case State.WET:
				return tdWet;
			default:
				return null;
		}
	}

	public float GetDrawerProcess()
	{
		if (state == State.COMPLETE || state == State.NONE) return 1;

		TextureDrawer drawer = GetCurrentTextureDrawer();
		if (drawer == null) return 0;
		if (drawer.totalPixel == 0) return 0;

		return ((float)drawer.blackPixel / (float)drawer.totalPixel);
	}

	#endregion
}
