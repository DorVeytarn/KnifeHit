using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Rigidbody2D))]
public class RewardItem : MonoBehaviour
{
    [SerializeField] private Animator selfAnimator;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent(out Knife knife))
        {
            Debug.Log("GIVE APPLE");
        }
    }
}
