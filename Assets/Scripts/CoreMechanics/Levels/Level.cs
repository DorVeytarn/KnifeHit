using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string ID;

    [Header("Settings")]
    public int RequiredKnifeAmount;
    public AnimationCurve RotationCurve;

    [Header("Items")]
    public Vector2[] ObstaclesItemsPositions;
    public Vector2[] RewardItemsPositions;

    [Header("View")]
    public Material Material;

    [Header("If Is Boss")]
    public string rewardKnifeID;
    public bool IsBossLevel;
}
