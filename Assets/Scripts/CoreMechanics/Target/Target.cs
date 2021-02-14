﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;

public class Target : MonoBehaviour
{
    public const int defaultRewardsAmount = 10;
    public const int defaultObstacleAmount = 10;

    [Header("Items")]
    [SerializeField] private RewardItemsPool rewardsPool;
    [SerializeField] private ObstacleItemsPool obstaclesPool;

    [Header("View")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Animator selfAnimator;

    [Header("Settings")]
    [SerializeField] private TargetRotation rotation;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(Target), this);

        rewardsPool.CreateItems(defaultRewardsAmount);
        obstaclesPool.CreateItems(defaultObstacleAmount);
    }

    public void SetTarget(AnimationCurve rotationCurve, Material targetMaterial, List<Vector2> rewardItemsPositions, List<Vector2> obstaclesItemsPositions)
    {
        meshRenderer.material = targetMaterial;

        rewardsPool.SetItemsAtPosition(rewardItemsPositions);
        obstaclesPool.SetItemsAtPosition(obstaclesItemsPositions);

        selfAnimator.SetTrigger("create");
        rotation.SetCurve(rotationCurve);
    }

    public void ClearTarget()
    {
        meshRenderer.material = null;

        rewardsPool.ReturnItems();
        obstaclesPool.ReturnItems();

        rotation.StopRotation();
    }
}
