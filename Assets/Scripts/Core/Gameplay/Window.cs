﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Cinemachine;

public class Window : MonoBehaviour
{
    #region DATA
    [Header("Data")]
    [SerializeField] protected WindowData data;
    public WindowData Data
    {
        get
        {
            return data;
        }
        set
        {
            data = value;
            Init();
        }
    }

    [Header("General setting")]
    public float targetProgress;

    [Header("Sprite Renderers")]
    public SpriteRenderer srMainPicture;
    public SpriteRenderer srWet;
    public SpriteRenderer srDirty;
    public SpriteRenderer srWindow;
    public SpriteRenderer srWindowBorder;

    [Header("Texture Drawer")]
    public TextureDrawer tdDirty;
    public TextureDrawer tdWet;

    [Header("Break Glass Settings")]
    public ExplodeWindow glassExplode;
    public float glassFallDuration = 2.5f;
    public float nextStateAfterBreakGlassTime = 1.0f;

    protected Transform holderAfterBroken;

    [Header("State")]
    public Window.State state;
    public enum State
    {
        DIRTY,
        WET,
        BREAK_GLASS,
        CAPTURE,
        COMPLETE,
        NONE
    }

    // Tracking
    protected int initBPixel;

    [HideInInspector] public CinemachineImpulseSource impulseSource;

    [Header("Effects")]
    public ParticleSystem bubbleEffect;
    public ParticleSystem starEffect;
    public ParticleSystem twinkleEffect;

    #endregion

    #region EVENT
    [Header("Events")]
    public UnityEvent onEnterStateDirty = new UnityEvent();
    public UnityEvent onEnterStateWet = new UnityEvent();
    public UnityEvent onEnterStateBreakGlass = new UnityEvent();
    public UnityEvent onEnterStateCapture = new UnityEvent();
    public UnityEvent onEnterStateComplete = new UnityEvent();

    #endregion

    #region UNITY_CALLBACK
    private void Start()
    {
        Init();
        impulseSource = FindObjectOfType<CinemachineImpulseSource>();
    }

    private void Update()
    {
        if (state == State.DIRTY || state == State.WET)
        {
            float progress = GetDrawerProcess(); // get current progress
            if (progress > targetProgress)
            {
                TextureDrawer drawer = GetCurrentTextureDrawer();
                if (drawer != null)
                    drawer.SetMaskTexture(GameManager.Instance.GeneralResources.TransparentPixel);
                NextState();
                progress = GetDrawerProcess(); // get new progress
            }

            UIManager.Instance.SetProgressBar(progress);
        }
        else if (state == State.BREAK_GLASS)
        {
            if (glassExplode.isBroken == true)
            {
                TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f);
                CoroutineUtils.WaitForSecondsRealtime(0.5f);
                TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f);

                StartCoroutine(CoroutineUtils.Chain(

                        //CoroutineUtils.Do(() =>
                        //{
                        ////glassExplodeExe.Action(holderAfterBroken);
                        //glassExplode.BreakGlass();
                        //    //impulseSource.GenerateImpulse();
                        //}),
                        TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f),
                        CoroutineUtils.WaitForSecondsRealtime(0.5f),
                        TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f)

                    ));
                NextState();
                //StartCoroutine(CoroutineUtils.DelaySeconds(
                //        NextState,
                //        nextStateAfterBreakGlassTime));
            }
        }
    }

    private void OnDestroy()
    {
        if (holderAfterBroken != null)
            Destroy(holderAfterBroken.gameObject);
    }

    #endregion

    #region INITIALIZATION
    protected void Init()
    {
        if (data == null) return;

        this.targetProgress = ConfigManager.Instance.gameNumberConfig.TargetProgress;

        srMainPicture.sprite = data.Picture;
        //srWindow.sprite = GameManager.Instance.GeneralResources.CloseWindowSprite;
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
    public void ChangeState(Window.State s)
    {
        state = s;

        switch (s)
        {
            case State.DIRTY:
                onEnterStateDirty.Invoke();
                initBPixel = tdDirty.blackPixel;
                break;

            case State.WET:
                onEnterStateWet.Invoke();
                initBPixel = tdWet.blackPixel;
                break;

            case State.BREAK_GLASS:
                onEnterStateBreakGlass.Invoke();
                srDirty.gameObject.SetActive(false);
                srWet.gameObject.SetActive(false);
                CameraMaster.Instance.TransitionToView(CameraMaster.View.MEDIUM_SHOT_1);
                break;

            case State.CAPTURE:
                //onEnterStateCapture.Invoke();
                PlayEndGameAnimation();
                break;

            case State.COMPLETE:
                onEnterStateComplete.Invoke();
                LevelManager.Instance.LevelCompleted(data.KeyName);

                //Game analytic
                GameAnalyticManager.Instance.EndLevel();

                break;
        }

        UIManager.Instance.ShowNextStepProgress(s);
    }

    public void NextState()
    {
        switch (state)
        {

            case State.DIRTY:
                this.bubbleEffect.Play();
                ChangeState(State.WET);
                break;

            case State.WET:
                this.twinkleEffect.Play();
                ChangeState(State.BREAK_GLASS);
                break;

            case State.BREAK_GLASS:
                this.starEffect.Play();
                ChangeState(State.CAPTURE);
                break;

            case State.CAPTURE:
                ChangeState(State.COMPLETE);
                break;

            case State.COMPLETE:
                ChangeState(State.NONE);
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
        srWindow.GetComponent<Animator>().SetTrigger("Open");
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            onEnterStateCapture.Invoke();
            UIManager.Instance.cityName.text = this.Data.WindowName;
        }, 1f));

        //Add haptic:
        VibrationManager.Instance.OnCompleteLevel();

        // Congrat
        //UIManager.Instance.CallLayout("End Game");
    }
    #endregion

    #region GET/SET
    public TextureDrawer GetCurrentTextureDrawer()
    {
        switch (state)
        {
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

        return (float)(drawer.blackPixel - initBPixel) / (float)(drawer.totalPixel - initBPixel);
    }

    #endregion
}
