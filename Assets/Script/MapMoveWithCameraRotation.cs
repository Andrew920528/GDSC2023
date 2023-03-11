using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
public class MapMoveWithCameraRotation : MonoBehaviour
{
    public GameObject cam;
    public AbstractMap map;
    public float offset;

    private float theta;
    void Start()
    {
        theta = Mathf.Clamp(cam.transform.eulerAngles.x, 5, 89);
    }

    // Update is called once per frame
    void Update()
    {

        cam.transform.eulerAngles = new Vector3(
            ZoomToTilt(map.Zoom),
            cam.transform.eulerAngles.y,
            cam.transform.eulerAngles.z);

        theta = Mathf.Clamp(cam.transform.eulerAngles.x,1,89.9f);

        
        float z = cam.transform.position.y / Mathf.Tan(ToRadians(theta));
        z += offset * (1 / Mathf.Sin(ToRadians(theta)));

        transform.position = new Vector3(transform.position.x, transform.position.y, z);

        ChangeExtensionWithZoom();
    }

    
    private static float ZoomToTilt(float zoom)
    {
        return 300f - zoom * 13.75f;
    }

    private static float ToRadians(float angle)
    {
        
        return (angle * Mathf.PI) / 180.0f;
    }

    private void ChangeExtensionWithZoom()
    {
        if(map.Zoom >= 18f)
        {
            map.SetExtentOptions(new RangeTileProviderOptions { east = 3, west = 3, north = 5, south = 1 });
        }
        else if (map.Zoom >= 17f)
        {
            map.SetExtentOptions(new RangeTileProviderOptions { east = 2, west = 2, north = 3, south = 1 });
        }
        else
        {
            map.SetExtentOptions(new RangeTileProviderOptions { east = 1, west = 1, north = 1, south = 1 });
        }
        
    }
}
