using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Plantomo
{
    [JsonProperty]
    public int Id { get; set; }
    [JsonProperty]
    public string Name { get; set; }
    [JsonProperty]
    public string Description { get; set; }
    [JsonProperty]
    public Plant Plant { get; set; }
    [JsonProperty]
    public float Familiarity { get; set; }
    [JsonProperty]
    public int Level { get; set; }
    [JsonProperty]
    private float FamiliarityToNextLevel = 10;

    // Plantomo gameobject (sprite + animation)
    public GameObject PlantomoPrefab { get; }

    public Plantomo()
    {
        Id = 0;
        Name = null;
        Familiarity = 0;
        Level = 1;
    }

    public Plantomo(Plantomo plantomo)
    {
        Id = plantomo.Id;
        Name = plantomo.Name;
        Description = plantomo.Description;
        Plant = plantomo.Plant;
        Familiarity = plantomo.Familiarity;
        Level = plantomo.Level;
    }

    public Plantomo(int id, string name)
    {
        Id = id;
        Name = name;
        Familiarity = 0;
        Level = 1;
    }


    public Plantomo(int id, string name, string description = null, Plant plant = null, float familiarity = 0, int level = 1)
    {
        Id = id;
        Name = name;
        Description = description;
        Plant = plant;
        Familiarity = familiarity;
        Level = level;
    }

    public Plantomo(int id, string name, GameObject prefab, string description = null, Plant plant = null, float familiarity = 0, int level = 1)
        : this(id, name, description, plant, familiarity, level)
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