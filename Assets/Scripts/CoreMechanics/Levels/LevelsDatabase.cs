using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelDatabase", menuName = "LevelDatabase", order = 1)]
public class LevelsDatabase : ScriptableObject
{
    [SerializeField] private List<Level> levels;

    public ObstaclePresets ObstaclePresets;

    public List<Level> Levels => levels;
    public int LevelsAmount => levels.Count;
}
