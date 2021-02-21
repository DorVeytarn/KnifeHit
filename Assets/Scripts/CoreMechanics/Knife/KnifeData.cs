using UnityEngine;

public enum KnifeType
{
    Usual,
    BossKnife,
    //Legendary, Challenge, etc.
}

[System.Serializable]
public class KnifeData
{
    [SerializeField] private int price = 0;
    [SerializeField] private KnifeType knifeType = KnifeType.Usual;
    [SerializeField] private string name;

    public GameObject Model;

    public int Price => price;
    public KnifeType KnifeType => knifeType;
    public string Name => name;
}
