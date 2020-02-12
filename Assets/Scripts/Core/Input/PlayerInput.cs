using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
	public TextureDrawer drawer;

    // Update is called once per frame
    void Update()
    {
        if (drawer != null && Input.GetMouseButton(0)) {
			drawer.DrawAt(Input.mousePosition);
		}
	}
}
