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
    public float Familiarity { get; set; }
    [SerializeField]
    public int Level { get; set; }
    [SerializeField]
    private float FamiliarityToNextLevel = 10;

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
    }

    //public Plantomo(int id, string name)
    //{
    //    Id = id;
    //    Name = name;
    //    Familiarity = 0;
    //    Level = 1;
    //}


    public Plantomo(int id, string name, string description = null, int plantID = 0, float familiarity = 0, int level = 1)
    {
        Id = id;
        Name = name;
        Description = description;
        PlantID = plantID;
        Familiarity = familiarity;
        Level = level;
    }


    public Plantomo(int id, string name, GameObject prefab, int plantID, string description = null, float familiarity = 0, int level = 1)
        : this(id, name, description, plantID, familiarity, level)
    {
        this.PlantomoPrefab = prefab;
    }


    public void GainFamiliarity(float familiarityToGain)
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
        Familiarity = Familiarity - FamiliarityToNextLevel;
        FamiliarityToNextLevel = (int)(10f * (Mathf.Pow(Level + 1, 2) - (5 * (Level + 1)) + 8));
    }
}