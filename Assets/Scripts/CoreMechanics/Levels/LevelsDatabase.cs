using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New LevelDatabase", menuName = "LevelDatabase", order = 1)]
public class LevelsDatabase : ScriptableObject
{
    [SerializeField] private Level[] levels;

    public Level[] Levels => levels;
    public int LevelsAmount => levels.Length;
}
