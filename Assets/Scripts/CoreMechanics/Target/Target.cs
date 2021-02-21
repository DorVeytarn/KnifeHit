using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnitySpriteCutter;
using Utils.Singletone;

public class Target : MonoBehaviour
{
    public const int defaultRewardsAmount = 10;
    public const int defaultObstacleAmount = 10;
    public const string createTriggerName = "create";
    public const string deathTriggerName = "death";
    public const string bumpTriggerName = "bump";

    [Header("Items")]
    [SerializeField] private RewardItemsPool rewardsPool;
    [SerializeField] private ObstacleItemsPool obstaclesPool;

    [Header("View")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private SpriteRenderer targetSpriteRenderer;
    [SerializeField] private Animator selfAnimator;
    [SerializeField] private Sprite defaultSprite;
    [SerializeField] private GameObject model;

    [Header("Settings")]
    [SerializeField] private TargetRotator rotation;
    [SerializeField] private GameObject launchKnivesPoint;
    [SerializeField] private List<GameObject> dependentObjects = new List<GameObject>();

    [Header("Audio")]
    [SerializeField] private AudioClip bumpClip;
    [SerializeField] private AudioClip crashClip;

    public GameObject LaunchKnivesPoint => launchKnivesPoint;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(Target), this);

        rewardsPool.CreateItems(defaultRewardsAmount);
        obstaclesPool.CreateItems(defaultObstacleAmount);
    }

    public void SetTarget(AnimationCurve rotationCurve, Sprite targetSprite, List<Vector2> rewardItemsPositions, List<Vector2> obstaclesItemsPositions)
    {
        SetDependentObjectsActive(true);

        if (targetSprite != defaultSprite)
            targetSpriteRenderer.sprite = targetSprite;

        rewardsPool.SetItemsAtPosition(rewardItemsPositions);
        obstaclesPool.SetItemsAtPosition(obstaclesItemsPositions);

        selfAnimator.SetTrigger(createTriggerName);
        rotation.SetCurve(rotationCurve);
    }

    public void AnimatableDestroy(Action completeCallback, float delay = 0)
    {
        StartCoroutine(CutsModel(delay, completeCallback));
    }

    private IEnumerator CutsModel(float time, Action completeCallback)
    {
        Vector2 cutLine = model.transform.position;

        SpriteCutterOutput outputUpDown = SpriteCutter.Cut(new SpriteCutterInput(model, cutLine, Vector2.down * 1000, CutterMode.CUT_INTO_TWO, true));

        SpriteCutterOutput outputLeft = SpriteCutter.Cut(new SpriteCutterInput(outputUpDown.firstSideGameObject, cutLine, Vector2.right * 1000, CutterMode.CUT_INTO_TWO, true));
        SpriteCutterOutput outputRight = SpriteCutter.Cut(new SpriteCutterInput(outputUpDown.secondSideGameObject, cutLine, Vector2.right * 1000, CutterMode.CUT_INTO_TWO, true));

        SetDependentObjectsActive(false);

        outputUpDown.firstSideGameObject.SetActive(false);
        outputUpDown.secondSideGameObject.SetActive(false);

        List<GameObject> cutsModels = new List<GameObject>
        {
            outputUpDown.firstSideGameObject,
            outputUpDown.secondSideGameObject,
            outputLeft.firstSideGameObject,
            outputLeft.secondSideGameObject,
            outputRight.firstSideGameObject,
            outputRight.secondSideGameObject
        };

        SetCutsModel(cutsModels);

        yield return new WaitForSeconds(time);
        completeCallback?.Invoke();

        completeCallback = null;
    }

    private void SetCutsModel(List<GameObject> cutModels)
    {
        if (SettingsManager.IsSoundMode)
            audioSource.PlayOneShot(crashClip);

        for (int i = 0; i < cutModels.Count; i++)
        {
            var newCut = cutModels[i];
            newCut.transform.SetParent(transform, false);

            var cutRigidbody = newCut.AddComponent<Rigidbody2D>();
            cutRigidbody.gravityScale = 0.1f;
            cutRigidbody.angularVelocity = 10f;
            cutRigidbody.AddForce(new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), -10), ForceMode2D.Impulse);

            CoroutineRunner.Instance.DelayedCall(() => Destroy(newCut), 1f, true);
        }

    }

    public void BumpTarget()
    {
        selfAnimator.SetTrigger(bumpTriggerName);

        if(SettingsManager.IsSoundMode)
            audioSource.PlayOneShot(bumpClip);
    }

    public void ClearTarget()
    {
        targetSpriteRenderer.sprite = defaultSprite;

        rewardsPool.ReturnItems();
        obstaclesPool.ReturnItems();

        rotation.StopRotation();
    }

    private void SetDependentObjectsActive(bool active)
    {
        for (int i = 0; i < dependentObjects.Count; i++)
        {
            dependentObjects[i].SetActive(active);
        }
    }

}
