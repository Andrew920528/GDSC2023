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
    private Image resultImage;
    private RawImage queryImageField;
    private byte[] queryImage;
    private TMP_Text plantNameField;
    private TMP_Text descriptionField;


    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        plantInfo = gameManager.PlantInfo;

        Debug.Log(gameManager.PlantInfo);
        //plantImage = GameObject.Find("Response Image").GetComponent<RawImage>();
        queryImageField = GameObject.FindGameObjectWithTag("QueryImage").GetComponent<RawImage>();
        plantNameField = GameObject.Find("Name").GetComponent<TMP_Text>();
        descriptionField = GameObject.Find("Description").GetComponent<TMP_Text>();

        queryImage = gameManager.PlantImage;

        Texture2D queryTexture = new Texture2D(1, 1);

        queryTexture.LoadImage(queryImage);

        queryImageField.texture = queryTexture;

        Debug.Log(gameManager.ResultImage);
        resultImage = gameManager.ResultImage;



        Debug.Log(plantInfo.results);
        plantNameField.text = plantInfo.results[0].species.commonNames.Count == 0 ? "No common name :(" : plantInfo.results[0].species.commonNames[0];
        descriptionField.text = plantInfo.results[0].species.genus.scientificName;

        EventManager.Instance.QueueEvent(new GameEvent.ScanningGameEvent(plantNameField.text));
    }
}

