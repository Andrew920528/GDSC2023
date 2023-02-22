using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using Newtonsoft.Json;
using TMPro;

public class Root
{
    public Query query { get; set; }
    public string language { get; set; }
    public string preferedReferential { get; set; }
    public string switchToProject { get; set; }
    public string bestMatch { get; set; }
    public List<Result> results { get; set; }
    public int remainingIdentificationRequests { get; set; }
    public string version { get; set; }
}

public class Result
{
    public float score { get; set; }
    public Species species { get; set; }
    public List<Image> images { get; set; }
    public Gbif gbif { get; set; }
}

public class Query
{
    public string project { get; set; }
    public List<string> images { get; set; }
    public List<string> organs { get; set; }
    public bool includeRelatedImages { get; set; }
}

public class Genus
{
    public string scientificNameWithoutAuthor { get; set; }
    public string scientificNameAuthorship { get; set; }
    public string scientificName { get; set; }
}

public class Gbif
{
    public int id { get; set; }
}

public class Species
{
    public string scientificNameWithoutAuthor { get; set; }
    public string scientificNameAuthorship { get; set; }
    public string scientificName { get; set; }
    public Genus genus { get; set; }
    public List<string> commonNames { get; set; }
}

public class Image
{
    public static Image FromFile { get; internal set; }
    public Url url { get; set; }

    internal static void FromStream(MemoryStream ms)
    {
        throw new NotImplementedException();
    }
}

public class Url
{
    public string o { get; set; }
    public string m { get; set; }
    public string s { get; set; }
}


public class GetPlantData : MonoBehaviour
{
    [SerializeField]
    private int plantInfoScene;
    //TMP_InputField outputArea;
    byte[] byteArray;
    ScreenshotHandler screenshotHandler;
    private GameManager gameManager;

    void Start()
    {
        //outputArea = GameObject.Find("OutputArea").GetComponent<TMP_InputField>();
        // for testing
        //GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetPlantInfo);
        gameManager = GameObject.FindObjectOfType<GameManager>();
        screenshotHandler = GameObject.FindObjectOfType<ScreenshotHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private const string API_KEY = "2b10RIOZXAmYw1L53Jxov8Fe";
    private static string PROJECT = "all";

    private string urlParameters = String.Format("?api-key={0}", API_KEY);

    private string URL = "https://my-api.plantnet.org/v2/identify/" + PROJECT + String.Format("?api-key={0}", API_KEY);

    public void GetPlantInfo(byte[] plantImage) => StartCoroutine(GetPlantInfo_Coroutine(plantImage));

    public IEnumerator GetPlantInfo_Coroutine(byte[] plantImage)
    {
        //outputArea.text = "Plant info goes here";
        WWWForm form = new WWWForm();
        // adds the image to the API request form
        form.AddBinaryData("images", plantImage);
        form.AddField("organs", "fruit");
        using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
        {
            // sends the web request
            yield return request.SendWebRequest();
            // log errors if there is one
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                //outputArea.text = request.error;
                Debug.Log($"{request.error}: {request.downloadHandler.text}");
            }
            else
            {
                // parse results
                Root root = JsonConvert.DeserializeObject<Root>(request.downloadHandler.text);
                List<Result> results = root.results;
                Result result = results[0];
                Species species = result.species;
                string commonName = species.commonNames[0];
                string scientificName = species.scientificName;
                List<Image> images = result.images;

                Debug.Log(String.Format("Common Name: {0}, Scientific Name: {1}", commonName, scientificName));
                //outputArea.text = String.Format("Common Name: {0}, Scientific Name: {1}", commonName, scientificName);

                // save the plant info to game manager
                gameManager.PlantInfo = root;
                gameManager.PlantImage = plantImage;
                GameObject.FindObjectOfType<ChangeScene>().MoveToScene(plantInfoScene);
            }
            yield break;
        }
    }

}
