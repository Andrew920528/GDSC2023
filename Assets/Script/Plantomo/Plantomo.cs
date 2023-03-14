using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plantomo
{
    private int _id;

    private string _name;

    private string _description;

    private Plant _plant;

    public Plantomo(int id, string name, string description, Plant plant = null)
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
