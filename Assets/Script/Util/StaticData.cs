using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StaticData
{
    // amount of coins you have to purchase actions for plantomos
    public static int Coins { get; set; }
    // for selecting plantomo in wiki page
    public static string SelectedPlantomo { get; set; }
    // for selecting plantomo in pot page
    public static int SelectedPlantomoIndex { get; set; }

    public static List<Plantomo> plantomoInventory = new List<Plantomo>();

    public static List<Plantomo> plantomoList = new List<Plantomo>()
    {
        new Plantomo(0, "Northern Red Oak", "This is a description for Northern Red Oak." ),
        new Plantomo(1, "Star Magnolia", "This is a description for Star Magnolia."),
        new Plantomo(2, "Southern Magnolia", "This is a description for Southern Magnolia."),
        new Plantomo(3, "Trident Maple", "This is a description for Trident Maple."),
        new Plantomo(4, "Slash Pine", "This is a description for Slash Pine."),
    };

    public static Dictionary<string, Plantomo> plantomoDict = new Dictionary<string, Plantomo>
    {
        {"Northern Red Oak" , plantomoList[0] },
        {"Star Magnolia" , plantomoList[1]},
        {"Southern Magnolia" , plantomoList[2]},
        {"Trident Maple" , plantomoList[3]},
        {"Slash Pine" , plantomoList[4]},
        {"Quercus", plantomoList[0] },
    };


    // Storing API response
    public static Root plantInfo;
    public static byte[] plantImage;
    

}
