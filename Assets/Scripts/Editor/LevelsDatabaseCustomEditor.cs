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
    //private AnimBool animBool;

    private void OnEnable()
    {
        database = (LevelsDatabase)target;
        levels = database.Levels;
    }

    public override void OnInspectorGUI()
    {
        for (int i = 0; i < levels.Count; i++)
        {
            var level = levels[i];

            level.ID = EditorGUILayout.TextField("ID", level.ID);

            level.RequiredKnifeAmount = EditorGUILayout.IntField("Required Knife Amount", level.RequiredKnifeAmount);

            level.RotationCurve = EditorGUILayout.CurveField("Rotation Curve", level.RotationCurve);

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

                level.RewardItemsPositions = new Vector2[level.RewardItemsAmount];

                if (level.RewardItemsAmount != 0)
                {
                    for (int j = 0; j < level.RewardItemsPositions.Length; j++)
                    {
                        level.RewardItemsPositions[j] = EditorGUILayout.Vector2Field(j.ToString() + " Position", level.RewardItemsPositions[j]);
                    }
                }
            }

            if (level.RandomObstacle = EditorGUILayout.Toggle("Random Obstacle", level.RandomObstacle))
            {
                level.RandomObstacleRange = EditorGUILayout.Vector2IntField("Range", level.RandomObstacleRange);
                level.RandomObstacleChance = EditorGUILayout.Slider("Chance", level.RandomObstacleChance, 0f, 1f);
            }
            else
            {
                level.ObstaclesItemsAmount = EditorGUILayout.IntField("Obstacle Amount", level.ObstaclesItemsAmount);
                if (level.ObstaclesItemsAmount < 0)
                    level.ObstaclesItemsAmount = 0;

                level.ObstaclesItemsPositions = new Vector2[level.ObstaclesItemsAmount];

                if (level.ObstaclesItemsAmount != 0)
                {
                    for (int j = 0; j < level.ObstaclesItemsPositions.Length; j++)
                    {
                        level.ObstaclesItemsPositions[j] = EditorGUILayout.Vector2Field(j.ToString() + " Position", level.ObstaclesItemsPositions[j]);
                    }
                }
            }

            if(level.IsBossLevel = EditorGUILayout.Toggle("Is Boss", level.IsBossLevel))
            {
                level.rewardKnifeID = EditorGUILayout.TextField("Reward Knife", level.rewardKnifeID);
            }

            GUILayout.Space(7);
        }

        if(GUILayout.Button("Add Level"))
        {
            levels.Add(new Level());
        }
    }
}
