using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScanningGoal : Quest.QuestGoal
{
    public string targetObject;
    public override string GetDescription()
    {
        return $"Scan {(RequiredAmount == 1 ? "a" : "")} {targetObject}{(RequiredAmount == 1 ? "": "s")}";
    }

    public override void Initialize()
    {
        base.Initialize();
        EventManager.Instance.AddListener<GameEvent.ScanningGameEvent>(OnScanning);
    }

    private void OnScanning(GameEvent.ScanningGameEvent eventInfo)
    {
        if (eventInfo.scannedObject == targetObject)
        {
            // Increment quest progress on scan matching object
            CurrentAmount++;
            Evaluate();
        }
    }
}
