using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class Plantomo
{
    [JsonProperty]
    private int _id;
    [JsonProperty]
    private string _name;
    [JsonProperty]
    private string _description;
    [JsonProperty]
    private Plant _plant;

    public Plantomo()
    {
        _id = 0;
        _name = null;
    }

    public Plantomo(Plantomo plantomo)
    {
        _id = plantomo.GetID();
        _name = plantomo.GetName();
        _description = plantomo.GetDescription();
        _plant = plantomo.GetPlant();
    }

    public Plantomo(int id, string name)
    {
        _id = id;
        _name = name;
    }

 
    public Plantomo(int id, string name, string description = null, Plant plant = null)
    {
        _id = id;
        _name = name;
        _description = description;
        _plant = plant;
    }

    public int GetID()
    {
        return _id;
    }

    public string GetName()
    {
        return _name;
    }

    public string GetDescription()
    {
        return _description;
    }


    public Plant GetPlant()
    {
        return _plant;
    }
}
