using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using System.Globalization;
using UnityEngine.Networking;
using Newtonsoft.Json;
using System.Web;

public class PopulateFoundPage : MonoBehaviour
{
    private GameObject gameManager;
    private Root plantInfo;
    private Result result;
    private List<PlantImage> images;
    public List<GameObject> plantomos;
    public GameObject plantomoPlaceholder;

    private LevelSystem levelSystem;
    public int captureExperience;

    private CheckoutPlantomo checkoutButton;

    // Creates a TextInfo based on the "en-US" culture.
    //private TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

    private GameObject descriptionBox;
    private TMP_Text plantNameText;
    [SerializeField]
    private int plantomoScale = 30;

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager");
        levelSystem = gameManager.GetComponent<LevelSystem>();
        plantInfo = StaticData.plantInfo;
        result = plantInfo.results[0];
        images = result.images;

        plantNameText = GameObject.FindGameObjectWithTag("PlantName").GetComponent<TMP_Text>();

        checkoutButton = GameObject.FindObjectOfType<CheckoutPlantomo>();

        string commonName = GetPlantName();
        if (StaticData.plantMap.ContainsKey(commonName))
        {
            commonName = StaticData.plantMap[commonName];
        }
        plantNameText.text = commonName;

        // If it's not a plantomo, generate image then return.
        if (!StaticData.plantomoDict.ContainsKey(commonName))
        {
            GameObject pc = Instantiate(plantomoPlaceholder, new Vector3(0, 0, 0), Quaternion.identity, transform);
            pc.transform.localPosition = new Vector3(0, 300, 0);
            pc.transform.localScale = new Vector3(1f, 1f, 1);

            gameObject.transform.Find("DescriptionCard").Find("Description")
                .GetComponent<TMP_Text>().text = "Description not available...";
            StartCoroutine(DownloadImage(pc, images[0].url.m));

            checkoutButton.Checkout();

            levelSystem.AddExperience(captureExperience);

            GetWikiInfo(commonName);

        }
        else
        {
            gameObject.transform.Find("DescriptionCard").Find("Description").
                GetComponent<TMP_Text>().text = StaticData.plantomoDict[commonName].Description;


            Plantomo plantomoData = StaticData.plantomoDict[commonName];

            //Plant plantData = StaticData.plantDict[plantomoData.PlantID];

            GameObject plantomoObj = Instantiate(plantomoData.PlantomoPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
            plantomoObj.transform.localPosition = new Vector3(0, 300, 0);
            plantomoObj.transform.localScale = new Vector3(plantomoScale, plantomoScale, 1);

            checkoutButton.Checkout(commonName);

            if (StaticData.plantomoInventory != null)
            {
                StaticData.plantomoInventory.Add(new Plantomo(plantomoData));
            }
            else
            {
                StaticData.plantomoInventory = new List<Plantomo>
                {
                    new Plantomo(plantomoData)
                };
            }

            // Multiply capture bonus for catching plantomos
            captureExperience *= 10;

            // Increment player stat
            StaticData.PlayerStats.PlantomosCollected++;
        }

        levelSystem.AddExperience(captureExperience);
    }

    private string GetPlantName()
    {
        // If there isn't a common name, set the scientific name as the name.
        string finalName = result.species.scientificName;
        // If there is a common name list, iterate through them and look for plantomo name
        Debug.Log(result.species.commonNames);

        foreach (string name in result.species.commonNames)
        {
            if (StaticData.plantomoDict.ContainsKey(name))
            {
                finalName = name;
            }
        }
        
        return finalName;
    }


    private string URL = "https://en.wikipedia.org/w/api.php?action=opensearch&limit=1&namespace=0";

    public void GetWikiInfo(string query) => StartCoroutine(ContinueGetWikiInfo(query));



    public IEnumerator ContinueGetWikiInfo(string query)
    {

        query = query.Split(" (")[0];

        var uriBuilder = new UriBuilder(URL);

        var q = HttpUtility.ParseQueryString(uriBuilder.Query);

        q["search"] = query;

        uriBuilder.Query = q.ToString();
        URL = uriBuilder.ToString();
        string title = "";

        using (UnityWebRequest request = UnityWebRequest.Get(URL))
        {
            // sends the web request
            yield return request.SendWebRequest();


            // log errors if there is one
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                //outputArea.text = request.error;
                Debug.Log($"{request.error}: {request.downloadHandler.text}");
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                string result = request.downloadHandler.text;
                if (!result.Contains("wiki"))
                {
                    yield break;
                }

                title = result.Split("/wiki/")[1].Split("\"")[0].Replace("_", "%20");
            }
        }

        string descriptionURL = "https://en.wikipedia.org/w/api.php?action=query&prop=extracts&exintro&explaintext&format=json&redirects&titles=" + title;
        using (UnityWebRequest request = UnityWebRequest.Get(descriptionURL))
        {
            // sends the web request
            yield return request.SendWebRequest();


            // log errors if there is one
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError)
            {
                //outputArea.text = request.error;
                Debug.Log($"{request.error}: {request.downloadHandler.text}");
            }
            else
            {
                string result = request.downloadHandler.text;
                Debug.Log(result);
                WikiResult wikiResult = JsonConvert.DeserializeObject<WikiResult>(result);
                List<string> keyList = new List<string>(wikiResult.query.pages.Keys);

                string description = wikiResult.query.pages[keyList[0]].extract;

                description = description.Split(".")[0] + ".";

                GameObject.FindGameObjectWithTag("Description")
            .GetComponent<TMP_Text>().text = description;

            }
            yield break;
        }
    }

    public static IEnumerator DownloadImage(GameObject plantImage, string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        var downloadTask = request.SendWebRequest();
        yield return new WaitUntil(() => downloadTask.isDone);
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError)
            Debug.Log(request.error);
        else
            Debug.Log(((DownloadHandlerTexture)request.downloadHandler).texture);
        plantImage.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }


}
