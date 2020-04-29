using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : Singleton<PlayerInput>
{
    #region DATA
    public Window window;
    public Tool tool;

    public bool IsLockInput
    {
        get
        {
            return lockChain.Count > 0;
        }
    }
    protected HashSet<string> lockChain;

    #endregion

    #region UNITY_CALLBACK
    private void Awake()
    {
        lockChain = new HashSet<string>();
    }

    void Update()
    {
        if (IsLockInput == false)
        {
            if (window != null && Input.GetMouseButton(0))
            {
                if (UIManager.Instance.IsPointerUIsObject() == false)
                {
                    if (window.GetCurrentTextureDrawer() != null && tool != null/* && tool.readyToUse == true*/)
                    {
                        window.GetCurrentTextureDrawer().DrawAt(tool.GetTargetPosition());
                        if (window.state == Window.State.DIRTY)
                        {
                            VibrationManager.Instance.OnGlassStep();
                        }
                        else if (window.state == Window.State.WET)
                        {
                            VibrationManager.Instance.OnCleanStep();
                        }

                        if (tool.Data.ToolType != ToolData.Type.BREAKER)
                        {
                            tool.Move(InputMoveTrail.Instance.GetDeltaPosition(true));
                            if (tool.Data.ToolType == ToolData.Type.GLASSER)
                            {
                                if (tool.glasserEffect != null)
                                    tool.glasserEffect.PlayGlasserEffect(true);
                            }
                        }
                    }
                }
            }

            if (window != null && Input.GetMouseButtonUp(0))
            {
                if (tool != null)
                {
                    if (tool.Data.ToolType == ToolData.Type.GLASSER)
                    {
                        if (tool.glasserEffect != null)
                        {
                            tool.glasserEffect.PlayGlasserEffect(false);
                        }
                        if (AudioManager.Instance.audioSource.isPlaying)
                        {
                            AudioManager.Instance.audioSource.Stop();
                        }
                    }
                    if (tool.Data.ToolType == ToolData.Type.CLEANER)
                    {
                        if (AudioManager.Instance.audioSource.isPlaying)
                        {
                            AudioManager.Instance.audioSource.Stop();
                        }
                    }
                }
            }
        }
    }

    #endregion

    #region FUNCTION
    public void LockInput(string key)
    {
        if (lockChain.Contains(key) == false)
            lockChain.Add(key);
    }

    public void UnlockInput(string key)
    {
        if (lockChain.Contains(key))
            lockChain.Remove(key);
    }
    #endregion
}
