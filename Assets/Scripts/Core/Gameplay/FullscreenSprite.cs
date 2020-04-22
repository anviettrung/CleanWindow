using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FullscreenSprite : MonoBehaviour
{
    public CinemachineVirtualCamera cam;
    void Awake()
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();

        float cameraHeight = cam.m_Lens.OrthographicSize * 2;
        Vector2 cameraSize = new Vector2(cam.m_Lens.Aspect * cameraHeight, cameraHeight);
        Vector2 spriteSize = spriteRenderer.sprite.bounds.size;

        Vector2 scale = transform.localScale;
        if (cameraSize.x >= cameraSize.y)
        { // Landscape (or equal)
            scale *= cameraSize.x / spriteSize.x;
        }
        else
        { // Portrait
            scale *= cameraSize.y / spriteSize.y;
        }

        transform.position = Vector2.zero; // Optional
        transform.localScale = scale;
    }
}
