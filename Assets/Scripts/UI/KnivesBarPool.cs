using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Utils.Singletone;

public class KnivesBarPool : MonoBehaviour
{

    [SerializeField] private List<KnivesBarElements> elements = new List<KnivesBarElements>();
    [SerializeField] private Color markedColor;
    [SerializeField] private Color hideColor;

    private LevelCreator levelCreator;
    private KnifeLauncher knifeLauncher;
    private Color unmarkedColor = Color.white;
    private int lastActiveItem;

    private void Start()
    {
        levelCreator = SceneComponentProvider.GetComponent(typeof(LevelCreator)) as LevelCreator;
        knifeLauncher = SceneComponentProvider.GetComponent(typeof(KnifeLauncher)) as KnifeLauncher;

        levelCreator.LevelCreated += OnLevelCreated;
        knifeLauncher.KnifeLaunched += OnKnifeLaunched;
    }

    private void OnDestroy()
    {
        levelCreator.LevelCreated -= OnLevelCreated;
        knifeLauncher.KnifeLaunched -= OnKnifeLaunched;
    }

    private void OnLevelCreated()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            elements[i].Mark(unmarkedColor);
        }

        if (levelCreator.CurrentLevel.RequiredKnifeAmount <= elements.Count)
        {
            for (int i = elements.Count - 1; i >= levelCreator.CurrentLevel.RequiredKnifeAmount; i--)
                elements[i].Mark(hideColor);
        }

        lastActiveItem = 0;
    }

    private void OnKnifeLaunched()
    {
        for (int i = 0; i < elements.Count; i++)
        {
            int itemToMark = elements.Count - 1;

            if (elements[i].IsMarked)
            {
                itemToMark = (i - 1 > 0) ? i - 1 : 0;
                elements[itemToMark].Mark(markedColor);
                break;
            }

        }
    }
}
