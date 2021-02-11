using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils.singletone;

public class KnifeLauncher : MonoBehaviour
{
    [SerializeField] private GameObject targetParent;
    [SerializeField] private float offset;
    [SerializeField] private float speed;

    private TapInput tapInput;
    private KnifePool knifePool;

    public Action LastKnifeFinished;
    public Action KnifeClashed;

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
        if (knifePool == null || knifePool.CheckKnifeAmount() == false)
            return;

        var knife = knifePool.GetNextKnife();

        Action lastKnifeCallback = null;

        if (knifePool.IsLastKnife)
            lastKnifeCallback = LastKnifeFinished;

        knife.SetAndLaunchKnife(targetParent.transform, speed, offset, lastKnifeCallback, KnifeClashed);
    }
}
