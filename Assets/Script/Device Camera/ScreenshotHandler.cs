using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera cam;

    private bool takeScreenshotOnNextFrame;

    private byte[] currentImage;

    private GameManager gameManager;


    private void Awake()
    {
        instance = this;
        cam = gameObject.GetComponent<Camera>();
    }


    public byte[] CurrentImage
    {
        get
        {
            return currentImage;
        }
    }

    private void OnPostRender()
    {
        if (takeScreenshotOnNextFrame)
        {
            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = cam.targetTexture;

            Texture2D renderResult = new Texture2D(renderTexture.width, renderTexture.height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(0, 0, renderTexture.width, renderTexture.height);
            renderResult.ReadPixels(rect, 0, 0);

            currentImage = renderResult.EncodeToPNG();
            if (Application.isEditor)
            {
                System.IO.File.WriteAllBytes(Application.dataPath + "/Screenshots/CameraScreenshot.png", currentImage);
            } else
            {
                // Save to gallery if not on editor
                string name = string.Format("{0}_Capture{1}_{2}.png", Application.productName, "{0}", System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
                Debug.Log("Permission result: " + NativeGallery.SaveImageToGallery(renderResult, Application.productName + " Captures", name));
            }
            
 
            Debug.Log("Screenshot saved");

            RenderTexture.ReleaseTemporary(renderTexture);
            cam.targetTexture = null;

            // call the API to get plant data from the screenshot
            GameObject.FindObjectOfType<GetPlantData>().GetPlantInfo(currentImage);
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        cam.targetTexture = RenderTexture.GetTemporary(width, height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }

}
