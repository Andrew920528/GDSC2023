using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationGoal : Quest.QuestGoal
{
    public string GoalLocation;
    public double GoalLatitude;
    public double GoalLongitude;

    public override string GetDescription()
    {
        return $"Go to {GoalLocation}!";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<GameEvent.LocationGameEvent>(OnWalking);
    }

    public void OnWalking(GameEvent.LocationGameEvent eventInfo)
    {
        Evaluate(eventInfo.latitude, eventInfo.longitude);
    }
}
