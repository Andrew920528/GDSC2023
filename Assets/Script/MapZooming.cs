using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Map;
using Mapbox.Unity.Utilities;
using Mapbox.Utils;
using UnityEngine.EventSystems;
using System;

/// <summary>
/// Allow Zooming on mobile/ pc
/// </summary>
public class MapZooming : MonoBehaviour
{
	[SerializeField]
	float _maxZoom = 20;

	[SerializeField]
	float _minZoom = 16;

	[SerializeField]
	float _zoomSpeed = 0.25f;

	[SerializeField]
	public Camera _referenceCamera;

	[SerializeField]
	AbstractMap _map;

	private bool _isInitialized = false;
	private bool _dragStartedOnUI = false;

	

	void Awake()
	{
		if (null == _referenceCamera)
		{
			_referenceCamera = GetComponent<Camera>();
			if (null == _referenceCamera) { Debug.LogErrorFormat("{0}: reference camera not set", this.GetType().Name); }
		}
		_map.OnInitialized += () =>
		{
			_isInitialized = true;
		};

		
	}

	public void Update()
	{
		if (Input.GetMouseButtonDown(0) && EventSystem.current.IsPointerOverGameObject())
		{
			_dragStartedOnUI = true;
		}

		if (Input.GetMouseButtonUp(0))
		{
			_dragStartedOnUI = false;
		}
	}


	private void LateUpdate()
	{
		if (!_isInitialized) { return; }

		if (!_dragStartedOnUI)
		{
			if (Input.touchSupported && Input.touchCount > 0)
			{
				HandleTouch();
			}
			else
			{
				HandleMouseAndKeyBoard();
			}
		}
	}

	void HandleMouseAndKeyBoard()
	{
		// zoom
		float scrollDelta;
		scrollDelta = Input.GetAxis("Mouse ScrollWheel");
		ZoomMapUsingTouchOrMouse(scrollDelta);

	}

	void HandleTouch()
	{
		float zoomFactor;
		//pinch to zoom.
		if (Input.touchCount == 2)
		{
			
				// Store both touches.
				Touch touchZero = Input.GetTouch(0);
				Touch touchOne = Input.GetTouch(1);

				// Find the position in the previous frame of each touch.
				Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
				Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

				// Find the magnitude of the vector (the distance) between the touches in each frame.
				float prevTouchDeltaMag = (touchZeroPrevPos - touchOnePrevPos).magnitude;
				float touchDeltaMag = (touchZero.position - touchOne.position).magnitude;

				// Find the difference in the distances between each frame.
				zoomFactor = 0.01f * (touchDeltaMag - prevTouchDeltaMag);
				
				ZoomMapUsingTouchOrMouse(zoomFactor);
				
		}
	}

	void ZoomMapUsingTouchOrMouse(float zoomFactor)
	{
		var zoom = Mathf.Clamp(_map.Zoom + zoomFactor * _zoomSpeed, _minZoom, _maxZoom);
		if (Math.Abs(zoom - _map.Zoom) > 0.0f)
		{
			_map.UpdateMap(_map.CenterLatitudeLongitude, zoom);
		}
	}

	

}

