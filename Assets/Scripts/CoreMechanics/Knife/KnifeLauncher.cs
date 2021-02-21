using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class KnifeLauncher : MonoBehaviour
{
    [SerializeField] private float offset;
    [SerializeField] private float speed;
    [SerializeField] private float delayBetweenLaunch;
    
    private TapInput tapInput;
    private KnifePool knifePool;
    private LevelCreator levelCreator;
    private bool knifeBlocked;
    private Coroutine timerCoroutine;
    private Target target;

    public Action LastKnifeFinished;
    public Action KnifeClashed;
    public Action KnifeLaunched;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(KnifeLauncher), this);
    }

    private void Start()
    {
        tapInput = SceneComponentProvider.GetComponent(typeof(TapInput)) as TapInput;
        knifePool = SceneComponentProvider.GetComponent(typeof(KnifePool)) as KnifePool;
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;
        
        if(target == null)
            target = (SceneComponentProvider.GetComponent(typeof(Target)) as Target);

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

        Action successCallback = null;
        if (lastKnife)
            successCallback = LastKnifeFinished;
        else
            successCallback = () => { target.BumpTarget(); };

        knife.SetAndLaunchKnife(target.LaunchKnivesPoint.transform, speed, offset, successCallback, KnifeClashed);
        knifeBlocked = true;

        if(timerCoroutine == null)
            timerCoroutine = StartCoroutine(Timer());

        KnifeLaunched?.Invoke();
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSeconds(delayBetweenLaunch);
        knifeBlocked = false;
        timerCoroutine = null;
    }
}
