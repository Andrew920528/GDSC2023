using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopulatePot : MonoBehaviour
{
    
    public GameObject nameCard;
    public GameObject quizButtonPrefab;
    public TMP_Text levelField;
    [SerializeField]
    private int plantomoScale = 50;
    private DataManager dataManager;
    // Start is called before the first frame update

    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.

        // get info about the plantomo whose wiki page we're looking at
        // we saved the selection in a static data file in CreateWikiEntry
        


        GameObject.FindGameObjectWithTag("CoinCount").GetComponent<TMP_Text>().SetText(StaticData.Coins.ToString());

        if (StaticData.SelectedPlantomo == null)
        {
            nameCard.GetComponent<TMP_Text>().text = "This pot is empty.";
            levelField.SetText("");
            return;
        }

        
        Plantomo plantomoData = StaticData.plantomoList[StaticData.SelectedPlantomo.Id];
        string name = plantomoData.Name;

        Plant plantData = StaticData.plantDict[plantomoData.PlantID];


        nameCard.GetComponent<TMP_Text>().text = name;

        GameObject plantomo = plantomoData.PlantomoPrefab;

        GameObject pc = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
        pc.transform.localPosition = new Vector3(0, 0, 0);
        pc.transform.localScale = new Vector3(plantomoScale, plantomoScale, 1);




        levelField.SetText("lvl. " + StaticData.plantomoInventory[StaticData.SelectedPotIndex].Level);


        if (quizButtonPrefab != null)
        {
            quizButtonPrefab.SetActive(true);
        }

    }
}
