using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class ObjectRotator : MonoBehaviour
{
    [Range(0f, Mathf.PI *2)]
    [SerializeField] private float angle;
    [SerializeField] private float radius;
    [SerializeField] private Transform otherTransform;
    [SerializeField] private float distance;

    private void Update()
    {
        float angleZ = (transform.position.x > 0) ? Vector3.Angle(transform.parent.position - transform.position, Vector3.up)
                                                : -Vector3.Angle(transform.parent.position - transform.position, Vector3.up);
        transform.rotation = Quaternion.Euler(0, 0, angleZ);

        transform.localPosition = new Vector3(Mathf.Cos(angle) * radius,Mathf.Sin(angle) * radius, 0);
    }

    private void OnDrawGizmos()
    {
        if (otherTransform == null)
            return;

        Gizmos.DrawLine(transform.position, otherTransform.position);
        distance = (otherTransform.position - transform.position).magnitude;
    }
}
