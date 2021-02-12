using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;


public class RewardItemsPool : ObjectPool<RewardItem>
{
    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(RewardItemsPool), this);
    }
}
