using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class MainPopup : Popup
{
    [SerializeField] private Button playButton;

    private LevelCreator levelCreator;

    public override void InitPopup(Action openedCallback, Action closedCallback, bool needOpen = false)
    {
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;

        playButton.onClick.AddListener(OnPlayButtonClick);
        base.InitPopup(openedCallback, closedCallback, needOpen);
    }

    protected override void OnSelfButtonClick()
    {
        PopupManager.Instance.OpenPopup(PopupList.Settings);
    }

    private void OnPlayButtonClick()
    {
        levelCreator.CreateLevel();
        Close();
    }

    private void OnDestroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
    }
}
