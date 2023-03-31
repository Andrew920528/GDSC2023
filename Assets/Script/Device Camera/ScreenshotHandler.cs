using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera cam;

    public RectTransform CaptureArea;

    private bool takeScreenshotOnNextFrame;

    private byte[] currentImage;

    [SerializeField]
    private int lensVerticalOffest = 100;

    


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
            int width = (int) CaptureArea.rect.width;
            int height = (int)CaptureArea.rect.height;
            float startX = CaptureArea.offsetMin.x;
            float startY = CaptureArea.offsetMin.y;

            takeScreenshotOnNextFrame = false;
            RenderTexture renderTexture = cam.targetTexture;

            Texture2D renderResult = new Texture2D(width, height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(startX, startY, width, height);
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

    private void TakeScreenshot()
    {
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static()
    {
        instance.TakeScreenshot();
    }

}
