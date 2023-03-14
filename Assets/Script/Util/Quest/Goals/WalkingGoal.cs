using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingGoal : Quest.QuestGoal
{
    public float GoalDistance;

    public override string GetDescription()
    {
        return $"Walk {GoalDistance} kilometers";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<GameEvent.WalkingGameEvent>(OnWalking);
    }

    public void OnWalking(GameEvent.WalkingGameEvent eventInfo)
    {
        if (eventInfo.walkedDistance > 0)
        {
            // Convert from meter to km
            CurrentAmount += eventInfo.walkedDistance;
            Evaluate();
        }
    }
}
