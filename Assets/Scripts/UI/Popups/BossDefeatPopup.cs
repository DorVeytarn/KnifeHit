using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class BossDefeatPopup : Popup
{
    [SerializeField] private GameObject knifeModel;

    private KnifeRenewer knifeRenewer;
    private LevelCreator levelCreator;

    public override void InitPopup(Action openedCallback = null, Action closedCallback = null, bool needOpen = false)
    {
        knifeRenewer = SceneComponentProvider.GetComponent(typeof(KnifeRenewer)) as KnifeRenewer;
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;

        base.InitPopup(openedCallback, closedCallback, needOpen);

        if (knifeRenewer.UserDataManager.ChekKnifeAvailability(levelCreator.CurrentLevel.RewardKnifeName))
            return;

        SetKnife(knifeRenewer, levelCreator);
    }
    public override void Open()
    {
        base.Open();
        StartCoroutine(AnimatableDelay());
    }

    private IEnumerator AnimatableDelay()
    {
        yield return new WaitForSeconds(2f);
        Destroy();
    }

    private void SetKnife(KnifeRenewer knifeRenewer, LevelCreator levelCreator)
    {
        KnifeData newKnife = knifeRenewer.GetKnifeData(levelCreator.CurrentLevel.RewardKnifeName);

        var knifeObject = Instantiate(newKnife.Model, knifeModel.transform);
        var knifeRenderer = knifeObject.transform.GetComponentInChildren<SpriteRenderer>();

        if (knifeRenderer != null)
            knifeRenderer.sortingOrder = 0;

        knifeRenewer.SetNewKnife(newKnife, false);
    }
}
