using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CreateCard : MonoBehaviour
{
    // Reference to the Prefab. Drag a Prefab into this field in the Inspector.
    public GameObject titleCard;
    public GameObject descriptionCard;
    public GameObject nameCard;
    public GameObject multiPurposeCard;
    public GameObject imageCard;
    public GameObject distrCard;
    public GameObject careCard;


    [SerializeField]
    private float titleHorOffset = 350;
    [SerializeField]
    private float titleVertOffset = -210;
    [SerializeField]
    private float descCardHorOffset = 350;
    [SerializeField]
    private float descCardVertOffset = -445;

    private string plantomoName;
    private Plantomo plantomoData;
    private Plant plantData;

    // This script will simply instantiate the Prefab when the game starts.
    void Start()
    {
        // Instantiate at position (0, 0, 0) and zero rotation.

        // get info about the plantomo whose wiki page we're looking at
        // we saved the selection in a static data file in CreateWikiEntry

        plantomoName = StaticData.SelectedPlantomo.Name;
        plantomoData = StaticData.plantomoDict[plantomoName];
        plantData = StaticData.plantDict[plantomoData.PlantID];

        CreateTitleCard(titleHorOffset, titleVertOffset);
        CreateDescriptionCard(descCardHorOffset, descCardVertOffset);
        CreateInfoCards();
        CreatePlantomoObject();
    }

    private void CreateTitleCard(float horizontalOffset, float verticalOffset)
    {
        GameObject tc = Instantiate(titleCard, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
        tc.transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0);
        tc.transform.localScale = new Vector3(1, 1, 1);

        tc.transform.Find("Plant Name").GetComponent<TMP_Text>().text = plantomoName;
    }

    private void CreateDescriptionCard(float horizontalOffset, float verticalOffset)
    {
        GameObject dc = Instantiate(descriptionCard, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
        dc.transform.localPosition = new Vector3(horizontalOffset, verticalOffset, 0);
        dc.transform.localScale = new Vector3(1, 1, 1);

        dc.transform.Find("Description").GetComponent<TMP_Text>().text = StaticData.SelectedPlantomo.Description;

    }

    private void CreateInfoCards()
    {
        GameObject nc = Instantiate(nameCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        nc.transform.localPosition = new Vector3(0, 0, 0);
        nc.transform.localScale = new Vector3(1, 1, 1);

        GameObject mpc = Instantiate(multiPurposeCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        mpc.transform.localPosition = new Vector3(0, 0, 0);
        mpc.transform.localScale = new Vector3(1, 1, 1);

        GameObject ic = Instantiate(imageCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        ic.transform.localPosition = new Vector3(0, 0, 0);
        ic.transform.localScale = new Vector3(1, 1, 1);

        GameObject distr = Instantiate(distrCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        distr.transform.localPosition = new Vector3(0, 0, 0);
        distr.transform.localScale = new Vector3(1, 1, 1);

        GameObject cc = Instantiate(careCard, new Vector3(0, 0, 0), Quaternion.identity, transform);
        careCard.transform.localPosition = new Vector3(0, 0, 0);
        careCard.transform.localScale = new Vector3(1, 1, 1);


        // Get info about the plant the plantomo is based on
        string scientificName = plantData == null ? "Scientific Name" : plantData.ScientificName;
        string scientificDesc = plantData == null ? "Plant Data Not Available" : plantData.Description;


        nc.transform.Find("Plant Name").GetComponent<TMP_Text>().text = name;
        nc.transform.Find("Scientific Name").GetComponent<TMP_Text>().text = scientificName;

        mpc.transform.Find("Text Body").GetComponent<TMP_Text>().text = scientificDesc;

        GameObject[] images = GameObject.FindGameObjectsWithTag("RawImage");
        for (int i = 0; i < images.Length; ++i)
        {
            StartCoroutine(PopulateFoundPage.DownloadImage(images[i], plantData.Images[i]));
        }
    }

    private void CreatePlantomoObject()
    {

        GameObject plantomo = StaticData.plantomoList[StaticData.SelectedPlantomo.Id].PlantomoPrefab;

        GameObject pc = Instantiate(plantomo, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
        pc.transform.localPosition = new Vector3(160, -225, 0);
        pc.transform.localScale = new Vector3(50, 50, 1);
    }
}