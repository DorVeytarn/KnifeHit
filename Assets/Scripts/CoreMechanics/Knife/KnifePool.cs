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

    public bool IsLastKnife => knives.Count == 1;

    private void Start()
    {
        SceneComponentProvider.RegisterComponent(typeof(KnifeLauncher), this);

        CreateKnifes();
    }

    private void CreateKnifes(int amount = 0)
    {
        amount = (amount != 0) ? amount : knifeAmount;

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
