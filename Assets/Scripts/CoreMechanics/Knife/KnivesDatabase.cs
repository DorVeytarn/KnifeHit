using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KnivesDatabase", menuName = "KnivesDatabase", order = 1)]
public class KnivesDatabase : ScriptableObject
{
    [SerializeField] private List<KnifeData> knives;

    public List<KnifeData> Knives => knives;

    public KnifeData GetKnifeDataByName(string knifeName)
    {
        for (int i = 0; i < knives.Count; i++)
        {
            if (knives[i].Name == knifeName)
                return knives[i];
        }

        return null;
    }
}
