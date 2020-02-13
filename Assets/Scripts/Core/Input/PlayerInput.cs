using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
	public Window window;

    void Update()
    {
        if (window != null && Input.GetMouseButton(0)) {
			if (window.GetCurrentTextureDrawer() != null)
				window.GetCurrentTextureDrawer().DrawAt(Input.mousePosition);
		}
	}
}
