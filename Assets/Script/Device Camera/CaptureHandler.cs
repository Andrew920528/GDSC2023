using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.IO;
using UnityEngine.UI;

public class CaptureHandler : MonoBehaviour
{
    public GameObject cameraLens;
    private string imagePath = Application.dataPath + "/Screenshots/";
    public class Plant
    {
        public string name;
    }

    public class PlantInfo
    {
        public string name;
    }

    private const string API_KEY = "2b10RIOZXAmYw1L53Jxov8Fe";
    private static string PROJECT = "all";

    private string urlParameters = String.Format("?api-key={0}", API_KEY);

    private string URL = "https://my-api.plantnet.org/v2/identify/" + PROJECT + String.Format("?api-key={0}", API_KEY);

    //public IEnumerator GetPlantInfo_Coroutine()
    //{
        
    //}

    public void TakeScreenshot()
    {

        ScreenshotHandler.TakeScreenshot_Static();
    }

    public void TestScreenShot(string name)
    {
        byte[] testImage = System.IO.File.ReadAllBytes(imagePath + name);
        GameObject.FindObjectOfType<GetPlantData>().GetPlantInfo(testImage);
    }

}
