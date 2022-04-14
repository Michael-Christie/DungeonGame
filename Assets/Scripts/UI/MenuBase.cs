//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public abstract class MenuBase : MonoBehaviour
//{
//    private Action onShowComplete;
//    private Action onHideComplete;

//    public GameObject menuParent;

//    protected bool isActive = false;
//    protected bool hasInitalized = false;

//    //
//    public abstract void Initalize();

//    public virtual void Show(Action _onFinishShow = null)
//    {
//        onShowComplete += _onFinishShow;
//    }

//    protected void OnShowComplete()
//    {
//        onShowComplete?.Invoke();
//        onShowComplete = null;
//    }

//    public virtual void Hide(Action _onFinishHide = null)
//    {
//        onHideComplete += _onFinishHide;
//    }

//    protected void OnHideComplete()
//    {
//        onHideComplete?.Invoke();
//        onHideComplete = null;
//    }

//    protected abstract IEnumerator PlayShowAnimation();
//    protected abstract IEnumerator PlayHideAnimation();
//}
