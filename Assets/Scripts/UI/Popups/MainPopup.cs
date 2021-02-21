using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class MainPopup : Popup
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject knifeView;

    private LevelCreator levelCreator;
    private UserDataManager userDataManager;

    public override void InitPopup(Action openedCallback, Action closedCallback, bool needOpen = false)
    {
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;
        userDataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;

        playButton.onClick.AddListener(OnPlayButtonClick);
        userDataManager.KnivesChanged += UpdateKnifeView;
        base.InitPopup(openedCallback, closedCallback, needOpen);
    }

    public void UpdateKnifeView(KnifeData knifeData)
    {
        knifeView = knifeData.Model;
    }

    protected override void OnSelfButtonClick()
    {
        PopupManager.Instance.OpenPopup(PopupList.Settings);
    }

    private void OnPlayButtonClick()
    {
        levelCreator.CreateLevel(true);
        Close();
    }

    public override void Destroy()
    {
        playButton.onClick.RemoveListener(OnPlayButtonClick);
        userDataManager.KnivesChanged -= UpdateKnifeView;

        base.Destroy();
    }
}
