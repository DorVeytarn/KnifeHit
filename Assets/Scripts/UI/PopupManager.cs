using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupManager : Singleton<PopupManager>
{
    [SerializeField] private List<GameObject> immortalPopups = new List<GameObject>();

    public Action PopupOpeningStarted;
    public Action PopupClosingStarted;

    private Transform popupParent;
    private List<Popup> popupStack = new List<Popup>();

    public int PopupCount => popupStack.Count;

    public void RegistratePopupParent(Transform parent)
    {
        popupParent = parent;
    }

    public void RegistratePopup(Popup popup)
    {
        popupStack.Add(popup);
    }

    public void ClosePopup(Popup closingPopup)
    {
        if (!popupStack.Contains(closingPopup))
        {
            Debug.LogWarning("popupStack not has closingPopup! closingPopup.name:" + (closingPopup == null ? "<null>" : closingPopup.name), this);
            return;
        }

        PopupClosingStarted?.Invoke();

        closingPopup.Close();
    }

    public void OpenPopup(string popupName, Action popupOpenedCallback, Action popupClosedCallback)
    {
        OpenPopupCoroutine(popupName, popupOpenedCallback, popupClosedCallback);
    }

    private IEnumerator OpenPopupCoroutine(string popupName, Action popupOpenedCallback, Action popupClosedCallback)
    {
        PopupOpeningStarted.Invoke();

        if (popupStack.Contains(TryGetPopup(popupName)))
        {
            var popupToOpen = popupStack.Find(popup => popup.PopupName == popupName);

            if(popupToOpen != null)
                popupToOpen.InitPopup(popupOpenedCallback, popupClosedCallback, true);

            yield break;
        }

        CreatePopup(popupName, popupOpenedCallback, popupClosedCallback);
    }

    private void CreatePopup(string popupName, Action popupOpenedCallback, Action popupClosedCallback)
    {
        if (immortalPopups.Count == 0)
            return;

        var popupObject = Instantiate(immortalPopups.Find(popup => name == popupName), popupParent);
        var newPopup = popupObject.GetComponent<Popup>();

        popupStack.Add(newPopup);

        newPopup.InitPopup(popupOpenedCallback, popupClosedCallback, true);
    }

    private Popup TryGetPopup(string popupName)
    {
        for (int i = 0; i < popupStack.Count; i++)
        {
            if (popupStack[i].PopupName == popupName)
                return popupStack[i];
        }

        return null;
    }
}
