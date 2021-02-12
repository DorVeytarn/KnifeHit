using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class ObstacleItemsPool : ObjectPool<ObstacleItem>
{
    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(ObstacleItemsPool), this);
    }
}
