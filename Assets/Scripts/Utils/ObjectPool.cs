using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool<T> : MonoBehaviour where T : MonoBehaviour
{
    [SerializeField] private Transform spawnPoint;
    [SerializeField] protected GameObject objectToSpawn;

    protected List<T> items = new List<T>();
    protected List<T> usedItems = new List<T>();

    public int ItemMaxCount { get; private set; }
    public int ItemCount => items.Count;
    public bool CanGetItem => items != null && items.Count > 0;
    public bool IsLastItem => items.Count == 0;

    public void ReturnItems()
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

    public virtual void CreateItems(int amount = 0)
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

        ItemMaxCount = items.Count;
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

    public void SetItemsAtPosition(List<Vector2> positions)
    {
        if (usedItems.Count > 0)
            ReturnItems();

        if (CheckRequiredItemsAmount(positions.Count) == false)
            CreateItems(positions.Count - items.Count);

        SetItemsActive(positions.Count, true);

        for (int i = 0; i < positions.Count; i++)
        {
            var item = GetNextItem(false);
            item.gameObject.transform.localPosition = positions[i];

            float angle = (positions[i].x > 0) ? Vector3.Angle(Vector3.up, spawnPoint.localPosition - item.gameObject.transform.localPosition)
                                               : -Vector3.Angle(Vector3.up, spawnPoint.localPosition - item.gameObject.transform.localPosition);

            item.gameObject.transform.localRotation = Quaternion.Euler(0, 0, angle);
        }
    }

    public bool CheckRequiredItemsAmount(int requiredAmount)
    {
        return items.Count >= requiredAmount;
    }

    public void SetNewSpawnObject(GameObject newObject)
    {
        ReturnItems();

        for (int i = 0; i < items.Count; i++)
            Destroy(items[i].gameObject);

        items.Clear();

        objectToSpawn = newObject;
    }
}
