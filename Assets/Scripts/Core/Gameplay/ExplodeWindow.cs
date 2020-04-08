using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Destructible2D;

public enum GlassSide
{
    LEFT, RIGHT
}

public class ExplodeWindow : MonoBehaviour
{
    private Window window;
    private float forceBreak = 0f;

    public List<D2dDestructible> fragments = new List<D2dDestructible>();
    public D2dDestructible glassLeft;
    public D2dDestructible glassRight;
    public GlassSide glassSide;
    [HideInInspector] public bool isBroken = false;
    [HideInInspector] public bool changeSide;
    [HideInInspector] public Animation breakerAnim;

    private void Start()
    {
        fragments.Add(glassLeft);
        glassSide = GlassSide.LEFT;
        window = GetComponent<Window>();
    }

    private void Update()
    {
        if (window.state == Window.State.BREAK_GLASS)
        {
            if (changeSide == true && glassSide == GlassSide.LEFT)
            {
                ChangeWindow();
            }
            if (isBroken == false)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    if (breakerAnim != null)
                    {
                        if (breakerAnim.isPlaying)
                        {
                            breakerAnim.Stop();
                        }
                        breakerAnim.Play("Break");
                    }
                    forceBreak += 50f;
                    for (int i = 0; i < fragments.Count; i++)
                    {
                        fragments[i].Damage = forceBreak;
                    }
                    //Debug.Log("<b>On Mouse Down</b>");
                }

                if (Input.GetMouseButton(0))
                {
                    if (breakerAnim != null)
                    {
                        if (breakerAnim.isPlaying)
                        {
                            breakerAnim.Stop();
                        }
                        breakerAnim.Play("Break");
                    }
                    forceBreak += 1f;
                    for (int i = 0; i < fragments.Count; i++)
                    {
                        fragments[i].Damage = forceBreak;
                    }
                    //Debug.Log("<b>On Mouse Drag</b>");
                }

                if (Input.GetMouseButtonUp(0))
                {
                    BreakGlass();
                    //Debug.Log("<color=red>On Mouse Up</color>");
                }
            }
        }
    }

    public void UpdateGlass()
    {
        for (int i = 0; i < fragments.Count; i++)
        {
            fragments[i].Damage += 10;
        }
        if (fragments.Count > 0 && fragments[0].Damage >= 300)
        {
            //fragments.Clear();
            isBroken = true;
            for (int i = 0; i < fragments.Count; i++)
            {
                fragments[i].transform.GetChild(0).gameObject.SetActive(true);
            }
        }
    }

    public void BreakGlass()
    {
        if (fragments.Count > 0 && fragments[0].Damage >= 300)
        {
            for (int i = 0; i < fragments.Count; i++)
            {
                var rigidbody_2D = fragments[i].GetComponent<Rigidbody2D>();
                //rigidbody_2D.constraints = RigidbodyConstraints2D.FreezeRotation;
                rigidbody_2D.constraints = RigidbodyConstraints2D.None;
                rigidbody_2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
                rigidbody_2D.gravityScale = 5f;
            }

            fragments.Clear();
            if (glassSide == GlassSide.RIGHT)
            {
                isBroken = true;
            }
            if (glassSide == GlassSide.LEFT)
            {
                changeSide = true;
            }
        }
    }

    public void ChangeWindow()
    {
        fragments.Clear();
        fragments.Add(glassRight);
        glassSide = GlassSide.RIGHT;

        if (breakerAnim != null)
        {
            if (breakerAnim.isPlaying)
            {
                breakerAnim.Stop();
            }
            breakerAnim.Play("MoveTool");
        }
        forceBreak = 0f;
    }
}
