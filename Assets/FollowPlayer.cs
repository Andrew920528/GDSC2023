using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private Vector3 offset;
    [SerializeField] private float damping;

    private Vector3 velocity = Vector3.zero;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        Vector3 movePosition = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, movePosition, ref velocity, damping); // Camera follows the player with specified offset position
    }
}
