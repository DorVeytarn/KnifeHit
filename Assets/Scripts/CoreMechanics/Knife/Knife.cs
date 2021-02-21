﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    [SerializeField] private BoxCollider2D selfBoxCollider;
    [SerializeField] private GameObject effect;

    private Coroutine flyingCoroutine;
    private float speed;
    private float offset;
    private bool isFlying = false;

    private Action successFinished;
    private Action failure;
    private Action additionalSuccessCallback;

    public void SetAndLaunchKnife(Transform targetParent, float speed, float offset, Action succsesCallback = null, Action failureCallback = null)
    {
        effect.SetActive(false);

        isFlying = false;

        this.speed = speed;
        this.offset = offset;

        successFinished = succsesCallback;
        failure = failureCallback;

        flyingCoroutine = StartCoroutine(Flying(targetParent));
    }

    public void AddSuccessCallback(Action callback)
    {
        additionalSuccessCallback = callback;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isFlying && collision.TryGetComponent(out Knife knife) || collision.TryGetComponent(out ObstacleItem obstacle))
        {
            if (flyingCoroutine != null)
            {
                StopCoroutine(flyingCoroutine);
                flyingCoroutine = null;
            }

            KnifeClash();
        }
    }

    private IEnumerator Flying(Transform targetParent)
    {
        while (transform.position.y <= targetParent.transform.position.y - offset)
        {
            yield return null;

            if(transform.position.y >= (transform.position - targetParent.transform.position).y / 2)
                isFlying = true;

            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
        isFlying = false;

        Vector3 targetPosition = targetParent.transform.position;
        targetPosition.y = targetParent.transform.position.y - offset;

        transform.SetPositionAndRotation(targetPosition, Quaternion.identity);
        transform.SetParent(targetParent.transform);

        SuccessfulFinished();
    }

    private void SuccessfulFinished()
    {
        Vibration.VibratePop();

        effect.SetActive(true);

        successFinished?.Invoke();
        additionalSuccessCallback?.Invoke();

        additionalSuccessCallback = null;
    }

    private void KnifeClash()
    {
        Vibration.VibratePop();

        isFlying = false;
        failure?.Invoke();

        additionalSuccessCallback = null;
    }
}
