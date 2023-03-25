using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class MapRotation : MonoBehaviour
{
	[SerializeField]
	float _rotateSpeed = 1f;

	public Camera _referenceCamera;
	public GameObject map;

	private Vector3 previousPosition;

	void Awake()
	{
		if (null == _referenceCamera)
		{
			_referenceCamera = GetComponent<Camera>();
			if (null == _referenceCamera) { Debug.LogErrorFormat("{0}: reference camera not set", this.GetType().Name); }
		}
	}

	
	public void Update()
	{
		if (Input.GetMouseButtonDown(0))
		{
			previousPosition = _referenceCamera.ScreenToViewportPoint(Input.mousePosition);
		}
		else if (Input.GetMouseButton(0))
		{
			Vector3 currentPosition = _referenceCamera.ScreenToViewportPoint(Input.mousePosition);
			Vector3 direction = previousPosition - currentPosition;


			Vector3 targetPosition = _referenceCamera.WorldToViewportPoint(map.transform.position);
			float rotationAroundYAxis;

			if (targetPosition.y > currentPosition.y)
			{
				rotationAroundYAxis = direction.x * 180; 
			}
			else
			{
				rotationAroundYAxis = -direction.x * 180;
			}

			if (targetPosition.y + 0.2 > currentPosition.y && currentPosition.y > targetPosition.y - 0.1)
			{
				if (targetPosition.x > currentPosition.x)
				{
					rotationAroundYAxis -= direction.y * 180; 
				}
				else
				{
					rotationAroundYAxis += direction.y * 180; 
				}
			}
			map.transform.Rotate(new Vector3(0, 1, 0), rotationAroundYAxis*_rotateSpeed, Space.World);
			previousPosition = currentPosition;
		}

	}

    
}
