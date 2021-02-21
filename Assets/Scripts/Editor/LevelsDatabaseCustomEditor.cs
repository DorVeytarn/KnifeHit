using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

[CustomEditor(typeof(LevelsDatabase))]
public class LevelsDatabaseCustomEditor : Editor
{
    private LevelsDatabase database;
    private List<Level> levels;
    private SerializedProperty obstaclePresets;
    private ObstaclePresets presets;

    private void OnEnable()
    {
        database = (LevelsDatabase)target;
        levels = database.Levels;
        obstaclePresets = serializedObject.FindProperty("ObstaclePresets");
    }

    public override void OnInspectorGUI()
    {
        EditorGUILayout.PropertyField(obstaclePresets);
        serializedObject.ApplyModifiedProperties();

        presets = obstaclePresets.objectReferenceValue as ObstaclePresets;

        for (int i = 0; i < levels.Count; i++)
        {
            var level = levels[i];

            string idLabel = (i + 1).ToString();

            GUILayout.BeginHorizontal();
            GUILayout.Label("--------------------------Level " + idLabel + "--------------------------", EditorStyles.boldLabel);
            if (GUILayout.Button("X"))
            {
                levels.RemoveAt(i);
                break;
            }
            GUILayout.EndHorizontal();

            level.ID = EditorGUILayout.TextField("ID", level.ID);

            level.RequiredKnifeAmount = EditorGUILayout.IntField("Required Knife Amount", level.RequiredKnifeAmount);

            level.RotationCurve = EditorGUILayout.CurveField("Rotation Curve", level.RotationCurve);
            level.Sprite = (Sprite)EditorGUILayout.ObjectField("Sprite", level.Sprite, typeof(Sprite), false);
            level.BumpClip = (AudioClip)EditorGUILayout.ObjectField("Bump Clip", level.BumpClip, typeof(AudioClip), false);
            level.CrashClip = (AudioClip)EditorGUILayout.ObjectField("Crash Clip", level.CrashClip, typeof(AudioClip), false);

            GUILayout.Space(4);
            GUILayout.Label("--------------------------Reward Items--------------------------");
            if (level.RandomReward = EditorGUILayout.Toggle("Random Reward", level.RandomReward))
            {
                level.RandomRewardRange = EditorGUILayout.Vector2IntField("Range", level.RandomRewardRange);
                level.RandomRewardChance = EditorGUILayout.Slider("Chance", level.RandomRewardChance, 0f, 1f);
            }
            else
            {
                level.RewardItemsAmount = EditorGUILayout.IntField("Reward Amount", level.RewardItemsAmount);
                if (level.RewardItemsAmount < 0)
                    level.RewardItemsAmount = 0;

                level.RewardItemsPositions = new List<Vector2>(level.RewardItemsAmount);

                if (level.RewardItemsAmount != 0)
                    for (int j = 0; j < level.RewardItemsPositions.Count; j++)
                        level.RewardItemsPositions[j] = EditorGUILayout.Vector2Field(j.ToString() + " Position", level.RewardItemsPositions[j]);
            }

            GUILayout.Space(4);
            GUILayout.Label("--------------------------Obstacle Items--------------------------");
            if (level.RandomObstacle = EditorGUILayout.Toggle("Random Obstacle", level.RandomObstacle))
            {
                level.RandomObstacleRange = EditorGUILayout.Vector2IntField("Range", level.RandomObstacleRange);

                if (!(level.DymamicObstaclesRandom = EditorGUILayout.Toggle("Dymamic Random", level.DymamicObstaclesRandom)))
                {
                    if (GUILayout.Button("Randomaze!"))
                    {
                        level.ObstaclesItemsPositions = presets.GetPreset(level.RandomObstacleRange.x, level.RandomObstacleRange.y);
                        level.ObstaclesItemsAmount = level.ObstaclesItemsPositions.Count;
                    }

                    if (level.ObstaclesItemsAmount != 0)
                        for (int j = 0; j < level.ObstaclesItemsPositions.Count; j++)
                            level.ObstaclesItemsPositions[j] = EditorGUILayout.Vector2Field(j.ToString() + " Position", level.ObstaclesItemsPositions[j]);
                }
            }
            else
            {
                level.DymamicObstaclesRandom = false;
                level.ObstaclesItemsAmount = EditorGUILayout.IntSlider("Obstacle Amount", level.ObstaclesItemsAmount, 0, 15);

                if(level.ObstaclesItemsPositions == null)
                    level.ObstaclesItemsPositions = new List<Vector2>(level.ObstaclesItemsAmount);
                else
                {
                    if (level.ObstaclesItemsPositions.Count == level.ObstaclesItemsAmount)
                    {
                    }
                    else if (level.ObstaclesItemsPositions.Count >= level.ObstaclesItemsAmount)
                        level.ObstaclesItemsPositions.RemoveRange(level.ObstaclesItemsAmount, level.ObstaclesItemsPositions.Count - level.ObstaclesItemsAmount);
                    else
                        level.ObstaclesItemsPositions.Add(Vector2.zero);
                }

                if (level.ObstaclesItemsAmount < 0)
                    level.ObstaclesItemsAmount = 0;

                if (level.ObstaclesItemsAmount != 0)
                    for (int j = 0; j < level.ObstaclesItemsPositions.Count; j++)
                        level.ObstaclesItemsPositions[j] = EditorGUILayout.Vector2Field(j.ToString() + " Position", level.ObstaclesItemsPositions[j]);
            }

            if (level.IsBossLevel = EditorGUILayout.Toggle("Is Boss", level.IsBossLevel))
                level.RewardKnifeName = EditorGUILayout.TextField("Reward Knife", level.RewardKnifeName);

            GUILayout.Space(14);
        }

        if (GUILayout.Button("Add Level"))
            levels.Add(new Level());

        EditorUtility.SetDirty(target);
    }
}
