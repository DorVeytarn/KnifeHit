using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class Target : MonoBehaviour
{
    public const int defaultRewardsAmount = 10;
    public const int defaultObstacleAmount = 10;
    public const string createTriggerName = "create";
    public const string deathTriggerName = "death";

    [Header("Items")]
    [SerializeField] private RewardItemsPool rewardsPool;
    [SerializeField] private ObstacleItemsPool obstaclesPool;

    [Header("View")]
    [SerializeField] private Image targetImage;
    [SerializeField] private Animator selfAnimator;

    [Header("Settings")]
    [SerializeField] private TargetRotation rotation;
    [SerializeField] private GameObject launchKnivesPoint;

    public GameObject LaunchKnivesPoint => launchKnivesPoint;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(Target), this);

        rewardsPool.CreateItems(defaultRewardsAmount);
        obstaclesPool.CreateItems(defaultObstacleAmount);
    }

    private IEnumerator WaitTransitionEnd(Action completeCallback)
    {
        yield return null;

        while (selfAnimator.IsInTransition(0))
            yield return null;

        completeCallback?.Invoke();
    }

    public void SetTarget(AnimationCurve rotationCurve, Sprite targetSprite, List<Vector2> rewardItemsPositions, List<Vector2> obstaclesItemsPositions)
    {
        targetImage.sprite = targetSprite;

        rewardsPool.SetItemsAtPosition(rewardItemsPositions);
        obstaclesPool.SetItemsAtPosition(obstaclesItemsPositions);

        selfAnimator.SetTrigger(createTriggerName);
        rotation.SetCurve(rotationCurve);
    }

    public void AnimatableDestroy(Action completeCallback)
    {
        selfAnimator.SetTrigger(deathTriggerName);
        StartCoroutine(WaitTransitionEnd(completeCallback));
    }



    public void ClearTarget()
    {
        targetImage.sprite = null;

        rewardsPool.ReturnItems();
        obstaclesPool.ReturnItems();

        rotation.StopRotation();
    }
}
