﻿using System.Collections;
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

    private float holdingTime;
    private bool canTap;

    private void Start()
    {
        fragments.Add(glassLeft);
        glassSide = GlassSide.LEFT;
        window = GetComponent<Window>();

        holdingTime = 0f;
        canTap = true;
    }

    private void Update()
    {
        if (window.state == Window.State.BREAK_GLASS)
        {
            if (changeSide == true && glassSide == GlassSide.LEFT)
            {
                ChangeWindow();
            }
            if (isBroken == false && canTap == true)
            {
                if (Input.GetMouseButtonDown(0))
                {
                    forceBreak += 100f;
                    //Debug.Log("<b>On Mouse Down</b>");
                }

                else if (Input.GetMouseButton(0))
                {
                    holdingTime += Time.deltaTime;
                    if (holdingTime > 1f)
                    {
                        if (breakerAnim != null)
                        {
                            if (breakerAnim.isPlaying)
                            {
                                breakerAnim.Stop();
                            }
                            breakerAnim.Play("Holding");
                        }
                        LevelManager.Instance.currentWindow.impulseSource.GenerateImpulse();
                        forceBreak += 1f;
                        //Debug.Log("<b>On Mouse Drag</b>");
                    }
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (UIManager.Instance.tapAndHold.activeInHierarchy == true)
                    {
                        UIManager.Instance.tapAndHold.SetActive(false);
                    }
                    if (breakerAnim != null)
                    {
                        //if (breakerAnim.isPlaying)
                        if (holdingTime > 1f)
                        {
                            if (breakerAnim.IsPlaying("Holding"))
                            {
                                breakerAnim.Stop("Holding");
                            }
                            breakerAnim.Play("Release");
                            for (int i = 0; i < fragments.Count; i++)
                            {
                                fragments[i].Damage = 350f;
                            }
                            BreakGlass();
                        }
                        else
                        {
                            if (breakerAnim.isPlaying)
                            {
                                breakerAnim.Stop();
                            }
                            breakerAnim.Play("Break");
                        }
                    }
                    for (int i = 0; i < fragments.Count; i++)
                    {
                        fragments[i].Damage = forceBreak;
                    }
                    BreakGlass();
                    holdingTime = 0f;
                    //Debug.Log("<color=red>On Mouse Up</color>");
                }
            }
        }
        if (window.state == Window.State.COMPLETE)
        {
            if (this.fragments.Count > 0)
            {
                this.fragments.Clear();
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
                rigidbody_2D.constraints = RigidbodyConstraints2D.None;
                //rigidbody_2D.sleepMode = RigidbodySleepMode2D.NeverSleep;
                rigidbody_2D.sleepMode = RigidbodySleepMode2D.StartAwake;
                rigidbody_2D.gravityScale = Random.Range(2f, 5f);
            }

            //fragments.Clear();
            if (glassSide == GlassSide.RIGHT)
            {
                isBroken = true;
            }
            if (glassSide == GlassSide.LEFT)
            {
                changeSide = true;
            }

            //LevelManager.Instance.currentWindow.impulseSource.GenerateImpulse();
        }
    }

    public void ChangeWindow()
    {
        canTap = false;
        if (breakerAnim != null)
        {
            if (breakerAnim.isPlaying)
            {
                breakerAnim.Stop();
                breakerAnim.Play("MoveTool");
            }
        }
        StartCoroutine(CoroutineUtils.DelaySeconds(() =>
        {
            foreach (var frag in fragments)
            {
                Destroy(frag.gameObject);
            }
            canTap = true;
            fragments.Clear();
            fragments.Add(glassRight);
        }, breakerAnim.GetClip("MoveTool").length));
        if (UIManager.Instance.tapAndHold.activeInHierarchy == false)
        {
            UIManager.Instance.tapAndHold.SetActive(true);
        }
        //fragments.Clear();
        //fragments.Add(glassRight);
        glassSide = GlassSide.RIGHT;
        forceBreak = 0f;
    }
}
