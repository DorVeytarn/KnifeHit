using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private GameObject objectToSpawn;
    [SerializeField] private Transform spawnPoint;

    protected List<T> items = new List<T>();
    protected List<T> usedItems = new List<T>();

    public bool CanGetItem => items != null && items.Count > 0;
    public bool IsLastItem => items.Count == 0;

    private void ReturnItems()
    {
        for (int i = 0; i < usedItems.Count; i++)
        {
            usedItems[i].gameObject.transform.SetParent(spawnPoint);

            usedItems[i].gameObject.transform.localPosition = Vector3.zero;
            usedItems[i].gameObject.transform.rotation = Quaternion.identity;

            usedItems[i].gameObject.SetActive(false);

            items.Add(usedItems[i]);
        }

        usedItems.Clear();
    }

    private void SetItemsActive(int amount, bool active)
    {
        for (int i = 0; i < amount; i++)
        {
            items[i].gameObject.SetActive(active);
        }
    }

    public void CreateItems(int amount = 0)
    {
        if (usedItems.Count > 0)
            ReturnItems();

        if (items.Count >= amount)
            return;

        if (amount > items.Count)
            amount -= items.Count;

        for (int i = 0; i < amount; i++)
        {
            var newItem = Instantiate(objectToSpawn, spawnPoint);
            newItem.SetActive(false);

            items.Add(newItem.GetComponent<T>());
        }
    }

    public T GetNextItem(bool activateNext = true)
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].gameObject.activeSelf == true)
            {
                var item = items[i];

                if (activateNext && i + 1 < items.Count)
                    items[i + 1].gameObject.SetActive(true);

                usedItems.Add(items[i]);
                items.RemoveAt(i);

                return item.GetComponent<T>();
            }
        }

        return default;
    }

    public void SetItemsAtPosition(Vector2[] positions)
    {
        if (usedItems.Count > 0)
            ReturnItems();

        if (CheckRequiredItemsAmount(positions.Length) == false)
            CreateItems(positions.Length - items.Count);

        SetItemsActive(positions.Length, true);

        for (int i = 0; i < positions.Length; i++)
        {
            var item = GetNextItem(false);
            item.gameObject.transform.localPosition = positions[i];

            Vector3 supportingVector = (positions[i].x > 0) ? Vector3.up : Vector3.down;
            item.gameObject.transform.localRotation = Quaternion.Euler(0, 0, Vector3.Angle(supportingVector, spawnPoint.localPosition - item.gameObject.transform.localPosition));
        }
    }

    public bool CheckRequiredItemsAmount(int requiredAmount)
    {
        return items.Count >= requiredAmount;
    }
}
