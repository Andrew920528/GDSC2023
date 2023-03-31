using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Firebase.Database;
using TMPro;

public class ActivityManager : MonoBehaviour
{
    private DataManager dataManager;
    // Start is called before the first frame update
    void Start()
    {
        dataManager = GameObject.FindObjectOfType<DataManager>();
        DataManager.Data gameData = dataManager.GetGameData();
        // Populate the activity frame with data

        GameObject[] activityList = GameObject.FindGameObjectsWithTag("ActivityData");

        foreach (GameObject g in activityList)
        {
            // TODO: Fix the activity data, only plantomo found working atm
            switch (g.name)
            {
                case "Distance":
                    g.transform.Find("Data").GetComponent<TMP_Text>().text = gameData.totalDistance.ToString();
                    break;
                case "Plantomo Found":
                    g.transform.Find("Data").GetComponent<TMP_Text>().text = gameData.plantomoID.ToString();
                    break;
                case "Quests Completed":
                    g.transform.Find("Data").GetComponent<TMP_Text>().text = gameData.plantomoID.ToString();
                    break;
                case "Quiz Completed":
                    g.transform.Find("Data").GetComponent<TMP_Text>().text = gameData.plantomoID.ToString();
                    break;
                case "Time Played":
                    g.transform.Find("Data").GetComponent<TMP_Text>().text = gameData.plantomoID.ToString();
                    break;
            }
        }
    }
}
