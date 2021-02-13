using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class LossPopup : Popup
{
    [SerializeField] private Button restartButton;

    private LevelCreator levelCreator;

    public override void InitPopup(Action openedCallback = null, Action closedCallback = null, bool needOpen = false)
    {
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;

        restartButton.onClick.AddListener(OnRestartButtonClick);

        base.InitPopup(openedCallback, closedCallback, needOpen);
    }

    public override void Close()
    {
        PopupManager.Instance.OpenPopup(PopupList.Main);
        base.Close();
    }

    private void OnRestartButtonClick()
    {
        restartButton.onClick.RemoveListener(OnRestartButtonClick);
        base.Close();
        levelCreator.CreateLevel(true);
    }
}
