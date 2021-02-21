using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OpenPopupButton : MonoBehaviour
{
    [SerializeField] private string popupName;
    [SerializeField] private Button selfButton;

    private void Start()
    {
        selfButton.onClick.AddListener(OnSelfButtonClick);
    }

    private void OnDestroy()
    {
        selfButton.onClick.RemoveListener(OnSelfButtonClick);
    }

    private void OnSelfButtonClick()
    {
        PopupManager.Instance.OpenPopup(popupName, null, null);
    }
}
