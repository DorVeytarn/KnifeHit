using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PositionPreset
{
    public int currentPresetsAmount;
    public Vector2[] preset;

    public PositionPreset(int currentPresetsAmount)
    {
        this.currentPresetsAmount = currentPresetsAmount;
        preset = new Vector2[currentPresetsAmount];
    }
}

[CreateAssetMenu(fileName = "New ObstaclePresets", menuName = "ObstaclePresets", order = 1)]
public class ObstaclePresets : ScriptableObject
{
    public List<PositionPreset> OneObstaclePresets = new List<PositionPreset>();
    public List<PositionPreset> TwoObstaclePresets = new List<PositionPreset>();
    public List<PositionPreset> ThreeObstaclePresets = new List<PositionPreset>();
    public List<PositionPreset> FoureObstaclePresets = new List<PositionPreset>();
}
