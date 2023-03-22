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
    private GameManager gameManager;
    private Root plantInfo;
    public List<GameObject> plantomos;
    public GameObject plantomoPlaceholder;


    private CheckoutPlantomo checkoutButton;

    // Creates a TextInfo based on the "en-US" culture.
    TextInfo textInfo = new CultureInfo("en-US", false).TextInfo;

    private GameObject descriptionBox;

    // Start is called before the first frame update
    void Start()
    {

        gameManager = GameObject.FindObjectOfType<GameManager>();
        plantInfo = gameManager.PlantInfo;

        checkoutButton = GameObject.FindObjectOfType<CheckoutPlantomo>();

        Result result = plantInfo.results[0];
        List<image> images = result.images;
        Debug.Log("image: " + images[0].url.m);


        Gbif gbif = result.gbif;
        int gbif_id = gbif == null ? 0 : gbif.id;
        
        string commonName = result.species.commonNames.Count == 0 ? result.species.scientificName : result.species.commonNames[0];
        commonName = textInfo.ToTitleCase(commonName);
        string scientificName = result.species.scientificName;

        descriptionBox = gameObject;

        gameObject.transform.Find("Middle Text").Find("Plant Name").GetComponent<TMP_Text>().text = commonName;

        EventManager.Instance.QueueEvent(new GameEvent.ScanningGameEvent(commonName));
        Debug.Log("scanning event queued");


        Debug.Log("gbif_id: " + gbif_id);


        if (StaticData.plantomoDict.ContainsKey(commonName))
        {
            gameObject.transform.Find("Description Box").Find("Description")
                .GetComponent<TMP_Text>().text = StaticData.plantomoDict[commonName].GetDescription();

            Plantomo plantomoData = StaticData.plantomoDict[commonName];
            Plant plantData = plantomoData.GetPlant();

            plantomoPlaceholder = plantomos[plantomoData.GetID()];

            GameObject pc = Instantiate(plantomoPlaceholder, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
            pc.transform.localPosition = new Vector3(0, 300, 0);
            pc.transform.localScale = new Vector3(25, 25, 1);

            checkoutButton.Checkout(commonName);


        } else
        {
            GameObject pc = Instantiate(plantomoPlaceholder, new Vector3(0, 0, 0), Quaternion.identity, transform.parent);
            pc.transform.localPosition = new Vector3(0, 300, 0);
            pc.transform.localScale = new Vector3(25, 25, 1);

            gameObject.transform.Find("Description Box").Find("Description")
                .GetComponent<TMP_Text>().text = "Description of this plant from GBIF API";
            StartCoroutine(DownloadImage(pc ,images[0].url.m));


            checkoutButton.Checkout();
        }

        GetPlantInfo(commonName);
    }


    private string URL = "https://en.wikipedia.org/w/api.php?action=opensearch&limit=1&namespace=0";

    public void GetPlantInfo(string query) => StartCoroutine(GetPlantInfo_Coroutine(query));



    public IEnumerator GetPlantInfo_Coroutine(string query)
    {

        query = query.Split(" (")[0];

        var uriBuilder = new UriBuilder(URL);

        var q = HttpUtility.ParseQueryString(uriBuilder.Query);

        q["search"] = query;

        uriBuilder.Query = q.ToString();
        URL = uriBuilder.ToString();
        string title = "";
        Debug.Log(URL);

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

                //title = "Sea Coconut";
            }
        }

        string descriptionURL = "https://en.wikipedia.org/w/api.php?action=query&prop=extracts&exintro&explaintext&format=json&redirects&titles=" + title;
        using (UnityWebRequest request = UnityWebRequest.Get(descriptionURL))
        {
            Debug.Log(descriptionURL);
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
                Debug.Log(keyList[0]);
                Debug.Log(wikiResult.query.pages[keyList[0]].extract);

                string description = wikiResult.query.pages[keyList[0]].extract;

                description = description.Split(".")[0] + ".";

                descriptionBox.transform.Find("Description Box").Find("Description")
        .GetComponent<TMP_Text>().text = description;

            }
            yield break;
        }
    }

    IEnumerator DownloadImage(GameObject plantImage, string MediaUrl)
    {
        UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
        yield return request.SendWebRequest();
        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.DataProcessingError)
            Debug.Log(request.error);
        else
            Debug.Log(((DownloadHandlerTexture)request.downloadHandler).texture);
            plantImage.GetComponent<RawImage>().texture = ((DownloadHandlerTexture)request.downloadHandler).texture;
    }


}
