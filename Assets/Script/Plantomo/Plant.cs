using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class Plant
{
    [FirestoreProperty("ID")]
    public int Index { get; set; }
    [FirestoreProperty("CommonName")]
    public string CommonName { get; set; }
    [FirestoreProperty("SpeciesName")]
    public string ScientificName { get; set; }
    [FirestoreProperty("Nativity")]
    public string[] Distribution { get; set; }
    [FirestoreProperty("Climate")]
    public string[] Climates { get; set; }
    [FirestoreProperty("Description")]
    public string Description { get; set; }
    [FirestoreProperty("Image")]
    public string[] Images { get; set; }
    [FirestoreProperty("CareGuide")]
    public CareGuide CareGuide { get; set; }
    [FirestoreProperty("Edible")]
    public bool Edible { get; set; }
    [FirestoreProperty("EdibleClariffication")]
    public string EdibleClarification { get; set; }
    [FirestoreProperty("LifeSpan")]
    public string Lifespan { get; set; }
    [FirestoreProperty("GrowthRate")]
    public string GrowthRate { get; set; }
    [FirestoreProperty("Poisonous")]
    public bool Poisonous { get; set; }
    [FirestoreProperty("ReproductionMethod")]
    public string[] ReproductionMethod { get; set; }
    [FirestoreProperty("Season")]
    public string Season { get; set; }


    public Plant(int index, string commonName, string scientificName)
    {
        Index = index;
        CommonName = commonName;
        ScientificName = scientificName;
    }

    [JsonConstructor]
    public Plant(int index, string commonName, string scientificName, string[] distribution = null,
        string[] climates = null, string description = null, string[] images = null, CareGuide careGuide = null,
        bool edible = false, string edibleClarification = null, string lifeSpan = null, string growthRate = null,
        bool poisonous = false, string[] reproductionMethod = null, string season = null)
    {
        Index = index;
        CommonName = commonName;
        ScientificName = scientificName;
        Description = description;
        Distribution = distribution;
        Climates = climates;
        Images = images;
        CareGuide = careGuide;
        Edible = edible;
        EdibleClarification = edibleClarification;
        Lifespan = lifeSpan;
        GrowthRate = growthRate;
        Poisonous = poisonous;
        ReproductionMethod = reproductionMethod;
        Season = season;
    }

    public Plant(int index, string commonName, string scientificName, string description)
    {
        Index = index;
        CommonName = commonName;
        ScientificName = scientificName;
        Description = description;
    }


    public void SetIndex(int index)
    {
        Index = index;
    }


}