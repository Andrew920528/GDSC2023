using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RandomizeTip : MonoBehaviour
{
    public List<string> tipList = new List<string>
    {
        "Hint: Taking picture of different plants to get new Plantomo",
        "Hint: ABCD",
        "Hint: DEFG",
    };
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start called");
        int randomInt = Random.Range(0, tipList.Count);
        GetComponent<TMP_Text>().text = tipList[randomInt];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
