using System;
using System.Collections.Generic;
using UnityEngine;

public abstract class UIWidget : MonoBehaviour
{
    public bool isShowing { get; private set; }
    
    public void Show()
    {
        if (isShowing) return;
        isShowing = true;
        OnShow();
    }

    public void Hide()
    {
        if (!isShowing) return;
        OnHide();
        isShowing = false;
    }
    
    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    protected abstract void OnShow();
    protected abstract void OnHide();
}


public abstract class UIWidget<T> : MonoBehaviour
{
    public bool isShowing { get; private set; }
    
    public void Show(T value)
    {
        if (isShowing) return;
        isShowing = true;
        OnShow(value);
    }

    public void Hide()
    {
        if (!isShowing) return;
        OnHide();
        isShowing = false;
    }

    protected virtual void Awake()
    {
        gameObject.SetActive(false);
    }

    protected abstract void OnShow(T context);
    protected abstract void OnHide();
}