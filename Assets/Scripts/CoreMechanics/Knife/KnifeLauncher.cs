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

    private TapInput tapInput;
    private KnifePool knifePool;

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

        if (tapInput != null)
            tapInput.Taped += LaunchKnife;
    }

    private void OnDestroy()
    {
        tapInput.Taped -= LaunchKnife;
    }

    private void LaunchKnife()
    {
        if (knifePool == null || knifePool.CanGetItem == false)
            return;

        var knife = knifePool.GetNextItem();

        Action lastKnifeCallback = null;

        if (knifePool.IsLastItem)
            lastKnifeCallback = LastKnifeFinished;

        knife.SetAndLaunchKnife(targetParent.transform, speed, offset, lastKnifeCallback, KnifeClashed);
    }
}
