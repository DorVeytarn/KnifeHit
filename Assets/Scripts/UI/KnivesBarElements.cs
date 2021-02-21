using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class KnivesBarElements : MonoBehaviour
{
    [SerializeField] private Image selfImage;
    
    public bool IsMarked { get; private set; }

    public void Mark(Color color)
    {
        IsMarked = color != Color.white;

        selfImage.color = color;
    }
}
