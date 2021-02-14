using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;


public class SettingsManager : MonoBehaviour
{
    public const string soundSetting = "Sounds";
    public const string vibrationSetting = "Vibrations";

    public static bool IsMute { get; private set; }
    public static bool IsVibrationMode { get; private set; }

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(SettingsManager), this);
        Vibration.Init();

        UpdateSettingsValues();
    }

    public void UpdateSettingsValues()
    {
        IsMute = PlayerPrefsUtility.GetSetting(soundSetting);
        IsMute = PlayerPrefsUtility.GetSetting(vibrationSetting);
    }
}
