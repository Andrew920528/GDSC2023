using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class EnlargeWhenClick : MonoBehaviour
{
    
    private EventTrigger eventTrigger;

    private void Start()
    {
        eventTrigger = gameObject.AddComponent<EventTrigger>();
        SetUpEnlargeWhenClicked();
        
    }

    private void SetUpEnlargeWhenClicked()
    {

        EventTrigger.Entry downEvent = new();
        EventTrigger.Entry upEvent = new();

        downEvent.eventID = EventTriggerType.PointerDown;
        upEvent.eventID = EventTriggerType.PointerUp;

        float x = transform.localScale.x;
        float y = transform.localScale.y;
        float z = transform.localScale.z;

        downEvent.callback.AddListener((data) =>
        {
            transform.localScale = new Vector3(x * 1.1f, y * 1.1f, z);
        });
        upEvent.callback.AddListener((data) =>
        {
            transform.localScale = new Vector3(x, y, z);
        });
        eventTrigger.triggers.Add(downEvent);
        eventTrigger.triggers.Add(upEvent);
    }
}
