using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class FirebaseData
{
    // Wrapper class for retrieving and storing Firebase Realtime Database Data
    public PlayerStats PlayerStats { get; set; }
    public List<QuestData> QuestTracker { get; set; }
    public List<Plantomo> PlantomoInventory { get; set; }
    public Dictionary<string, int> ItemInventory { get; set; }

    public FirebaseData(PlayerStats stats, List<QuestData> quests, List<Plantomo> plantomoInv, Dictionary<string, int> itemInv)
    {
        PlayerStats = stats;
        QuestTracker = quests;
        PlantomoInventory = plantomoInv;
        ItemInventory = itemInv;
    }
}


[Serializable]
public class QuestData
{
    public int index = 0;
    public string questType = null;
    public double currentAmount = 0;
    public double requiredAmount = 1;
    public bool completed = false;

    public QuestData(int index, string questType, double currentAmount, double requiredAmount, bool completed)
    {
        this.index = index;
        this.questType = questType;
        this.currentAmount = currentAmount;
        this.requiredAmount = requiredAmount;
        this.completed = completed;

    }

    public QuestData(QuestData questData)
    {
        this.index = questData.index;
        this.questType = questData.questType;
        this.currentAmount = questData.currentAmount;
        this.requiredAmount = questData.requiredAmount;
        this.completed = questData.completed;
    }

    public QuestData()
    {
        // Default constructor for JSON
    }

}