using System.Collections;
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
    public float targetProgress = 0.95f;

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
    //public ExplodeExe glassExplodeExe;
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
        COMPLETE,
        NONE
    }

    // Tracking
    protected int initBPixel;

    protected CinemachineImpulseSource impulseSource;

    #endregion

    #region EVENT
    [Header("Events")]
    public UnityEvent onEnterStateDirty = new UnityEvent();
    public UnityEvent onEnterStateWet = new UnityEvent();
    public UnityEvent onEnterStateBreakGlass = new UnityEvent();
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

                        CoroutineUtils.Do(() =>
                        {
                        //glassExplodeExe.Action(holderAfterBroken);
                        glassExplode.BreakGlass();
                            impulseSource.GenerateImpulse();
                        }),
                        TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f),
                        CoroutineUtils.WaitForSecondsRealtime(0.5f),
                        TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f)

                    ));

                //if (glassExplode.isBroken == true)
                //{
                //    TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f);
                //    CoroutineUtils.WaitForSecondsRealtime(0.5f);
                //    TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f);
                //}

                //StartCoroutine(CoroutineUtils.DelaySeconds(
                //        () => holderAfterBroken.gameObject.SetActive(false),
                //        glassFallDuration));

                StartCoroutine(CoroutineUtils.DelaySeconds(
                        NextState,
                        nextStateAfterBreakGlassTime));
            }

            //if (Input.GetKeyDown(KeyCode.G))
            //{
            //    //holderAfterBroken = new GameObject(glassExplodeExe.name).transform;

            //    //StartCoroutine(CoroutineUtils.Chain(

            //    //	CoroutineUtils.Do(() =>
            //    //	{
            //    //		//glassExplodeExe.Action(holderAfterBroken);
            //    //		glassExplode.UpdateGlass();
            //    //		impulseSource.GenerateImpulse();
            //    //	}),
            //    //	TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f),
            //    //	CoroutineUtils.WaitForSecondsRealtime(0.5f),
            //    //	TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f)

            //    //));

            //    //if (glassExplode.isBroken == true)
            //    //{
            //    //	TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f);
            //    //	CoroutineUtils.WaitForSecondsRealtime(0.5f);
            //    //	TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f);
            //    //}

            //    //StartCoroutine(CoroutineUtils.DelaySeconds(
            //    //		() => holderAfterBroken.gameObject.SetActive(false),
            //    //		glassFallDuration));

            //    //StartCoroutine(CoroutineUtils.DelaySeconds(
            //    //		NextState,
            //    //		nextStateAfterBreakGlassTime));

            //    this.BreakGlass();
            //}
        }
    }

    private void BreakGlass()
    {
        glassExplode.UpdateGlass();
        if (glassExplode.isBroken == true)
        {
            TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f);
            CoroutineUtils.WaitForSecondsRealtime(0.5f);
            TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f);

            StartCoroutine(CoroutineUtils.Chain(

                    CoroutineUtils.Do(() =>
                    {
                        //glassExplodeExe.Action(holderAfterBroken);
                        glassExplode.BreakGlass();
                        impulseSource.GenerateImpulse();
                    }),
                    TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f),
                    CoroutineUtils.WaitForSecondsRealtime(0.5f),
                    TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f)

                ));

            //if (glassExplode.isBroken == true)
            //{
            //    TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 0.1f, 0.2f);
            //    CoroutineUtils.WaitForSecondsRealtime(0.5f);
            //    TimeScaleControl.Instance.DecayOverTime(Time.timeScale, 1.0f, 0.3f);
            //}

            //StartCoroutine(CoroutineUtils.DelaySeconds(
            //        () => holderAfterBroken.gameObject.SetActive(false),
            //        glassFallDuration));

            StartCoroutine(CoroutineUtils.DelaySeconds(
                    NextState,
                    nextStateAfterBreakGlassTime));
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
                break;

            case State.COMPLETE:
                onEnterStateComplete.Invoke();
                LevelManager.Instance.LevelCompleted(data.KeyName);
                PlayEndGameAnimation();

                break;
        }
    }

    public void NextState()
    {
        switch (state)
        {

            case State.DIRTY:
                ChangeState(State.WET);
                break;

            case State.WET:
                ChangeState(State.BREAK_GLASS);
                break;

            case State.BREAK_GLASS:
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
        // Congrat
        UIManager.Instance.CallLayout("End Game");
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
