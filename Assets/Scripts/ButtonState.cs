using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonState : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool IsPressed { get; private set; }
    public bool IsPointerDown { get; private set; }

    private void LateUpdate()
    {
        IsPointerDown = false;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        IsPressed = true;
        IsPointerDown = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        IsPressed = false;
    }
}