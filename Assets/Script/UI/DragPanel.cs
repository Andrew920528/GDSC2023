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
    private float dragDirY;
    private float panelMaxPosition;
    private float panelMinPosition;
    private float velocity = 1f;


    private void Start()
    {
        referenceCamera = Camera.main;
        eventTrigger = gameObject.AddComponent<EventTrigger>();
        SetUpPanelDrag();
        isDragging = false;

        panelMinPosition = transform.position.y;
        panelMaxPosition = -1.36f;
        dragDirY = 0;

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
                StopAllCoroutines();
                previousPosition = referenceCamera.ScreenToViewportPoint(Input.mousePosition);
                dragDirY = 0;
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
            DragWithMouse(data);
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

        
        dragDirY = 10 * direction.y;

        transform.position = new Vector2(transform.position.x,
            Math.Clamp(transform.position.y + dragDirY, panelMinPosition, panelMaxPosition));
        // Debug.Log(transform.position.y);
        previousPosition = currentPosition;

    }

    private void LeaveDrag(PointerEventData data)
    {
        if(isDragging)
        {
            if (dragDirY > 0)
            {
                StartCoroutine(MovePanelUp());
            } else if (dragDirY < 0)
            {
                StartCoroutine(MovePanelDown());
            }

            isDragging = false;
            dragDirY = 0;
        }

    }

    IEnumerator MovePanelUp()
    {
        
        while (transform.position.y <= panelMaxPosition)
        {
            transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y + velocity, panelMinPosition, panelMaxPosition), transform.position.z);
            yield return null;
        }
    }

    IEnumerator MovePanelDown()
    {
        while (transform.position.y >= panelMinPosition)
        {
            transform.position = new Vector3(transform.position.x, Math.Clamp(transform.position.y -velocity , panelMinPosition, panelMaxPosition), transform.position.z);
            yield return null;
        }
    }
}
