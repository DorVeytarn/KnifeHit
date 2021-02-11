using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Level
{
    public string ID;
    public bool IsBossLevel;

    [Header("Settings")]
    public int RequiredKnifeAmount;
    public AnimationCurve RotationCurve;
    public int ObstaclesItemsAmount;
    public int RewardItemsAmount;

    [Header("View")]
    public Material Material;

    [Header("If Is Boss")]
    public string rewardKnifeID;
}
