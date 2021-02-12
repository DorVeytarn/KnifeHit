using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class LevelCreator : MonoBehaviour
{
    [SerializeField] private LevelsDatabase database;
    [SerializeField] private int currentLevelNumber = 0;
    [SerializeField] private string currentLevelID;

    private KnifePool knifePool;
    private Target target;

    public Level CurrentLevel { get; private set; }

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(LevelCreator), this);
    }

    private void Start()
    {
        knifePool = SceneComponentProvider.GetComponent(typeof(KnifePool)) as KnifePool;
        target = SceneComponentProvider.GetComponent(typeof(Target)) as Target;

        CreateLevel();
    }

    public void CreateLevel()
    {
        CurrentLevel = database.Levels[currentLevelNumber];
        currentLevelID = CurrentLevel.ID;

        target.SetTarget(CurrentLevel.RotationCurve, CurrentLevel.Material, CurrentLevel.RewardItemsPositions, CurrentLevel.ObstaclesItemsPositions);

        knifePool.CreateItems(CurrentLevel.RequiredKnifeAmount);
        knifePool.SetFirstKnive();

        currentLevelNumber++;
    }
}
