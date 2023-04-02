using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to any game object to initialize the static data.
/// </summary>
public class LoadStaticData : MonoBehaviour
{
    
    private void Awake()
    {
        var basePlantomoPath = "Prefab/Plantomo/";
        StaticData.plantomoList = new List<Plantomo>()
        {
            new Plantomo(0, "Northern Red Oak",
                Resources.Load(basePlantomoPath + "P northern red oak") as GameObject, 0,
                "The northern red oak is an oak tree in the red oak group." ),
            new Plantomo(1, "Star Magnolia",
                Resources.Load(basePlantomoPath + "P star magnolia") as GameObject, 1,
                "This is a description for Star Magnolia."),
            new Plantomo(2, "Southern Magnolia",
                Resources.Load(basePlantomoPath + "P southern magnolia") as GameObject, 2,
                "This is a description for Southern Magnolia."),
            new Plantomo(3, "Trident Maple",
                Resources.Load(basePlantomoPath + "P trident maple") as GameObject, 3,
                "This is a description for Trident Maple."),
            new Plantomo(4, "Slash Pine",
                Resources.Load(basePlantomoPath + "P slash pine") as GameObject, 4,
                "This is a description for Slash Pine."),
        };

        var baseItemPath = "Prefab/Items/";
        StaticData.itemList = new List<Item>()
        {
            new Item("Water", 1, 100, "The elixir of life. It quenches the thirst of any Plantomo.",
            Resources.Load(baseItemPath + "P water item") as GameObject),
            new Item("Soil", 1, 100, "Just a bag of soil.",
            Resources.Load(baseItemPath + "P soil item") as GameObject),
        };


        StaticData.plantomoDict = new Dictionary<string, Plantomo>
        {
            {"Northern Red Oak" , StaticData.plantomoList[0] },
            {"Quercus rubra", StaticData.plantomoList[0] },
            {"Star Magnolia" , StaticData.plantomoList[1]},
            {"Southern Magnolia" , StaticData.plantomoList[2]},
            {"Bull bay" , StaticData.plantomoList[2]}, // include aliases
            {"Trident Maple" , StaticData.plantomoList[3]},
            {"Slash Pine" , StaticData.plantomoList[4]},
            
        };


        // If we didn't get the necessary data from Firebase, we instantiate them here

        if (StaticData.plantomoInventory == null)
            StaticData.plantomoInventory = new List<Plantomo>();
        if (StaticData.itemInventory == null)
            StaticData.itemInventory = new Dictionary<string, int>();
        if (StaticData.PlayerStats == null)
            StaticData.PlayerStats = new PlayerStats();
        if (StaticData.QuestTracker == null)
            StaticData.QuestTracker = new List<QuestData>();
        if (StaticData.plantDict == null)
        {
        // Populate the plant dict somehow if firebase missed
        }
    }

}
