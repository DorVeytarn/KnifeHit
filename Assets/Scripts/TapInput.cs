using Utils.Singletone;
using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class TapInput : MonoBehaviour, IPointerDownHandler
{
    public Action Taped;

    private void Awake()
    {
        SceneComponentProvider.RegisterComponent(typeof(TapInput), this);
    }

    private void Start()
    {
        transform.SetAsLastSibling();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Taped?.Invoke();
    }
}
