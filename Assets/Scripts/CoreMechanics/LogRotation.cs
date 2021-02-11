using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogRotation : MonoBehaviour
{
    [SerializeField] private AnimationCurve rotationCurve;
    [SerializeField] private WrapMode curveMode;

    private float currentTime;
    private Coroutine rotationCoroutine;

    private void Start()
    {
        rotationCurve.postWrapMode = curveMode;

        StartCoroutine(StartRotation());
    }

    public void SetCurve(AnimationCurve rotationCurve, WrapMode curveMode)
    {
        this.rotationCurve = rotationCurve;
        this.curveMode = curveMode;

        rotationCoroutine = StartCoroutine(StartRotation());
    }

    public void StopRotation()
    {
        StopCoroutine(rotationCoroutine);
        rotationCoroutine = null;
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
