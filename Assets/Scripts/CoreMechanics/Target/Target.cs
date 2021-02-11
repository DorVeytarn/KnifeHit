using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    [Header("View")]
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private Animator selfAnimator;

    [Header("Settings")]
    [SerializeField] private TargetRotation rotation;

    public void SetTarget(AnimationCurve rotationCurve, Material targetMaterial, int rewardItemsAmount, int obstacleItemsAmount)
    {
        meshRenderer.material = targetMaterial;

        Debug.Log("rewardItemsAmount: " + rewardItemsAmount + " obstacleItemsAmount: " + obstacleItemsAmount);

        selfAnimator.SetTrigger("create");
        rotation.SetCurve(rotationCurve);
    }
}
