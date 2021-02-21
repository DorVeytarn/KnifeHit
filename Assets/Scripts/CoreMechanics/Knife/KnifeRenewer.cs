using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class KnifeRenewer : MonoBehaviour
{
    [SerializeField] private KnivesDatabase knivesDatabase;

    private KnifePool knifePool;
    private UserDataManager userDataManager;
    private LevelCycle levelCycle;

    public UserDataManager UserDataManager => userDataManager;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(KnifeRenewer), this);
    }

    private void Start()
    {
        knifePool = SceneComponentProvider.GetComponent(typeof(KnifePool)) as KnifePool;
        userDataManager = SceneComponentProvider.GetComponent(typeof(UserDataManager)) as UserDataManager;
        levelCycle = SceneComponentProvider.GetComponent(typeof(LevelCycle)) as LevelCycle;
    }

    public void SetNewKnife(KnifeData knifeData, bool setImmediately)
    {
        if (knifeData == null)
            return;

        userDataManager.UpdateUserData(UDType.Knife, knifeData);

        if (setImmediately)
            knifePool.SetNewSpawnObject(knifeData.Model);
        else
            levelCycle.Failed += () => knifePool.SetNewSpawnObject(knifeData.Model);
    }

    public void SetNewKnife(string knifeName, bool setImmediately)
    {
        KnifeData newKnifeData = null;

        for (int i = 0; i < knivesDatabase.Knives.Count; i++)
        {
            if (knivesDatabase.Knives[i].Name == knifeName)
                newKnifeData = knivesDatabase.Knives[i];
        }

        SetNewKnife(newKnifeData, setImmediately);
    }

    public KnifeData GetKnifeData(string knifeName)
    {
        for (int i = 0; i < knivesDatabase.Knives.Count; i++)
        {
            if (knivesDatabase.Knives[i].Name == knifeName)
                return knivesDatabase.Knives[i];
        }

        return null;
    }
}
