using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;


public class RewardItemsPool : ObjectPool<RewardItem>
{
    private UserDataManager dataManager;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(RewardItemsPool), this);
    }

    private void Start()
    {
        dataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;
    }

    public override void CreateItems(int amount = 0)
    {
        base.CreateItems(amount);

        for (int i = 0; i < items.Count; i++)
        {
            items[i].SetDyingCallbac(() => dataManager.UpdateUserData(UDType.Reward, 1));
        }
    }
}
