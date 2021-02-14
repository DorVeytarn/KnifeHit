using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;


public class Setting : MonoBehaviour
{
    [SerializeField] private string settingName;
    [SerializeField] private Toggle selfToggle;

    private SettingsManager settingsManager;

    public string SettingName => settingName;

    private void Start()
    {
        settingsManager = SceneComponentProvider.GetComponent(typeof(SettingsManager)) as SettingsManager;
        selfToggle.onValueChanged.AddListener(OnToggleValueChanged);
    }

    private void OnDestroy()
    {
        selfToggle.onValueChanged.RemoveListener(OnToggleValueChanged);
    }

    private void OnToggleValueChanged(bool value)
    {
        PlayerPrefsUtility.SaveSetting(settingName, value);
        settingsManager.UpdateSettingsValues();
    }
}
