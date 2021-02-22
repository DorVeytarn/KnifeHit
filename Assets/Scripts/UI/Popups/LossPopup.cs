using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class LossPopup : Popup
{
    [SerializeField] private Button restartButton;
    [SerializeField] private Text score;

    private LevelCreator levelCreator;
    private UserDataManager dataManager;


    public override void InitPopup(Action openedCallback = null, Action closedCallback = null, bool needOpen = false)
    {
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;
        dataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;

        restartButton.onClick.AddListener(OnRestartButtonClick);

        base.InitPopup(openedCallback, closedCallback, needOpen);

        SetCurrentScore(dataManager.CashedScore);

        dataManager.CashedScore = 0;
    }

    public void SetCurrentScore(int value)
    {
        score.text = value.ToString();
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
