using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetRotation : MonoBehaviour
{
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private WrapMode curveMode;

    private float currentTime;
    private Coroutine rotationCoroutine;

    public void SetCurve(AnimationCurve rotationCurve, WrapMode curveMode = WrapMode.Loop)
    {
        this.rotationCurve = rotationCurve;
        rotationCurve.postWrapMode = curveMode;

        StopRotation();
        rotationCoroutine = StartCoroutine(StartRotation());
    }

    public void StopRotation()
    {
        if(rotationCoroutine != null)
        {
            StopCoroutine(rotationCoroutine);
            rotationCoroutine = null;
        }
    }

    private IEnumerator StartRotation()
    {
        while (true)
        {
            transform.rotation = Quaternion.AngleAxis(rotationCurve.Evaluate(currentTime), Vector3.forward);
            currentTime += Time.deltaTime;

            yield return null;
        }
    }
}
