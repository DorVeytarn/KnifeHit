using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class ScoreView : UserProgressView
{
    [SerializeField] private CanvasGroup selfCanvasGroup;
    [SerializeField] private bool isHightScore;

    private LevelCycle levelCycle;
    private LevelCreator levelCreator;

    protected override void Start()
    {
        levelCycle = SceneComponentProvider.GetComponent(typeof(LevelCycle)) as LevelCycle;
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;

        levelCycle.Failed += Hide;
        levelCreator.LevelCreated += Show;

        base.Start();
        if (isHightScore)
        {
            dataManager.HightScoreChanged += UpdateView;
            UpdateView(dataManager.HightScore);
        }
        else
        {
            dataManager.ScoreChanged += UpdateView;
            UpdateView(dataManager.CurrentScore);
        }
    }


    protected override void UnSubscribeOnProgressChange()
    {
        levelCycle.Failed -= Hide;
        levelCreator.LevelCreated -= Show;

        dataManager.ScoreChanged -= UpdateView;
        base.UnSubscribeOnProgressChange();
    }

    public override void Hide()
    {
        if (selfCanvasGroup != null)
            selfCanvasGroup.alpha = 0;
    }

    public override void Show()
    {
        if (selfCanvasGroup != null)
            selfCanvasGroup.alpha = 1;
    }
}
