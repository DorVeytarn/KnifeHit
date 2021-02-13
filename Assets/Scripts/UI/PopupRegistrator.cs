using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopupRegistrator : MonoBehaviour
{
    [SerializeField] private List<Popup> regitratedPopups = new List<Popup>();

    private void Start()
    {
        if (regitratedPopups.Count == 0)
            return;

        for (int i = 0; i < regitratedPopups.Count; i++)
        {
            PopupManager.Instance.RegistratePopup(regitratedPopups[i]);
            regitratedPopups[i].InitPopup();
        }
    }
}
