using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using Firebase.Database;
using TMPro;

public class ActivityManager : MonoBehaviour
{

    // Start is called before the first frame update
    void Start()
    {
        PlayerStats stats = StaticData.PlayerStats;
        // Populate the activity frame with data

        GameObject[] activityList = GameObject.FindGameObjectsWithTag("ActivityData");

        foreach (GameObject g in activityList)
        {
            // TODO: Fix the activity data, only plantomo found working atm
            switch (g.name)
            {
                case "Distance":
                    g.transform.Find("Data").GetComponentInChildren<TMP_Text>().text = "2.5km";
                    break;
                case "Plantomo Found":
                    g.transform.Find("Data").GetComponentInChildren<TMP_Text>().text = stats.PlantomosCollected.ToString();
                    break;
                case "Quests Completed":
                    g.transform.Find("Data").GetComponentInChildren<TMP_Text>().text = stats.QuestsCompleted.ToString();
                    break;
                case "Quiz Completed":
                    g.transform.Find("Data").GetComponentInChildren<TMP_Text>().text = stats.QuizCompleted.ToString();
                    break;
                case "Time Played":
                    g.transform.Find("Data").GetComponentInChildren<TMP_Text>().text = stats.TimePlayed.ToString();
                    break;
            }
        }
    }
}
