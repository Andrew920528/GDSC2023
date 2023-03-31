using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlantomoManager : MonoBehaviour
{
    // Class for managing the currently selected plantomo in the Pot Page.
    private Plantomo currentPlantomo;
    private CareGuide careGuide;

 
    private void Awake()
    {
        currentPlantomo = StaticData.SelectedPlantomo;

        if (currentPlantomo == null)
        {
            return;
        }
        careGuide = StaticData.plantDict[currentPlantomo.PlantID].CareGuide;
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
