using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public static class StaticData
{
    // The username of Firebase user
    public static string username { get; set; }
    // amount of coins you have to purchase actions for plantomos

    // for keeping track of player stats
    public static PlayerStats PlayerStats = new PlayerStats();

    public static List<QuestData> QuestTracker;
    // for selecting plantomo in wiki page
    public static Plantomo SelectedPlantomo { get; set; }
    // for selecting plantomo in pot page
    public static int SelectedPotIndex { get; set; }

    public static List<Plantomo> plantomoInventory = new List<Plantomo>();

    public static List<Plantomo> plantomoList;


    // useful for mapping api result to plantomo
    public static Dictionary<string, Plantomo> plantomoDict;

    public static Dictionary<int, Plant> plantDict = new Dictionary<int, Plant>();

    // map of item name and quantity
    public static Dictionary<string, int> itemInventory = new Dictionary<string, int>()
    {
        { "Water", 0 },
        { "Sunlight", 0 },
        { "Soil", 0 },
        {"Coins", 0 },
    };

    public static List<Item> itemList = new List<Item>()
    {
        new Item("Water", 1, 100, "The elixir of life. It quenches the thirst of any Plantomo."),
        new Item("Sunlight", 1, 100, "The sunlight from heaven. It gives the plantomos the best photosynthesis of their lives."),
        new Item("Soil", 1, 100, "Just a bag of soil."),
    };


    // Storing API response
    public static Root plantInfo;
    public static byte[] plantImage;

}
