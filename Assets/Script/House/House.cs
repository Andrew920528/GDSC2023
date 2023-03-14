using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House
{
    [SerializeField] private string plantomoID = "";
    [SerializeField] private string assetID = "";
    [SerializeField] private bool isOwned = false;
    [SerializeField] private int wateringPeriod = 0;//how often (in hours) it shoudl be watered
    [SerializeField] private int watering = 0; //how many times it should be watered before owned
    //we'll have to design this
    [SerializeField] private List<string> missions = new List<string>();

    //making the object
    public House(string plantomoID, int watering, int wateringPeriod) {
        //still have to make this bigger
        this.plantomoID = plantomoID;
        this.watering = watering;
        this.wateringPeriod = wateringPeriod;
    }

    //method for it to generate some sort of mission
    public string generateMission()
    {
        string result = "";
        Random rnd = new Random();
        int num = rnd.Next(0, missions.length());
        result = missions.GetEnumerator(num);
        return result;
    }

    //check if it's owned depending on the wateringperiod
    public void checkIsOwned()
    {
        if(watering == 0)
        {
            isOwned == true;
        } else
        {
            return;
        }
    }

    //watering the pot house
    public void water()
    {
        watering--;
    }

    //we would need another method to prompt the user
    //to water the pot after the watering period

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public override string ToString()
    {
        return base.ToString();
    }
}
