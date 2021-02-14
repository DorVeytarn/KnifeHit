using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class RewardItem : MonoBehaviour
{
    [SerializeField] private Animator selfAnimator;

    private Action dyingCallback;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Knife knife))
        {
            dyingCallback?.Invoke();
            gameObject.SetActive(false);
        }
    }

    public void SetDyingCallbac(Action callback)
    {
        dyingCallback = callback;
    }
}
