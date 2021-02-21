using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utils.Singletone;


public class SettingsManager : MonoBehaviour
{
    public const string soundSetting = "Sounds";
    public const string vibrationSetting = "Vibrations";

    public static bool IsSoundMode { get; private set; }
    public static bool IsVibrationMode { get; private set; }

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(SettingsManager), this);
        Vibration.Init();

        UpdateSettingsValues();
    }

    public bool GetSettingState(string settingName)
    {
        UpdateSettingsValues();

        switch (settingName)
        {
            case soundSetting:
                return IsSoundMode;

            case vibrationSetting:
                return IsVibrationMode;

            default:
                return false;
        }
    }

    public void UpdateSettingsValues()
    {
        IsSoundMode = PlayerPrefsUtility.GetSetting(soundSetting);
        IsVibrationMode = PlayerPrefsUtility.GetSetting(vibrationSetting);
    }
}
