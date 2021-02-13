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
    }

    public void CreateLevel(bool isNewGame = false)
    {
        if (isNewGame)
            currentLevelNumber = 0;

        CurrentLevel = database.Levels[currentLevelNumber];
        currentLevelID = CurrentLevel.ID;

        if (CurrentLevel.IsBossLevel)
            PopupManager.Instance.OpenPopup(PopupList.BossFight, null, () => SetLevelSettings(CurrentLevel));
        else
            SetLevelSettings(CurrentLevel);

        currentLevelNumber++;

        if (currentLevelNumber >= database.Levels.Count)
            currentLevelNumber = Random.Range(0, database.Levels.Count - 1);
    }

    private void SetLevelSettings(Level level)
    {
        target.SetTarget(level.RotationCurve, level.Material, level.RewardItemsPositions, level.ObstaclesItemsPositions);

        knifePool.CreateItems(level.RequiredKnifeAmount);
        knifePool.SetFirstKnive();
    }
}
