
using Mapbox.Unity.Location;
using Mapbox.Utils;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistanceTracker : Singleton<DistanceTracker>
{
	[SerializeField]
	private LocationInfo lastLocation;
	private LocationInfo currentLocation;
	[SerializeField]
	private double distanceTraveled = 0;
	[SerializeField]
	private double updateDistanceThreshold = 70; 
	public float waitTimeForUpdate = 5;

	//private AbstractLocationProvider _locationProvider = null;
	void Start()
	{
		Input.location.Start();

		if (Input.location.status == LocationServiceStatus.Running)
        {
			currentLocation = Input.location.lastData;
			lastLocation = currentLocation;
		}


		EventManager.Instance.AddListener<GameEvent.WalkingGameEvent>(Walk);

		StartCoroutine(UpdateLocation(currentLocation));
	}

    private void Update()
    {
		if (Input.location.status == LocationServiceStatus.Running)
		{
			currentLocation = Input.location.lastData;
		}
	}


    IEnumerator UpdateLocation(LocationInfo location)
    {
		
		distanceTraveled = HaversineDistance(lastLocation, location);
		Debug.Log("distance update:" + distanceTraveled);
		Debug.Log(distanceTraveled < updateDistanceThreshold);
		if (distanceTraveled < updateDistanceThreshold)
        {
			Debug.Log("walking event queued");
			EventManager.Instance.QueueEvent(new GameEvent.WalkingGameEvent(distanceTraveled));
		}
		lastLocation = location;
		
		yield return new WaitForSecondsRealtime(waitTimeForUpdate);
		//Location currLoc = _locationProvider.CurrentLocation;
		StartCoroutine(UpdateLocation(currentLocation));
	}

	// Calculates the distance between two points on the Earth's surface
	public static double HaversineDistance(LocationInfo location1, LocationInfo location2)
	{
		double lat1 = location1.latitude;
		double lon1 = location1.longitude;
		double lat2 = location2.latitude;
		double lon2 = location2.longitude;


		const double R = 6371; // Earth's radius in km

		// Convert latitude and longitude to radians
		lat1 = ToRadians(lat1);
		lon1 = ToRadians(lon1);
		lat2 = ToRadians(lat2);
		lon2 = ToRadians(lon2);

		// Calculate the differences between the latitudes and longitudes
		double dLat = lat2 - lat1;
		double dLon = lon2 - lon1;

		// Apply the Haversine formula to calculate the distance
		double a = Mathd.Pow(Mathd.Sin(dLat / 2), 2) + Mathd.Cos(lat1) * Mathd.Cos(lat2) * Mathd.Pow(Mathd.Sin(dLon / 2), 2);
		double c = 2 * Mathd.Atan2(Mathd.Sqrt(a), Mathd.Sqrt(1 - a));
		double distance = R * c * 1000; // Convert km to meters

		return distance;
	}

	// Converts degrees to radians
	public static double ToRadians(double degrees)
	{
		return degrees * Mathd.PI / 180;
	}

	public void Walk(GameEvent.WalkingGameEvent eventInfo)
	{
		if (eventInfo.walkedDistance > 0)
		{
			// Convert from meter to km
			distanceTraveled += eventInfo.walkedDistance / 1000;
		}
	}

}