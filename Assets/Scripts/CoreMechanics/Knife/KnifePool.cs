using Utils.Singletone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KnifePool : ObjectPool<Knife>
{
    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(KnifePool), this);
    }

    public void SetFirstKnive()
    {
        items[0].gameObject.SetActive(true);
    }
}
