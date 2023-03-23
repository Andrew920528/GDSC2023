using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DragPanel : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private Vector3 previousPosition;
    private Camera referenceCamera;
    private bool isDragging;

    private float panelMaxPosition;
    private float panelMinPosition;


    private void Start()
    {
        referenceCamera = Camera.main;
        eventTrigger = gameObject.AddComponent<EventTrigger>();
        SetUpPanelDrag();
        isDragging = false;

        panelMinPosition = transform.position.y;
        panelMaxPosition = -1.36f;
        Debug.Log(panelMaxPosition);
        Debug.Log(panelMinPosition);

    }


    private void SetUpPanelDrag()
    {
        EventTrigger.Entry downEntry = new();
        EventTrigger.Entry dragEntry = new();
        EventTrigger.Entry upEntry = new();
        downEntry.eventID = EventTriggerType.PointerDown;
        dragEntry.eventID = EventTriggerType.Drag;
        upEntry.eventID = EventTriggerType.PointerUp;

        dragEntry.callback.AddListener((data) =>
        {
            // Keep Track of pointer position
            if (!isDragging)
            {
                previousPosition = referenceCamera.ScreenToViewportPoint(Input.mousePosition);
                isDragging = true;
            }

        });

        dragEntry.callback.AddListener((data) =>
        {
            DragDelegate((PointerEventData)data);
        });

        upEntry.callback.AddListener((data) =>
        {
            LeaveDrag((PointerEventData)data);

        });

        eventTrigger.triggers.Add(downEntry);
        eventTrigger.triggers.Add(dragEntry);
        eventTrigger.triggers.Add(upEntry);
    }



    private void DragDelegate(PointerEventData data)
    {
        if (Input.touchSupported && Input.touchCount > 0)
        {
            // HandleTouch();
            Debug.Log("Use touch");
        }
        else
        {
            DragWithMouse(data);
        }
    }


    private void DragWithMouse(PointerEventData data)
    {
        Vector3 currentPosition = referenceCamera.ScreenToViewportPoint(Input.mousePosition);
        Vector3 direction = currentPosition - previousPosition;

        float dragDirY = 10 * direction.y;

        transform.position = new Vector2(transform.position.x,
            Math.Clamp(transform.position.y + dragDirY, panelMinPosition, panelMaxPosition));
        Debug.Log(transform.position.y);
        previousPosition = currentPosition;

    }

    private void LeaveDrag(PointerEventData data)
    {
        isDragging = false;
    }
}
