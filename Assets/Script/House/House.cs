using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class House : MonoBehaviour
{
    private string plantomoID = "";
    private string assetID = "";
    private bool isOwned = false;
    private int wateringPeriod = 0;//how often (in hours) it shoudl be watered
    private int watering = 0; //how many times it should be watered before owned
    //we'll have to design this
    private List<string> missions = new List<string>();

    //check if it's owned depending on the wateringperiod
    public void checkIsOwned()
    {
        if(watering == 0)
        {
            isOwned = true;
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
