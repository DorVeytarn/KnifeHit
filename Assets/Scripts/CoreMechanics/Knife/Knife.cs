using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class Knife : MonoBehaviour
{
    private Coroutine flyingCoroutine;
    private float speed;
    private float offset;
    private bool isFlying = false;

    private Action succsesFinished;
    private Action failure;

    public void SetAndLaunchKnife(Transform targetParent, float speed, float offset, Action succsesCallback = null, Action failureCallback = null)
    {
        this.speed = speed;
        this.offset = offset;

        succsesFinished = succsesCallback;
        failure = failureCallback;

        flyingCoroutine = StartCoroutine(Flying(targetParent));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(isFlying && collision.TryGetComponent(out Knife knife))
        {
            //if(flyingCoroutine != null)
            //{
            //    StopCoroutine(flyingCoroutine);
            //    flyingCoroutine = null;
            //}

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

        Vector3 targetPosition = targetParent.transform.position;
        targetPosition.y = targetParent.transform.position.y - offset;

        transform.SetPositionAndRotation(targetPosition, Quaternion.identity);
        transform.SetParent(targetParent.transform);

        SuccessfulFinished();
    }

    private void SuccessfulFinished()
    {
        Debug.Log("SuccessfulFinished");
        isFlying = false;
        succsesFinished?.Invoke();
    }

    private void KnifeClash()
    {
        Debug.Log("KnifeClash");
        failure?.Invoke();
    }
}
