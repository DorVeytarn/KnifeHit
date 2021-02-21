using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRegistrator : MonoBehaviour
{
    [SerializeField] private List<Popup> registratedPopups = new List<Popup>();

    private void Start()
    {
        if (registratedPopups.Count == 0)
            return;

        for (int i = 0; i < registratedPopups.Count; i++)
        {
            PopupManager.Instance.RegistratePopup(registratedPopups[i]);
            registratedPopups[i].InitPopup();
        }
    }
}
