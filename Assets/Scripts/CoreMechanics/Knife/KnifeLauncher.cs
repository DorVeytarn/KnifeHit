using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class KnifeLauncher : MonoBehaviour
{
    [SerializeField] private GameObject targetParent;
    [SerializeField] private float offset;
    [SerializeField] private float speed;
    [SerializeField] private float delayBetweenLaunch;
    
    private TapInput tapInput;
    private KnifePool knifePool;
    private LevelCreator levelCreator;
    private bool knifeBlocked;
    private Coroutine timerCoroutine;

    public Action LastKnifeFinished;
    public Action KnifeClashed;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(KnifeLauncher), this);
    }

    private void Start()
    {
        tapInput = SceneComponentProvider.GetComponent(typeof(TapInput)) as TapInput;
        knifePool = SceneComponentProvider.GetComponent(typeof(KnifePool)) as KnifePool;
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;
        
        if(targetParent == null)
            targetParent = (SceneComponentProvider.GetComponent(typeof(Target)) as Target).LaunchKnivesPoint;

        if (tapInput != null)
            tapInput.Taped += LaunchKnife;
    }

    private void OnDestroy()
    {
        tapInput.Taped -= LaunchKnife;
    }

    private void LaunchKnife()
    {
        if (knifeBlocked || knifePool == null || knifePool.CanGetItem == false)
            return;

        bool lastKnife = (knifePool.ItemCount - 1 == knifePool.ItemMaxCount - levelCreator.CurrentLevel.RequiredKnifeAmount);

        var knife = knifePool.GetNextItem(!lastKnife);

        if (knife == null)
            return;

        Action lastKnifeCallback = null;

        if (lastKnife)
            lastKnifeCallback = LastKnifeFinished;

        knife.SetAndLaunchKnife(targetParent.transform, speed, offset, lastKnifeCallback, KnifeClashed);

        knifeBlocked = true;

        if(timerCoroutine == null)
            timerCoroutine = StartCoroutine(Timer());
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(delayBetweenLaunch);
        knifeBlocked = false;
        timerCoroutine = null;
    }
}
