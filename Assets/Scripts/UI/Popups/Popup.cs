using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Popup : MonoBehaviour
{
    [SerializeField] private string popupName;

    [Header("Угловая кнопочка")]
    [SerializeField] private Button selfButton;

    public string PopupName => popupName;

    private Action popupOpened;
    private Action popupClosed;

    public virtual void InitPopup(Action openedCallback = null, Action closedCallback = null, bool needOpen = false)
    {
        popupOpened = openedCallback;
        popupClosed = closedCallback;

        if(selfButton != null)
            selfButton.onClick.AddListener(OnSelfButtonClick);

        if (needOpen)
            Open();
    }

    public virtual void Close()
    {
        if (selfButton != null)
            selfButton.onClick.RemoveListener(OnSelfButtonClick);

        gameObject.SetActive(false);
        popupClosed?.Invoke();
    }

    public virtual void Destroy()
    {
        PopupManager.Instance.RemovePopup(this);
        popupClosed?.Invoke();
        Destroy(gameObject);
    }

    public virtual void Open()
    {
        gameObject.SetActive(true);
        popupOpened?.Invoke();
    }

    protected virtual void OnSelfButtonClick()
    {
        Close();
    }
}
