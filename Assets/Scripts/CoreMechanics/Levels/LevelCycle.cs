using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class LevelCycle : MonoBehaviour
{
    private KnifeLauncher knifeLauncher;
    private LevelCreator levelCreator;
    private UserDataManager dataManager;
    private bool isFailed;

    public Action Failed;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(LevelCycle), this);
    }

    private void Start()
    {
        knifeLauncher = SceneComponentProvider.GetComponent(typeof(KnifeLauncher)) as KnifeLauncher;
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;
        dataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;

        if (knifeLauncher != null)
        {
            knifeLauncher.KnifeClashed += LevelFailed;
            knifeLauncher.LastKnifeFinished += LevelPassed;
        }
    }

    private void OnDestroy()
    {
        if (knifeLauncher != null)
        {
            knifeLauncher.KnifeClashed -= LevelFailed;
            knifeLauncher.LastKnifeFinished -= LevelPassed;
        }
    }

    private void LevelPassed()
    {
        if (levelCreator == null)
            return;

        if (isFailed)
        {
            isFailed = false;
            return;
        }

        if (levelCreator.CurrentLevel.IsBossLevel)
        {
            levelCreator.DestroyLevel(() =>
            {
                PopupManager.Instance.OpenPopup(PopupList.BossDefeat, null, () => levelCreator.CreateLevel());
            });
        }
        else
            levelCreator.DestroyLevel(() => levelCreator.CreateLevel());

        dataManager.CashedScore = 0;
    }

    private void LevelFailed()
    {
        isFailed = true;
        PopupManager.Instance.OpenPopup(PopupList.Loss, () =>
        {
            levelCreator.Target.ClearTarget();
            dataManager.CashedScore = dataManager.CurrentScore;

        }, () => { isFailed = false; });

        Failed?.Invoke();
    }
}
