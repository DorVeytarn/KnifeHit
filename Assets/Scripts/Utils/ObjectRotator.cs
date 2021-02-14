using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectRotator : MonoBehaviour
{
    [Range(0f, Mathf.PI *2)]
    [SerializeField] private float angle;
    [SerializeField] private float radius;

    private void Update()
    {
        Vector3 supportingVector = (transform.position.x > 0) ? Vector3.up : Vector3.down;
        transform.rotation = Quaternion.Euler(0, 0, Vector3.Angle(transform.parent.position - transform.position, supportingVector));

        transform.localPosition = new Vector3(Mathf.Cos(angle) * radius,Mathf.Sin(angle) * radius, 0);
    }
}
