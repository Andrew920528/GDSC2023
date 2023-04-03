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
            new Plantomo(0, "Northern red oak",
                Resources.Load(basePlantomoPath + "P northern red oak") as GameObject, 0,
                "The northern red oak is an oak tree in the red oak group." ),
            new Plantomo(1, "Star magnolia",
                Resources.Load(basePlantomoPath + "P star magnolia") as GameObject, 1,
                "This is a description for Star Magnolia."),
            new Plantomo(2, "Southern magnolia",
                Resources.Load(basePlantomoPath + "P southern magnolia") as GameObject, 2,
                "This is a description for Southern Magnolia."),
            new Plantomo(3, "Trident maple",
                Resources.Load(basePlantomoPath + "P trident maple") as GameObject, 3,
                "This is a description for Trident Maple."),
            new Plantomo(4, "Slash pine",
                Resources.Load(basePlantomoPath + "P slash pine") as GameObject, 4,
                "This is a description for Slash Pine."),
        };

        var baseItemPath = "Prefab/Items/";
        StaticData.itemList = new List<Item>()
        {
            new Item("Water", 1, 100, "The elixir of life. It quenches the thirst of any Plantomo.",
            Resources.Load(baseItemPath + "Water") as GameObject),
            new Item("Soil", 1, 100, "Just a bag of soil.",
            Resources.Load(baseItemPath + "Soil") as GameObject),
        };

        // include aliases for each plant, from plantnet
        StaticData.plantomoDict = new Dictionary<string, Plantomo>
        {
            {"Northern red oak" , StaticData.plantomoList[0] },
            {"American red oak" , StaticData.plantomoList[0] },
            {"Red Oak" , StaticData.plantomoList[0] },
            {"Star magnolia" , StaticData.plantomoList[1]},
            {"Japanese Star Magnolia" , StaticData.plantomoList[1]},
            {"Royal Star" , StaticData.plantomoList[1]},
            {"Southern magnolia" , StaticData.plantomoList[2]},
            {"Evergreen Magnolia" , StaticData.plantomoList[2]},
            {"Bull bay" , StaticData.plantomoList[2]}, 
            {"Trident maple" , StaticData.plantomoList[3]},
            {"Three-toothed maple" , StaticData.plantomoList[3]},
            {"Taiwan Trident Maple" , StaticData.plantomoList[3]},
            {"Longleaf pitch pine" , StaticData.plantomoList[4]},
            {"Slash pine" , StaticData.plantomoList[4]},
            {"Swamp Pine" , StaticData.plantomoList[4]},
            {"American pitch pine" , StaticData.plantomoList[4]},
        };

        StaticData.plantMap = new Dictionary<string, string>
        {
            {"American red oak", "Northern red oak" },
            {"Red Oak", "Northern red oak" },
            {"Japanese Star Magnolia", "Star magnolia" },
            {"Royal Star", "Star magnolia" },
            {"Evergreen Magnolia", "Southern magnolia" },
            {"Bull bay", "Southern magnolia" },
            {"Three-toothed maple", "Trident maple" },
            {"Taiwan Trident Maple", "Trident maple" },
            {"Swamp Pine", "Slash pine" },
            {"American pitch pine", "Slash pine" },
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
