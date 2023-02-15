using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;

public class GetPlantData : MonoBehaviour
{
    TMP_InputField outputArea;
    byte[] byteArray;
    ScreenshotHandler screenshotHandler;

    
    void Start()
    {
        outputArea = GameObject.Find("OutputArea").GetComponent<TMP_InputField>();
        //GameObject.Find("GetButton").GetComponent<Button>().onClick.AddListener(GetPlantInfo);
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

    //void GetPlantInfo() => StartCoroutine(GetPlantInfo_Coroutine());

    //public IEnumerator GetPlantInfo_Coroutine()
    //{
    //    outputArea.text = "Plant info goes here";
    //    WWWForm form = new WWWForm();
    //    form.AddBinaryData("images", screenshotHandler.CurrentImage);
    //    using (UnityWebRequest request = UnityWebRequest.Post(URL, form))
    //    {
    //        yield return request.SendWebRequest();
    //        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
    //        {
    //            outputArea.text = request.error;
    //        } else
    //        {
    //            outputArea.text = request.downloadHandler.text;
    //        }
    //    }
    //}

}
