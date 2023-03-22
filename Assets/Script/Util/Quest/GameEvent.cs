using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEvent
{
    public class WalkingGameEvent : GameEvent
    {
        public double walkedDistance = 0;

        public WalkingGameEvent(double distance)
        {
            walkedDistance += distance / 1000;
        }

        
    }

    public class ScanningGameEvent : GameEvent
    {
        public string scannedObject;
        public ScanningGameEvent(string obj)
        {
            scannedObject = obj;
            Debug.Log("scanned" + obj);
        }
    }
}
