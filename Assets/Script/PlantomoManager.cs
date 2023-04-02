using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantomoManager : MonoBehaviour
{
    // Class for managing the currently selected plantomo in the Pot Page.
    public Plantomo CurrentPlantomo { get; set; }
    public CareGuide CareGuide { get; set; }

    // Fetch these values from the plantomo data, they're the current Plantomo's ideal values
    public string WaterGuide { get; set; }
    public string SunlightGuide { get; set; }
    public string SoilGuide { get; set; }
    public string HardinessGuide { get; set; }

    // Keep level and familiarity for each plantomo.
    // If we reach 3 familiarity, we go to the next level.
    public int Level { get; set; }
    public int Familiarity { get; set; }

    // You gain familiarity by watering plants and changing their soil.
    // You can also lose familiarity if you don't water them or don't change their soil.
    public int WaterLevel { get; set; }
    public int MaxWaterLevel { get; set; }
    public int SunlightLevel { get; set; }
    public string SoilType { get; set; }

    // This is the number of days a Plantomo can survive without water. Each day consumes one.
    // You can only water each Plantomo every set period of time, maybe 30 minutes.
    private Dictionary<string, int> MaxWaterLevelDict = new Dictionary<string, int>()
    {
        {"low", 10},
        {"medium", 7},
        {"high", 3},
    };

 
    private void Awake()
    {
        CurrentPlantomo = StaticData.SelectedPlantomo;

        if (CurrentPlantomo == null)
        {
            return;
        }
        CareGuide = StaticData.plantDict[CurrentPlantomo.PlantID].CareGuide;
        WaterGuide = CareGuide.Water;
        SunlightGuide = CareGuide.Sunlight;
        SoilGuide = CareGuide.Soil;
        HardinessGuide = CareGuide.Hardiness;

        // Store the values from the inventory for this plantomo

        Plantomo plantomoData = StaticData.plantomoInventory[StaticData.SelectedPotIndex];
        Level = plantomoData.Level;
        Familiarity = plantomoData.Familiarity;
        WaterLevel = plantomoData.WaterLevel;
        MaxWaterLevel = MaxWaterLevelDict[WaterGuide];
        SunlightLevel = plantomoData.SunlightLevel;
        SoilType = plantomoData.SoilType;

    }

    public void WaterPlant()
    {
        if (CurrentPlantomo == null)
        {
            return;
        }
        if (WaterLevel == MaxWaterLevel)
        {
            Debug.Log("Are you sure? The Plantomo is already full.");
            // If the user goes through with it, decrease familiarity level.
        }
        if (StaticData.itemInventory["Water"] <= 0)
        {
            Debug.Log("We're out of water! Buy more in the shop.");
        }
        CurrentPlantomo.WaterLevel++;
        StaticData.itemInventory["Water"]--;
        InventoryManager.instance.UpdateItems();
        WaterLevel++;
    }

    public void ChangeSoil(string soil)
    {
        if (CurrentPlantomo == null)
        {
            return;
        }
        if (soil != SoilType)
        {
            Debug.Log("Are you sure? The plant will grow slower in this soil.");
        }

    }

    public void AlertUser()
    {
        if (WaterLevel == 0)
        {
            Debug.Log("Plantomo needs watering!");
        }
        if (SoilType != SoilGuide)
        {
            Debug.Log("Plantomo needs change of home...");
        }
    }
}
