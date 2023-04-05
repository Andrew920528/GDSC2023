using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizeTip : MonoBehaviour
{
    public List<string> tipList = new List<string>
    {
        "Hint: Taking picture of different plants to get new Plantomo",
        "Hint: Water your Plantomos to gain familiarity.",
        "Hint: Go on quests to discover more Plantomos!",
        "Did you know? We use Firebase as our backend.",
        "Hint: Complete Quests to level up!",
        "Hint: After you meet a Plantomo, you can find them in the village.",
        "Hint: You can change your settings in the activity page.",


    };
    // Start is called before the first frame update
    void Start()
    {
        int randomInt = Random.Range(0, tipList.Count);
        GetComponent<TMP_Text>().text = tipList[randomInt];
    }
}
