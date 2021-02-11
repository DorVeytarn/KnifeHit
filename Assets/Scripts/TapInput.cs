using utils.singletone;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapInput : MonoBehaviour, IPointerDownHandler
{
    public Action Taped;

    private void Start()
    {
        SceneComponentProvider.RegisterComponent(typeof(TapInput), this);
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Taped?.Invoke();
    }
}
