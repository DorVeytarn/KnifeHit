using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class MainPopup : Popup
{
    [SerializeField] private Button playButton;
    [SerializeField] private GameObject knifeModel;

    private LevelCreator levelCreator;
    private UserDataManager userDataManager;
    private KnifeData currentKnife;

    private void Start()
    {
        playButton.onClick.AddListener(OnPlayButtonClick);
    }

    public override void InitPopup(Action openedCallback, Action closedCallback, bool needOpen = false)
    {
        if(levelCreator == null)
            levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;
        if (userDataManager == null)
            userDataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;

        base.InitPopup(openedCallback, closedCallback, needOpen);

        SetKnife(levelCreator);
    }

    private void SetKnife(LevelCreator levelCreator)
    {
        if (currentKnife == userDataManager.CurrentKnife)
            return;

        currentKnife = userDataManager.CurrentKnife;

        var knifeObject = Instantiate(currentKnife.Model, knifeModel.transform);
        var knifeRenderer = knifeObject.transform.GetComponentInChildren<SpriteRenderer>();

        if (knifeRenderer != null)
            knifeRenderer.sortingOrder = 0;
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
        base.Destroy();
    }
}
