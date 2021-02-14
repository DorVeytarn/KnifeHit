using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class UserProgressView : MonoBehaviour
{
    [SerializeField] private Text counter;

    protected int resourceValue;
    protected UserDataManager dataManager;

    protected Action<int> changedEvent;

    protected virtual void Start()
    {
        dataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;
    }

    protected virtual void UpdateView(int newValue)
    {
        counter.text = newValue.ToString();
    }

    protected virtual void UnSubscribeOnProgressChange() { }

    public virtual void Show() { }

    public virtual void Hide() { }

    private void OnDestroy()
    {
        UnSubscribeOnProgressChange();
    }
}
