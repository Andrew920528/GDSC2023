using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulatePot : MonoBehaviour
{
    public List<GameObject> plantomos;
    public GameObject nameCard;
    // Start is called before the first frame update
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.
        GameObject nc = Instantiate(nameCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        nc.transform.localPosition = new Vector3(0, 0, 0);
        nc.transform.localScale = new Vector3(1, 1, 1);

        // get info about the plantomo whose wiki page we're looking at
        // we saved the selection in a static data file in CreateWikiEntry
        string name = StaticData.SelectedPlantomo;
        Plantomo plantomoData = StaticData.plantomoDict[name];
        Plant plantData = plantomoData.GetPlant();

        nc.transform.Find("Plant Name").GetComponent<TMP_Text>().text = name;

        GameObject plantomo = plantomos[plantomoData.GetID()];

        GameObject pc = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
        pc.transform.localPosition = new Vector3(0, 0, 0);
        pc.transform.localScale = new Vector3(50, 50, 1);


    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
