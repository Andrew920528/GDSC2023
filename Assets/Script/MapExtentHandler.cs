using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
public class MapExtentHandler : MonoBehaviour
{
    private RangeTileProviderOptions extent;
    [SerializeField]
    AbstractMap _map;

    // with respect to phone user
    private int front, right, back, left;

    private void Start()
    {
        SetFRBL(1, 1, 1, 1);
        extent = new RangeTileProviderOptions { east = right, west = left, north = front, south = back };
        _map.SetExtentOptions(extent);

    }

    private void LateUpdate()
    {
        ChangeExtensionWithZoom();
        ChangeExtentWithRotation();
    }

    private void ChangeExtensionWithZoom()
    {

        if (_map.Zoom >= 19.5f)
        {
            if (front == 6)
            {
                return;
            }
            else
            {
                SetFRBL(6, 4, 1, 4);
            }
        }
        else if (_map.Zoom >= 18f)
        {
            if (front == 5)
            {
                return;
            }
            else
            {
                SetFRBL(5, 3, 1, 3);
            }
        }
        else if (_map.Zoom >= 17f)
        {
            if (front == 2)
            {
                return;
            }
            else
            {
                SetFRBL(2, 1, 1, 1);
            }
        }
        else
        {
            
            if (front == 1)
            {
                return;
            }
            else
            {
                SetFRBL(1, 1, 1, 1);
            }
        }

    }


    private void ChangeExtentWithRotation()
    {

        float rotY = transform.eulerAngles.y;
        //Debug.Log(rotY);
        if (rotY > 180)
        {
            rotY -= 360;
        }
        if (rotY <= 45 && rotY > -45)
        {
            // facing north
            extent.SetOptions(northRange: front, eastRange: right, southRange: back, westRange: left);

        }
        else if (rotY <= -45 && rotY > -135)
        {
            // facing east
            extent.SetOptions(northRange: left, eastRange: front, southRange: right, westRange: back);

        }
        else if (rotY <= 135 && rotY > 45)
        {
            // facing west
            extent.SetOptions(northRange: right, eastRange: back, southRange: left, westRange: front);
        }
        else
        {
            // facing south
            extent.SetOptions(northRange: back, eastRange: left, southRange: front, westRange: right);
        }
        _map.SetExtentOptions(extent);

    }

    private void SetFRBL(int f, int r, int b, int l)
    {
        front = f;
        right = r;
        back = b;
        left = l;
    }
}