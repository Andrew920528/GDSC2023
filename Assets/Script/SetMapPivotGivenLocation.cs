using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mapbox.Unity.Location;
using Mapbox.Unity.Map;

/// <summary>
/// Slightly modified from Mapbox.Examples.ImmediatePositionWithLocationProvider
/// Sets map pivot to the given location (user location)
/// </summary>
public class SetMapPivotGivenLocation : MonoBehaviour
{
	bool _isInitialized;

	[SerializeField] AbstractMap map;
	ILocationProvider _locationProvider;
	ILocationProvider LocationProvider
	{
		get
		{
			if (_locationProvider == null)
			{
				_locationProvider = LocationProviderFactory.Instance.DefaultLocationProvider;
			}

			return _locationProvider;
		}
	}

	Vector3 _targetPosition;

	void Start()
	{
		LocationProviderFactory.Instance.mapManager.OnInitialized += () => _isInitialized = true;
	}

	void LateUpdate()
	{
		if (_isInitialized)
		{
			map.UpdateMap(LocationProvider.CurrentLocation.LatitudeLongitude, map.Zoom);
			
		}
	}
}
