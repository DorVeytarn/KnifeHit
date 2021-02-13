using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObstaclePresets))]
public class ObstaclePresetsCustomEditor : Editor
{
    private ObstaclePresets presets;
    private List<PositionPreset> onePositions;
    private List<PositionPreset> twoPositions;
    private List<PositionPreset> threePositions;
    private List<PositionPreset> fourPositions;

    private void OnEnable()
    {
        presets = (ObstaclePresets)target;
        onePositions = presets.OneObstaclePresets;
        twoPositions = presets.TwoObstaclePresets;
        threePositions = presets.ThreeObstaclePresets;
        fourPositions = presets.FoureObstaclePresets;
    }

    public override void OnInspectorGUI()
    {
        DrawPreset(1, "One obstacle presets", onePositions);
        DrawPreset(2, "Two obstacle presets", twoPositions);
        DrawPreset(3, "Three obstacle presets", threePositions);
        DrawPreset(4, "Four obstacle presets", fourPositions);
    }

    private void DrawPreset(int postionAmount, string label, List<PositionPreset> positions)
    {
        GUILayout.Space(7);

        GUILayout.Label(label);

        if (GUILayout.Button("Create new preset  " + postionAmount))
            positions.Add(new PositionPreset(postionAmount));

        for (int i = 0; i < positions.Count; i++)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label(i.ToString());

            if (GUILayout.Button("X"))
            {
                if (positions.Count > 0)
                    positions.RemoveAt(i);

                break;
            }
            GUILayout.EndHorizontal();

            for (int j = 0; j < positions[i].preset.Length; j++)
                positions[i].preset[j] = EditorGUILayout.Vector2Field("Position", positions[i].preset[j]);
        }
    }
}
