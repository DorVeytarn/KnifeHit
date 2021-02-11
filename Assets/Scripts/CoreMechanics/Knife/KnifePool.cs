using utils.singletone;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class KnifePool : MonoBehaviour
{
    [SerializeField] private int knifeAmount;
    [SerializeField] private GameObject currentKnife;
    [SerializeField] private Transform spawnPoint;

    private List<Knife> knives = new List<Knife>();
    private List<Knife> usedKnives = new List<Knife>();

    public bool IsLastKnife => knives.Count == 0;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(KnifePool), this);
    }

    private void ReturnItems()
    {
        for (int i = 0; i < usedKnives.Count; i++)
        {
            usedKnives[i].gameObject.transform.SetParent(spawnPoint);
            usedKnives[i].gameObject.transform.localPosition = Vector3.zero;
            usedKnives[i].gameObject.transform.rotation = Quaternion.identity;
            usedKnives[i].gameObject.SetActive(false);

            knives.Add(usedKnives[i]);
        }

        usedKnives.Clear();
    }

    public void CreateKnives(int amount = 0)
    {
        if (usedKnives.Count > 0)
            ReturnItems();

        if (amount > knives.Count)
            amount -= knives.Count;

        for (int i = 0; i < amount; i++)
        {
            var newKnife = Instantiate(currentKnife, spawnPoint);
            newKnife.SetActive(false);

            knives.Add(newKnife.GetComponent<Knife>());
        }

        knives[0].gameObject.SetActive(true);
    }

    public bool CheckKnifeAmount()
    {
        return knives != null && knives.Count > 0;
    }

    public Knife GetNextKnife()
    {
        for (int i = 0; i < knives.Count; i++)
        {
            if (knives[i].gameObject.activeSelf == true)
            {
                var knife = knives[i];

                if(i + 1 < knives.Count)
                    knives[i + 1].gameObject.SetActive(true);

                usedKnives.Add(knives[i]);
                knives.RemoveAt(i);

                return knife;
            }
        }

        return null;
    }
}
