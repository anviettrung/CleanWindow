using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextureDrawer : MonoBehaviour
{
	#region DATA
	[Header("General")]
	//public string affectedTextureName = "_MainTex";
	public string maskTextureName = "_MaskTex";
	public float savingTime = 0.02f;

	[Header("Brush")]
	public Texture2D brushTexture;

	protected int brushWidth;  // Get from brush texture
	protected int brushHeight; // Get from brush texture

	protected Color[] brushPixels; // convert brush texture into array of pixels

	[Header("Pixel Counter")]
	public int totalPixel;
	public int whitePixel;
	public int blackPixel;
	public float cutoutThresh = 0.2f; // if alpha go under this value, pixel will be black

	// Texture
	protected Texture2D dynamicMaskTexture;
	protected float textureWidth;
	protected float textureHeight;

	protected Color[] colors;

	// Tracking
	public SpriteRenderer sr;
	protected Collider2D collider2d;

	// Control script flow 
	protected Vector2Int drawBoxUpperLeft;
	protected Vector2Int drawBoxResize;
	protected bool isSaving = false;
	[HideInInspector] public bool isFillAnything;

	#endregion

	#region EVENT
	[Header("Drawer events")]
	public UnityEvent onDrawStart  = new UnityEvent();
	public UnityEvent onDrawFinish = new UnityEvent();
	public UnityEvent onDrawFail   = new UnityEvent();

	#endregion

	#region UNITY_CALLBACK
	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
		collider2d = GetComponent<Collider2D>();
	}

	private void Start()
	{
		Init();
	}

	#endregion

	#region INITIALIZATION
	public void Init()
	{
		InitMask();
		InitBrush();
		textureWidth = sr.sprite.rect.width;
		textureHeight = sr.sprite.rect.height;
	}

	public void InitMask()
	{
		//Texture2D blankTex = new Texture2D((int)sr.sprite.rect.width, (int)sr.sprite.rect.height, TextureFormat.Alpha8, true);
		dynamicMaskTexture = Instantiate(sr.material.GetTexture(maskTextureName)) as Texture2D;

		sr.material.SetTexture(maskTextureName, dynamicMaskTexture);

		UpdateCounter();
	}

	public void InitBrush()
	{
		brushWidth = brushTexture.width;
		brushHeight = brushTexture.height;

		brushPixels = new Color[brushHeight * brushWidth];
		for (int y = 0; y < brushHeight; y++) {
			for (int x = 0; x < brushWidth; x++) {
				brushPixels[y * brushWidth + x] = brushTexture.GetPixel(x, y);
			}
		}

		colors = new Color[brushHeight * brushWidth];
	}
	#endregion

	#region FUNCTION
	public void DrawAt(Vector2 targetPosition)
	{
		if (isSaving) {
			return;
		}

		Ray ray = Camera.main.ScreenPointToRay(targetPosition);
		RaycastHit2D[] hits = Physics2D.RaycastAll(ray.origin, ray.direction);

		RaycastHit2D hit = new RaycastHit2D();
		foreach (RaycastHit2D h in hits) {
			if (h.collider == this.collider2d) {
				hit = h;
				break;
			}
		}

		if (hit.collider == null) {
			// EVENT CALL
			onDrawFail.Invoke();
			return;
		}

		// Convert hit point world space to texture UV
		Vector2 localpos = hit.point - (Vector2)(sr.transform.position + sr.sprite.bounds.min);
		Vector2 pixelUV = new Vector2(localpos.x / sr.sprite.bounds.size.x, localpos.y / sr.sprite.bounds.size.y);

		// Recaculate affected region
		int startX = (int)(pixelUV.x * dynamicMaskTexture.width - brushWidth * 0.5f);
		int startY = (int)(pixelUV.y * dynamicMaskTexture.height - brushHeight * 0.5f);
		int brWidthResize = brushWidth;
		int brHeightResize = brushHeight;
		int deltaX = 0;
		int deltaY = 0;

		if (startX < 0) {
			brWidthResize += startX;
			deltaX = startX;
			startX = 0;
		}

		if (startY < 0) {
			brHeightResize += startY;
			deltaY = startY;
			startY = 0;
		}

		if (startX + brWidthResize > dynamicMaskTexture.width) {
			brWidthResize = dynamicMaskTexture.width - startX;
		}

		if (startY + brHeightResize > dynamicMaskTexture.height) {
			brHeightResize = dynamicMaskTexture.height - startY;
		}

		drawBoxUpperLeft = new Vector2Int(
			startX,
			startY
		);

		drawBoxResize = new Vector2Int(
			brWidthResize,
			brHeightResize
		);



		Color[] curTexViewport = dynamicMaskTexture.GetPixels(
			drawBoxUpperLeft.x, drawBoxUpperLeft.y,
			drawBoxResize.x, drawBoxResize.y
		);

		isSaving = true;

		// EVENT CALL
		onDrawStart.Invoke();

		for (int i = 0; i < drawBoxResize.y; i++) {
			for (int j = 0; j < drawBoxResize.x; j++) {
				int vpcoord = i * drawBoxResize.x + j; // viewportcoord
				int bxcoord = (i - deltaY) * brushWidth + (j - deltaX);
				colors[vpcoord].a = curTexViewport[vpcoord].a - brushPixels[bxcoord].a;
			}
		}

		SaveTexture();
	}

	//Sets the base material with a our canvas texture, then removes all our brushes
	void SaveTexture()
	{
		dynamicMaskTexture.SetPixels(
			drawBoxUpperLeft.x, drawBoxUpperLeft.y,
			drawBoxResize.x, drawBoxResize.y,
			colors
		);

		dynamicMaskTexture.Apply();

		UpdateCounter();

		// EVENT CALL
		onDrawFinish.Invoke();

		Invoke("SavingDone", savingTime);
	}

	void SavingDone()
	{
		isSaving = false;
	}

	void UpdateCounter()
	{
		Color[] texmap = dynamicMaskTexture.GetPixels();
		totalPixel = texmap.Length;
		blackPixel = 0;
		for (int i = 0; i < totalPixel; i++)
			if (texmap[i].a < cutoutThresh)
				blackPixel++;
		whitePixel = totalPixel - blackPixel;
	}

	public void SetMaskTexture(Texture2D tex)
	{
		sr.material.SetTexture(maskTextureName, tex);
	}

	#endregion

}
