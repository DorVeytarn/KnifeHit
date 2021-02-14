using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string ID;

    [Header("Settings")]
    public int RequiredKnifeAmount = 7;
    public AnimationCurve RotationCurve = AnimationCurve.Linear(0, 0, 10, 360);
    //AnimationCurve.Linear(0, 0, 10, 360);

    [Header("Obstacle")]
    public int ObstaclesItemsAmount;
    public List<Vector2> ObstaclesItemsPositions;
    public bool DymamicObstaclesRandom = true;
    public bool RandomObstacle = true;
    public Vector2Int RandomObstacleRange = new Vector2Int(1,4);

    [Header("Reward")]
    public int RewardItemsAmount;
    public List<Vector2> RewardItemsPositions;

    public bool RandomReward = true;
    public Vector2Int RandomRewardRange = new Vector2Int(1,2);
    public float RandomRewardChance = 0.25f;

    [Header("View")]
    public Material Material;

    [Header("If Is Boss")]
    public bool IsBossLevel;
    public string rewardKnifeID;
}
