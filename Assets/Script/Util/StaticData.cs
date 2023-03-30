using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StaticData
{
    // amount of coins you have to purchase actions for plantomos
    public static int Coins { get; set; }
    // for selecting plantomo in wiki page
    public static Plantomo SelectedPlantomo { get; set; }
    // for selecting plantomo in pot page
    public static int SelectedPotIndex { get; set; }

    public static List<Plantomo> plantomoInventory = new List<Plantomo>();

    public static List<Plantomo> plantomoList;


    // useful for mapping api result to plantomo
    public static Dictionary<string, Plantomo> plantomoDict; 


    // Storing API response
    public static Root plantInfo;
    public static byte[] plantImage;

}
