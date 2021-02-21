using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class LevelCreator : MonoBehaviour
{
    private const float rewardSpawnRadius = 310;
    private const float rewardPermissibleDistance = 1f;

    [SerializeField] private float delayBeforeCreating;
    [SerializeField] private LevelsDatabase database;
    [SerializeField] private int currentLevelNumber = 0;
    [SerializeField] private string currentLevelID;

    private KnifePool knifePool;
    public System.Action LevelCreated;

    public Level CurrentLevel { get; private set; }
    public Target Target { get; private set; }

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(LevelCreator), this);
    }

    private void Start()
    {
        knifePool = SceneComponentProvider.GetComponent(typeof(KnifePool)) as KnifePool;
        Target = SceneComponentProvider.GetComponent(typeof(Target)) as Target;
    }

    public void DestroyLevel(System.Action destroyedCallback)
    {
        Target.AnimatableDestroy(destroyedCallback, delayBeforeCreating);
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

        LevelCreated?.Invoke();

    }

    private void SetLevelSettings(Level level)
    {
        if (level.DymamicObstaclesRandom)
        {
            level.ObstaclesItemsPositions = database.ObstaclePresets.GetPreset(level.RandomObstacleRange.x, level.RandomObstacleRange.y);
            level.ObstaclesItemsAmount = level.ObstaclesItemsPositions.Count;
        }

        if (level.RandomReward)
        {
            level.RewardItemsAmount = Random.Range(level.RandomRewardRange.x, level.RandomRewardRange.x + 1);
            level.RewardItemsPositions.Clear();

            if (Random.Range(0f, 1f) < level.RandomRewardChance)
                for (int i = 0; i < level.RewardItemsAmount; i++)
                {
                    Vector2 newPosition;

                    do
                    {
                        float angle = Random.Range(0f, Mathf.PI * 2);
                        newPosition = new Vector2(Mathf.Cos(angle) * rewardSpawnRadius, Mathf.Sin(angle) * rewardSpawnRadius);
                    }
                    while (CheckRewardDistance(newPosition, level.ObstaclesItemsPositions));

                    level.RewardItemsPositions.Add(newPosition);
                }
        }

        Target.SetTarget(level.RotationCurve, level.Sprite, level.RewardItemsPositions, level.ObstaclesItemsPositions, level.BumpClip, level.CrashClip);

        knifePool.CreateItems(level.RequiredKnifeAmount);
        knifePool.SetFirstKnive();
    }

    private bool CheckRewardDistance(Vector2 positionToCheck, List<Vector2> otherPositions)
    {
        bool result = true;

        for (int i = 0; i < otherPositions.Count; i++)
            result = (positionToCheck - otherPositions[i]).magnitude >= rewardPermissibleDistance;

        return !result;
    }
}
