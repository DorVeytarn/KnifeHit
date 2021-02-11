using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using utils.singletone;

public class LevelCycle : MonoBehaviour
{
    private KnifeLauncher knifeLauncher;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(LevelCycle), this);
    }

    private void Start()
    {
        knifeLauncher = SceneComponentProvider.GetComponent(typeof(KnifeLauncher)) as KnifeLauncher;

        if(knifeLauncher != null)
        {
            knifeLauncher.KnifeClashed += LevelFailed;
            knifeLauncher.LastKnifeFinished += LevelPassed;
        }
    }

    private void OnDestroy()
    {
        if (knifeLauncher != null)
        {
            knifeLauncher.KnifeClashed -= LevelFailed;
            knifeLauncher.LastKnifeFinished -= LevelPassed;
        }
    }

    private void LevelPassed()
    {
        Debug.Log("LevelPassed");
    }

    private void LevelFailed()
    {
        Debug.Log("LevelFailed");
    }
}
