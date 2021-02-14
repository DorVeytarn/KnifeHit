using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class LevelCycle : MonoBehaviour
{
    private KnifeLauncher knifeLauncher;
    private LevelCreator levelCreator;
    private bool isFailed;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(LevelCycle), this);
    }

    private void Start()
    {
        knifeLauncher = SceneComponentProvider.GetComponent(typeof(KnifeLauncher)) as KnifeLauncher;
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;

        if(knifeLauncher != null)
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
            Debug.Log("NEW KNIFE BITCH!");
        }

        levelCreator.CreateLevel();
    }

    private void LevelFailed()
    {
        isFailed = true;
        PopupManager.Instance.OpenPopup(PopupList.Loss, null, () => { isFailed = false; });
    }
}
