using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class PlayerStats 
{
    [SerializeField]
    public int Level { get; set; }
    [SerializeField]
    public int Experience { get; set; }
    [SerializeField]
    public int Coins { get; set; }
    [SerializeField]
    public int DistanceWalked { get; set; }
    [SerializeField]
    public int PlantomosCollected { get; set; }
    [SerializeField]
    public int QuestsCompleted { get; set; }
    [SerializeField]
    public int QuizCompleted { get; set; }
    [SerializeField]
    public int TimePlayed { get; set; }

    public PlayerStats()
    {
        Level = 0;
        Experience = 0;
        Coins = 0;
        DistanceWalked = 0;
        PlantomosCollected = 0;
        QuestsCompleted = 0;
        QuizCompleted = 0;
        TimePlayed = 0;
    }
}
