﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public enum UDType
{
    Reward,
    Score,
    Knife
}

public class UserDataManager : MonoBehaviour
{
    private List<string> currentKnives;
    private LevelCycle levelCycle;
    private LevelCreator levelCreator;

    public Action<int> RewardAmountChanged;
    public Action<int> ScoreChanged;
    public Action<int> HightScoreChanged;
    public Action<string> KnivesChanged;

    public UserData CurrentUserData { get; private set; }
    public int CurrentRewardAmount { get; private set; }
    public int CurrentScore { get; private set; }
    public int HightScore { get; private set; }

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(UserDataManager), this);

        CurrentUserData = PlayerPrefsUtility.LoadUserData();

        CurrentRewardAmount = CurrentUserData.RewardAmount;
        HightScore = CurrentUserData.HightScore;
        currentKnives = CurrentUserData.OpenedKnives;

        CurrentScore = 0;
    }

    private void Start()
    {
        levelCycle = SceneComponentProvider.GetComponent(typeof(LevelCycle)) as LevelCycle;
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;

        levelCycle.Failed += SaveCurrentUserData;
    }

    private void OnDestroy()
    {
        levelCycle.Failed -= SaveCurrentUserData;
        SaveCurrentUserData();
    }

    private void UpadateAllDatas()
    {
        RewardAmountChanged?.Invoke(CurrentRewardAmount);
        ScoreChanged?.Invoke(CurrentScore);
        KnivesChanged?.Invoke(currentKnives[currentKnives.Count - 1]);
    }

    public void UpdateUserData(UDType UDType, object arg)
    {
        switch (UDType)
        {
            case UDType.Reward:
                    CurrentRewardAmount += Convert.ToInt32(arg);
                RewardAmountChanged?.Invoke(CurrentRewardAmount);
                break;
            case UDType.Score:
                CurrentScore += Convert.ToInt32(arg);
                ScoreChanged?.Invoke(CurrentScore);

                if (CurrentScore > HightScore)
                {
                    HightScore = CurrentScore;
                    HightScoreChanged?.Invoke(HightScore);
                }
                break;
            case UDType.Knife:
                    currentKnives.Add(Convert.ToString(arg));
                KnivesChanged?.Invoke(currentKnives[currentKnives.Count - 1]);
                break;
        }
    }

    public void SaveCurrentUserData()
    {
        CurrentScore = 0;
        ScoreChanged?.Invoke(CurrentScore);

        PlayerPrefsUtility.SaveUserDataField(CurrentRewardAmount, HightScore, currentKnives);
    }

    private void ClearCurrentScore()
    {
        CurrentScore = 0;
    }
}
