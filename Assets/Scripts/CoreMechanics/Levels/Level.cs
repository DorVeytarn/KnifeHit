﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string ID;

    [Header("Settings")]
    public int RequiredKnifeAmount;
    public AnimationCurve RotationCurve;

    [Header("Obstacle")]
    public int ObstaclesItemsAmount;
    public Vector2[] ObstaclesItemsPositions;

    public bool RandomObstacle;
    public Vector2Int RandomObstacleRange;
    public float RandomObstacleChance;

    [Header("Reward")]
    public int RewardItemsAmount;
    public Vector2[] RewardItemsPositions;

    public bool RandomReward;
    public Vector2Int RandomRewardRange;
    public float RandomRewardChance;

    [Header("View")]
    public Material Material;

    [Header("If Is Boss")]
    public string rewardKnifeID;
    public bool IsBossLevel;
}
