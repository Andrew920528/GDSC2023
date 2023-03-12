using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
public class CameraMovementWithZoom : MonoBehaviour
{
    public GameObject cam;
    public AbstractMap map;
    public float offset;

    private float theta;
    
    // Update is called once per frame
    void Update()
    {

        // Handle camera rotation
        cam.transform.eulerAngles = new Vector3(
            ZoomToTilt(16f,80f,20f, 25f, map.Zoom),
            cam.transform.eulerAngles.y,
            cam.transform.eulerAngles.z);

        theta = Mathf.Clamp(cam.transform.eulerAngles.x,1,89.9f);

        // Handle camera translation
        float z = cam.transform.position.y / Mathf.Tan(ToRadians(theta)); // - (map.Zoom-16)* (map.Zoom - 16) * 5;
        z += offset * (1 / Mathf.Sin(ToRadians(theta)));
        z -= 1617.88f + (-192.615f) * map.Zoom + 5.72517f * map.Zoom * map.Zoom;
        cam.transform.position = new Vector3(cam.transform.position.x, cam.transform.position.y, -z);

        
        
    }

    
    
    private static float ZoomToTilt(float x1, float y1, float x2, float y2, float zoom)
    {

        float slope = (y1 - y2) / (x1 - x2);

        return (zoom-x1) * slope + y1;
    }

    private static float ToRadians(float angle) => (angle * Mathf.PI) / 180.0f;
    

    
}
