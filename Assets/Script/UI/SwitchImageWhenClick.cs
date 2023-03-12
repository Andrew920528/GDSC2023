using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class SwitchImageWhenClick : MonoBehaviour
{
    private EventTrigger eventTrigger;
    private Image image;
    public Sprite img0;
    public Sprite img1;

    private int currSprite;


    private void Start()
    {
        eventTrigger = gameObject.AddComponent<EventTrigger>();
        image = GetComponent<Image>();

        SetUpSwitchImageWhenClicked();

    }

    private void SetUpSwitchImageWhenClicked()
    {

        EventTrigger.Entry switchImgEntry = new();
        switchImgEntry.eventID = EventTriggerType.PointerClick;
        switchImgEntry.callback.AddListener((data) =>
        {
            HandleSwitch();
            
        });
        eventTrigger.triggers.Add(switchImgEntry);
    }

    private void HandleSwitch()
    {
        if(currSprite == 1)
        {
            image.sprite = img0;
            currSprite = 0;
        } else if(currSprite == 0)
        {
            image.sprite = img1;
            currSprite = 1;
        }
    }
}
