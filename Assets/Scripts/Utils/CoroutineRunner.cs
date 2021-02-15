using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CoroutineRunner : MonoBehaviour
{
    private class DelayedCallInfo
    {
        public Action action;
        public Coroutine coroutine;
    }

    public static CoroutineRunner Instance
    {
        get
        {
            if (self == null)
            {
                self = InstantiateCoroutineRunner();
            }
            return self;
        }
    }

    public static CoroutineRunner ImmortalInstance
    {
        get
        {
            if (immortalSelf == null)
            {
                immortalSelf = InstantiateCoroutineRunner();
                DontDestroyOnLoad(immortalSelf.gameObject);
            }
            return immortalSelf;
        }
    }

    private static CoroutineRunner self;
    private static CoroutineRunner immortalSelf;

    private List<DelayedCallInfo> delayedCallsList = new List<DelayedCallInfo>();

    private static CoroutineRunner InstantiateCoroutineRunner()
    {
        var go = new GameObject("CoroutineRunner");
        return go.AddComponent<CoroutineRunner>();
    }

    private void OnDestroy()
    {
        if (self == this)
            self = null;
    }

    public void RunCoroutine(IEnumerator action)
    {
        StartCoroutine(action);
    }

    public void WaitFewFramesAndDo(Action action, int frameCount = 1)
    {
        StartCoroutine(WaitCoroutine(action, frameCount));
    }

    public void WaitForEndOfFrameAndDo(Action action)
    {
        StartCoroutine(WaitEndOfFrameCoroutine(action));
    }

    public void DelayedCall(Action action, float delay, bool ignoreTimeScale)
    {
        DelayedCallInfo newDelayedCall = new DelayedCallInfo();
        newDelayedCall.action = action;
        delayedCallsList.Add(newDelayedCall);
        newDelayedCall.coroutine = StartCoroutine(DelayedCallCoroutine(newDelayedCall, delay, ignoreTimeScale));
    }

    public void DelayedCall(Action action, int framesDelay)
    {
        DelayedCallInfo newDelayedCall = new DelayedCallInfo();
        newDelayedCall.action = action;
        delayedCallsList.Add(newDelayedCall);
        newDelayedCall.coroutine = StartCoroutine(DelayedCallCoroutine(newDelayedCall, framesDelay));
    }

    public void CancelDelayedCall(Action action)
    {
        for (int i = delayedCallsList.Count - 1; i >= 0; i--)
            if (delayedCallsList[i].action == action)
            {
                StopCoroutine(delayedCallsList[i].coroutine);
                delayedCallsList.RemoveAt(i);
            }
    }

    private IEnumerator WaitCoroutine(Action action, int frameCount)
    {
        while (frameCount > 0)
        {
            yield return null;
            frameCount--;
        }

        action();
    }

    private IEnumerator WaitEndOfFrameCoroutine(Action action)
    {
        yield return new WaitForEndOfFrame();
        action();
    }

    private IEnumerator DelayedCallCoroutine(DelayedCallInfo delayedCall, float delay, bool ignoreTimeScale)
    {
        if (ignoreTimeScale)
            yield return new WaitForSecondsRealtime(delay);
        else
            yield return new WaitForSeconds(delay);

        if (delayedCall.action != null) delayedCall.action();
        delayedCallsList.Remove(delayedCall);
    }

    private IEnumerator DelayedCallCoroutine(DelayedCallInfo delayedCall, int framesDelay)
    {
        while (framesDelay > 0)
        {
            yield return null;
            framesDelay--;
        }

        if (delayedCall.action != null) delayedCall.action();
        delayedCallsList.Remove(delayedCall);
    }
}
