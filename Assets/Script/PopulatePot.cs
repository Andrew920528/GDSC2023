using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulatePot : MonoBehaviour
{
    public List<GameObject> plantomos = new List<GameObject>();
    public GameObject nameCard;
    [SerializeField]
    private int plantomoScale = 50;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.

        // get info about the plantomo whose wiki page we're looking at
        // we saved the selection in a static data file in CreateWikiEntry
        string name = StaticData.SelectedPlantomo;

        if (name == null)
        {
            nameCard.GetComponent<TMP_Text>().text = "This pot is empty.";
            return;
        }
        Plantomo plantomoData = StaticData.plantomoDict[name];
        Plant plantData = plantomoData.GetPlant();

        nameCard.GetComponent<TMP_Text>().text = name;

        GameObject plantomo = plantomos[plantomoData.GetID()];

        GameObject pc = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
        pc.transform.localPosition = new Vector3(0, 0, 0);
        pc.transform.localScale = new Vector3(plantomoScale, plantomoScale, 1);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
