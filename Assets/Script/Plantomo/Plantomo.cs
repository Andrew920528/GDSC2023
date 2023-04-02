using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

[Serializable]
public class Plantomo
{
    [SerializeField]
    public int Id { get; set; }
    [SerializeField]
    public string Name { get; set; }
    [SerializeField]
    public string Description { get; set; }
    [SerializeField]
    public int PlantID { get; set; }
    [SerializeField]
    public int Familiarity { get; set; }
    [SerializeField]
    public int Level { get; set; }
    [SerializeField]
    public int FamiliarityToNextLevel = 3;
    [SerializeField]
    public int WaterLevel { get; set; }
    [SerializeField]
    public int SunlightLevel { get; set; }
    [SerializeField]
    public string SoilType { get; set; }
    [SerializeField]
    public bool QuizCompleted { get; set; }

    // Plantomo gameobject (sprite + animation)
    public GameObject PlantomoPrefab { get; }

    public Plantomo()
    {
        // Default constructor for JSON
    }

    public Plantomo(Plantomo plantomo)
    {
        Id = plantomo.Id;
        Name = plantomo.Name;
        Description = plantomo.Description;
        PlantID = plantomo.PlantID;
        Familiarity = plantomo.Familiarity;
        Level = plantomo.Level;
        FamiliarityToNextLevel = 3;
    }

    //public Plantomo(int id, string name)
    //{
    //    Id = id;
    //    Name = name;
    //    Familiarity = 0;
    //    Level = 1;
    //}


    public Plantomo(int id, string name, string description = null, int plantID = 0, int familiarity = 0,
        int level = 1, int waterLevel = 0, int sunlightLevel = 0, string soilType = null, bool quizCompleted = false)
    {
        Id = id;
        Name = name;
        Description = description;
        PlantID = plantID;
        Familiarity = familiarity;
        Level = level;
        WaterLevel = waterLevel;
        SunlightLevel = sunlightLevel;
        SoilType = soilType;
        QuizCompleted = quizCompleted;
        FamiliarityToNextLevel = 3;
    }


    public Plantomo(int id, string name, GameObject prefab, int plantID, string description = null,
        int familiarity = 0, int level = 1, int waterLevel = 0, int sunlightLevel = 0, string soilType = null, bool quizCompleted = false)
        : this(id, name, description, plantID, familiarity, level, waterLevel, sunlightLevel, soilType, quizCompleted)
    {
        this.PlantomoPrefab = prefab;
        FamiliarityToNextLevel = 3;
}


    public void GainFamiliarity(int familiarityToGain)
    {
        Familiarity += familiarityToGain;
        if (Familiarity >= FamiliarityToNextLevel)
        {
            SetLevel(Level + 1);
        }
    }

    public void SetLevel(int value)
    {
        Debug.Log("setting plantomo level");
        Level = value;
        Familiarity = 0;
        FamiliarityToNextLevel = 3;
    }
}