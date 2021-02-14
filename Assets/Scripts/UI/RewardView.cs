using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class RewardView : UserProgressView
{
    protected override void Start()
    {
        base.Start();

        dataManager.RewardAmountChanged += UpdateView;

        UpdateView(dataManager.CurrentRewardAmount);
    }

    protected override void UnSubscribeOnProgressChange()
    {
        dataManager.RewardAmountChanged -= UpdateView;

        base.UnSubscribeOnProgressChange();
    }
}
