using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantomoManager : MonoBehaviour
{
    // Class for managing the currently selected plantomo in the Pot Page.
    private Plantomo currentPlantomo;
    private CareGuide careGuide;

    // Fetch these values from the plantomo data, they're the current Plantomo's ideal values
    private string waterGuide;
    private string sunlightGuide;
    private string soilGuide;
    private string hardinessGuide;

    private int waterLevel;
    private int sunlightLevel;
    private int soilLevel;

 
    private void Awake()
    {
        currentPlantomo = StaticData.SelectedPlantomo;

        if (currentPlantomo == null)
        {
            return;
        }
        careGuide = StaticData.plantDict[currentPlantomo.PlantID].CareGuide;
        waterGuide = careGuide.Water;
        sunlightGuide = careGuide.Sunlight;
        soilGuide = careGuide.Soil;
        hardinessGuide = careGuide.Hardiness;

        waterLevel = StaticData.plantomoInventory[StaticData.SelectedPotIndex].WaterLevel;
        sunlightLevel = StaticData.plantomoInventory[StaticData.SelectedPotIndex].SunlightLevel;
        soilLevel = StaticData.plantomoInventory[StaticData.SelectedPotIndex].SoilLevel;
    }

    public void Water()
    {

    }

    public void Sunlight()
    {

    }

    public void Soil()
    {

    }
}
