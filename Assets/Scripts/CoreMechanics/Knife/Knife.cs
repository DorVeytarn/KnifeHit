using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    [SerializeField] private BoxCollider2D selfBoxCollider;

    private Coroutine flyingCoroutine;
    private float speed;
    private float offset;
    private bool isFlying = false;

    private Action succsesFinished;
    private Action failure;
    private Action additionalCallback;

    public void SetAndLaunchKnife(Transform targetParent, float speed, float offset, Action succsesCallback = null, Action failureCallback = null)
    {
        isFlying = false;

        this.speed = speed;
        this.offset = offset;

        succsesFinished = succsesCallback;
        failure = failureCallback;

        flyingCoroutine = StartCoroutine(Flying(targetParent));
    }

    public void AddSuccsesCallback(Action callback)
    {
        additionalCallback = callback;
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
        Vibration.Vibrate(100);

        succsesFinished?.Invoke();
        additionalCallback?.Invoke();
    }

    private void KnifeClash()
    {
        Vibration.Vibrate(500);

        isFlying = false;
        failure?.Invoke();
    }
}
