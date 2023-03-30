using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to any game object to initialize the static data.
/// </summary>
public class LoadStaticData : MonoBehaviour
{
    
    void Awake()
    {
        var basePlantomoPath = "Prefab/Plantomo/";
        StaticData.plantomoList = new List<Plantomo>()
        {
            new Plantomo(0, "Northern Red Oak",
                Resources.Load(basePlantomoPath + "P northern red oak") as GameObject,
                "This is a description for Northern Red Oak." ),
            new Plantomo(1, "Star Magnolia",
                Resources.Load(basePlantomoPath + "P star magnolia") as GameObject,
                "This is a description for Star Magnolia."),
            new Plantomo(2, "Southern Magnolia",
                Resources.Load(basePlantomoPath + "P southern magnolia") as GameObject,
                "This is a description for Southern Magnolia."),
            new Plantomo(3, "Trident Maple",
                Resources.Load(basePlantomoPath + "P trident maple") as GameObject,
                "This is a description for Trident Maple."),
            new Plantomo(4, "Slash Pine",
                Resources.Load(basePlantomoPath + "P slash pine") as GameObject,
                "This is a description for Slash Pine."),
        };


        StaticData.plantomoDict = new Dictionary<string, Plantomo>
        {
            {"Northern Red Oak" , StaticData.plantomoList[0] },
            {"Star Magnolia" , StaticData.plantomoList[1]},
            {"Southern Magnolia" , StaticData.plantomoList[2]},
            {"Trident Maple" , StaticData.plantomoList[3]},
            {"Slash Pine" , StaticData.plantomoList[4]},
            {"Quercus", StaticData.plantomoList[0] },
        };
    }


}
