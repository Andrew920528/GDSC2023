using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;
    [SerializeField] private AbstractMap map;

    private Vector3 velocity = Vector3.zero;
    
    void LateUpdate()
    {
        // Debug.Log(map.WorldToGeoPosition(player.transform.position));
        Debug.Log((player.transform.localPosition));
        map.UpdateMap(map.WorldToGeoPosition(player.transform.position),map.Zoom);
        // Vector3 movePosition = player.position + offset;
        // transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping); // Camera follows the player with specified offset position
    }
}
