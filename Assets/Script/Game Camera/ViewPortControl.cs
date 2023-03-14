using System.Collections;
using System.Collections.Generic;
using Mapbox.Unity.Map;
using UnityEngine;

public class ViewPortControl : MonoBehaviour
{
    Vector3 touchStart;
    public float zoomOutMin;
    public float zoomOutMax;

    public float zoomOutRotation;
    public float zoomInRotation;

    public AbstractMap map;

    private void Start()
    {
        zoomOutMin = 1f;
        zoomOutMax = 22f;

        zoomOutRotation = 50f;
    }

    void Update()
    {
        HandlePinAndDrag();
        HandlePhoneZoom();
        HandlePCZoom();
        
    }

    public void HandlePhoneZoom()
    {
        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

            float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
            float currentMagnitude = (touchZero.position - touchOne.position).magnitude;

            float difference = currentMagnitude - prevMagnitude;

            Zoom(difference * 0.01f);
        }
    }

    public void HandlePCZoom()
    {
        Zoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    private void Zoom(float increment)
    { 
        map.UpdateMap(Mathf.Clamp(map.Zoom - increment,zoomOutMin,zoomOutMax));


        
    }

    public void HandlePinAndDrag()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touchStart - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(direction);
            Debug.Log("getmousebutton");
            Camera.main.transform.position += direction;
        }
    }

    
}
