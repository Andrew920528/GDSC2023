using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant
{
    private int _id;
    private string _commonName;
    private string _scientificName;
    private string[] _distribution;
    private string[] _climates;
    private string _description;
    private string[] _images;
    private CareGuide _careGuide;
    private bool edible;
    private string edibleClarification;
    private string lifeSpan;


    public int GetID()
    {
        return _id;
    }

    public string GetCommonName()
    {
        return _commonName;
    }

    public string GetScientificName()
    {
        return _scientificName;
    }

    public string[] GetDistribution()
    {
        return _distribution;
    }

    public string[] GetClimates()
    {
        return _climates;
    }

    public string[] GetImages()
    {
        return _images;
    }

    public string GetDescription()
    {
        return _description;
    }
}
