using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Popup : MonoBehaviour
{
    [SerializeField] private string popupName;

    public string PopupName => popupName;

    private Action popupOpened;
    private Action popupClosed;

    public void InitPopup(Action openedCallback, Action closedCallback, bool needOpen = false)
    {
        popupOpened = openedCallback;
        popupClosed = closedCallback;

        if (needOpen)
            Open();
    }

    public void Close()
    {
        gameObject.SetActive(false);
        popupClosed?.Invoke();
    }

    public void Open()
    {
        gameObject.SetActive(true);
        popupOpened?.Invoke();
    }
}
