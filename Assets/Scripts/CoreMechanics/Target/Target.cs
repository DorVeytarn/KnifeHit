using System.Collections;
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

    private void Start()
    {
        //rewardsPool = SceneComponentProvider.GetComponent(typeof(RewardItemsPool)) as RewardItemsPool;
        //obstaclesPool = SceneComponentProvider.GetComponent(typeof(KnifePool)) as KnifePool;
    }

    public void SetTarget(AnimationCurve rotationCurve, Material targetMaterial, Vector2[] rewardItemsPositions, Vector2[] obstaclesItemsPositions)
    {
        meshRenderer.material = targetMaterial;

        rewardsPool.SetItemsAtPosition(rewardItemsPositions);
        obstaclesPool.SetItemsAtPosition(obstaclesItemsPositions);

        selfAnimator.SetTrigger("create");
        rotation.SetCurve(rotationCurve);
    }
}
