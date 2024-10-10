using JUtils;
using UnityEngine;


public abstract class UIScreen<T> : SingletonBehaviour<T> where T : UIScreen<T>
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

    protected override void Awake()
    {
        base.Awake();
        gameObject.SetActive(false);
    }

    protected abstract void OnShow();
    protected abstract void OnHide();
}