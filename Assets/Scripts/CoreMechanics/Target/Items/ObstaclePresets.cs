using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionPreset
{
    public int currentPresetsAmount;
    public List<Vector2> preset = new List<Vector2>();

    public PositionPreset(int currentPresetsAmount)
    {
        this.currentPresetsAmount = currentPresetsAmount;

        for (int i = 0; i < currentPresetsAmount; i++)
        {
            preset.Add(Vector2.zero);
        }
    }
}

[CreateAssetMenu(fileName = "New ObstaclePresets", menuName = "ObstaclePresets", order = 1)]
public class ObstaclePresets : ScriptableObject
{
    public List<PositionPreset> OneObstaclePresets = new List<PositionPreset>();
    public List<PositionPreset> TwoObstaclePresets = new List<PositionPreset>();
    public List<PositionPreset> ThreeObstaclePresets = new List<PositionPreset>();
    public List<PositionPreset> FoureObstaclePresets = new List<PositionPreset>();


    public List<Vector2> GetPreset(int minObstacleInPreset, int maxObstacleInPreset)
    {
        int obstacleAmount = -1;

        obstacleAmount = (minObstacleInPreset == maxObstacleInPreset) ? minObstacleInPreset : Random.Range(minObstacleInPreset, maxObstacleInPreset + 1);

        switch (obstacleAmount)
        {
            case 0:
                return new List<Vector2>(0);
            case 1:
                return OneObstaclePresets[Random.Range(0, OneObstaclePresets.Count)].preset;
            case 2:
                return TwoObstaclePresets[Random.Range(0, TwoObstaclePresets.Count)].preset;
            case 3:
                return ThreeObstaclePresets[Random.Range(0, ThreeObstaclePresets.Count)].preset;
            case 4:
                return FoureObstaclePresets[Random.Range(0, FoureObstaclePresets.Count)].preset;
            default:
                return null;
        }
    }
}
