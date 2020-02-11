using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TextureDrawer : MonoBehaviour
{

	#region DATA
	[Header("General")]
	public string affectedTextureName = "_MainTex";
	public string maskTextureName = "_MaskTex";

	[Header("Brush")]
	public Texture2D brushTexture;
	public float brushSize;

	protected int brushWidth;  // Get from brush texture
	protected int brushHeight; // Get from brush texture

	protected Color[] brushPixels;

	// Texture
	protected Texture2D dynamicMaskTexture;
	protected SpriteRenderer sr;
	protected float spriteWidth;
	protected float spriteHeight;

	protected Texture2D affectedTexture;

	protected Color[] colors;

	// Control script flow 
	protected Vector2Int drawBoxUpperLeft;
	protected Vector2Int drawBoxResize;
	protected bool isSaving = false;
	[HideInInspector]public bool isFillAnything;

	#endregion

	#region EVENT
	[Header("Drawer events")]
	public UnityEvent onDrawStart  = new UnityEvent();
	public UnityEvent onDrawFinish = new UnityEvent();
	public UnityEvent onDrawFail   = new UnityEvent();

	#endregion

	private void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	private void Start()
	{
		Init();
	}

	private void Update()
	{
		if (Input.GetMouseButton(0)) {
			DrawAt(Input.mousePosition);
		}
	}

	public void Init()
	{
		ResetMask();
		brushWidth = brushTexture.width;
		brushHeight = brushTexture.height;
		spriteWidth = sr.sprite.rect.width;
		spriteHeight = sr.sprite.rect.height;

		colors = new Color[brushHeight * brushWidth];
		brushPixels = new Color[brushHeight * brushWidth];
		for (int y = 0; y < brushHeight; y++) {
			for (int x = 0; x < brushWidth; x++) {
				brushPixels[y * brushWidth + x] = brushTexture.GetPixel(x, y);
			}
		}
	}

	#region FUNCTION
	public void DrawAt(Vector2 targetPosition)
	{
		if (isSaving)
			return;


		Ray ray = Camera.main.ScreenPointToRay(targetPosition);
		RaycastHit2D hit = Physics2D.Raycast(ray.origin, ray.direction);

		if (hit.collider == null) return;

		// Convert hit point world space to texture UV
		Vector2 localpos = hit.point - (Vector2)(sr.transform.position + sr.sprite.bounds.min);
		Debug.Log("hit" + hit.point);
		Debug.Log("bound" + sr.sprite.bounds.min);
		Vector2 pixelUV = new Vector2(localpos.x / sr.sprite.bounds.size.x, localpos.y / sr.sprite.bounds.size.y);
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



		//isFillAnything = false;
		//for (int i = 0; i < curTexViewport.Length; i++) {
		//	colors[i] = curTexViewport[i] - brushPixels[i];
		//	if (curTexViewport[i].a < 1 && brushPixels[i].a > 0)
		//		isFillAnything = true;
		//}

		for (int i = 0; i < drawBoxResize.y; i++) {
			for (int j = 0; j < drawBoxResize.x; j++) {
				int vpcoord = i * drawBoxResize.x + j; // viewportcoord
				int bxcoord = (i - deltaY) * brushWidth + (j - deltaX);
				colors[vpcoord] = curTexViewport[vpcoord] - brushPixels[bxcoord];
			}
		}

		isSaving = true;
		Invoke("SaveTexture", 0.01f);
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
		onDrawFinish.Invoke();

		isSaving = false;
	}

	public void ResetMask()
	{
		//affectedTexture = sr.material.GetTexture(affectedTextureName);
		Texture2D blankTex = new Texture2D((int)sr.sprite.rect.width, (int)sr.sprite.rect.height, TextureFormat.Alpha8, true);
		dynamicMaskTexture = Instantiate(blankTex) as Texture2D;

		sr.material.SetTexture(maskTextureName, dynamicMaskTexture);
	}

	#endregion

}
