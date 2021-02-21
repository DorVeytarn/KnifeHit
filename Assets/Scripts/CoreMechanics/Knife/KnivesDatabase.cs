using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New KnivesDatabase", menuName = "KnivesDatabase", order = 1)]
public class KnivesDatabase : ScriptableObject
{
    [SerializeField] private List<KnifeData> knives;

    public List<KnifeData> Knives => knives;
}
