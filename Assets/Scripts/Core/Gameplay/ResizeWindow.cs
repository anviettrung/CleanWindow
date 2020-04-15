using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.iOS;

public class ResizeWindow : MonoBehaviour
{
    public SpriteRenderer spriteRenderer;

    private float ratioWidth = 0f;
    private float ratioHeight = 0f;

    private const float ORTHORGRAPHIC_SIZE = 12.144f;
    void Start()
    {
        //Camera cam = Camera.main;
        //float height = cam.orthographicSize;
        float height = ORTHORGRAPHIC_SIZE;
        float width = height * Screen.width / Screen.height;
        float unit_width = spriteRenderer.sprite.textureRect.width / spriteRenderer.sprite.pixelsPerUnit;
        float unit_height = spriteRenderer.sprite.textureRect.height / spriteRenderer.sprite.pixelsPerUnit;

#if UNITY_IOS
        if (Device.generation.ToString().Contains("iPad"))
        {
            ratioWidth = ratioHeight = 0.8f;
        }
        else
        {
            ratioWidth = 0.7f;
            ratioHeight = 0.8f;
        }
#elif UNITY_ANDROID
        ratioWidth = ratioHeight = 0.8f;
#elif UNITY_EDITOR 
        ratioWidth = ratioHeight = 0.8f;
#endif

        //on device
        this.transform.localScale = new Vector3(width / unit_width * ratioWidth, height / unit_height * ratioHeight);

        //for testing
        //this.transform.localScale = new Vector3(width / unit_width * 0.7f, height / unit_height * 0.8f);
    }
}
