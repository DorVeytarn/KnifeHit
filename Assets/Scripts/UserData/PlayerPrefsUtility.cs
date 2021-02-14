using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class PlayerPrefsUtility
{
    public const string userDataKey = "USER";

    public static void SaveSetting(string settingName, bool value)
    {
        PlayerPrefs.SetInt(settingName, value ? 1 : 0);
    }

    public static bool GetSetting(string settingName)
    {
        if (PlayerPrefs.HasKey(settingName))
            return PlayerPrefs.GetInt(settingName) == 1;

        return false;
    }

    public static void SaveUserData(UserData user)
    {
        string jsonData = JsonUtility.ToJson(user);

        PlayerPrefs.SetString(userDataKey, jsonData);
        PlayerPrefs.Save();
    }

    public static UserData LoadUserData()
    {
        if (!PlayerPrefs.HasKey(userDataKey))
        {
            UserData newUserData = new UserData();
            SaveUserData(newUserData);
        }

        string jsonData = PlayerPrefs.GetString(userDataKey);

        return JsonUtility.FromJson<UserData>(jsonData);
    }

    public static void SaveUserDataField(int rewardAmount, int hightScore, string openedKnife)
    {
        UserData newUserData;

        if (!PlayerPrefs.HasKey(userDataKey))
            newUserData = new UserData();
        else
        {
            string loadedData = PlayerPrefs.GetString(userDataKey);
            newUserData = JsonUtility.FromJson<UserData>(loadedData);
        }

        newUserData.RewardAmount = rewardAmount;
        newUserData.HightScore = hightScore;

        if(!string.IsNullOrEmpty(openedKnife))
            newUserData.OpenedKnives.Add(openedKnife);

        string userToJson = JsonUtility.ToJson(newUserData);

        PlayerPrefs.SetString(userDataKey, userToJson);
        PlayerPrefs.Save();
    }

    public static void SaveUserDataField(int rewardAmount, int hightScore, List<string> openedKnives)
    {
        UserData newUserData;

        if (!PlayerPrefs.HasKey(userDataKey))
            newUserData = new UserData();
        else
        {
            string loadedData = PlayerPrefs.GetString(userDataKey);
            newUserData = JsonUtility.FromJson<UserData>(loadedData);
        }

        newUserData.RewardAmount = rewardAmount;
        newUserData.HightScore = hightScore;

        newUserData.OpenedKnives = openedKnives;

        string userToJson = JsonUtility.ToJson(newUserData);

        PlayerPrefs.SetString(userDataKey, userToJson);
        PlayerPrefs.Save();
    }

#if UNITY_EDITOR
    [UnityEditor.MenuItem("Tools/Clear ALL Prefs")]
    public static void ClearPlayerPrefs()
    {
        PlayerPrefs.DeleteAll();
    }
#endif
}
