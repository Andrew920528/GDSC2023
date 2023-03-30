namespace Mapbox.Unity.Utilities
{
	using UnityEngine;
	using System;

	public class DontDestroyOnLoad : MonoBehaviour
	{
		static DontDestroyOnLoad _instance;

		[SerializeField]
		bool _useSingleInstance;

		protected virtual void Awake()
		{
			if (_instance != null && _useSingleInstance)
			{
				Destroy(gameObject);
				return;
			}

			DontDestroyOnLoad(gameObject);
		}
	}
}