using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
public class StyleWithZoom : MonoBehaviour
{
    public AbstractMap map;

    public GameObject playerFull;
    public GameObject playerHead;

    
    void Update()
    {
        if(map.Zoom >= 18)
        {
            playerFull.SetActive(true);
            playerHead.SetActive(false);
        }
        else
        {
            playerFull.SetActive(false);
            playerHead.SetActive(true);
        }
        HandlePlayerZoom();
    }

    private void HandlePlayerZoom()
    {
        float scale = -65.20895522f + (6.035714286f) * map.Zoom + -0.1289978678f * map.Zoom * map.Zoom;
        float y = 108.619403f + (-14.57142857f) * map.Zoom + 0.4818763326f * map.Zoom * map.Zoom;

        playerFull.transform.localScale = new Vector3(scale, scale, playerFull.transform.localScale.z);
        playerFull.transform.localPosition = new Vector3(playerFull.transform.localPosition.x, y, playerFull.transform.localPosition.z); 
    }
}
