using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Drawing;
using System.IO;

public class PlantInfoManager : MonoBehaviour
{
    private GameManager gameManager;
    private Root plantInfo;
    private RawImage plantImage;
    private RawImage queryImageField;
    private byte[] queryImage;
    private TMP_Text plantNameField;
    private TMP_Text descriptionField;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        Debug.Log(gameManager.PlantInfo);
        plantInfo = gameManager.PlantInfo;

        Debug.Log(gameManager.PlantInfo);
        //plantImage = GameObject.Find("Response Image").GetComponent<RawImage>();
        queryImageField = GameObject.FindGameObjectWithTag("QueryImage").GetComponent<RawImage>();
        plantNameField = GameObject.Find("Name").GetComponent<TMP_Text>();
        descriptionField = GameObject.Find("Description").GetComponent<TMP_Text>();

        queryImage = gameManager.PlantImage;
        Debug.Log(plantInfo);


        Texture2D renderResult = new Texture2D(1, 1);
        Debug.Log(plantInfo);

        renderResult.LoadImage(queryImage);
        Debug.Log(plantInfo);

        queryImageField.texture = renderResult;

        
        Debug.Log(plantInfo.results);
        plantNameField.text = plantInfo.results[0].species.commonNames[0];
        descriptionField.text = plantInfo.results[0].species.genus.scientificName;

    }
}

