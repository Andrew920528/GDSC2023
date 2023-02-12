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
    private string imagePath = Application.persistentDataPath + "/Screenshots/CameraScreenshot.png";
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


    public void TakeScreenshot()
    {
        ScreenshotHandler.TakeScreenshot_Static(Screen.width, Screen.height);
    }


}
