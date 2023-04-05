using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenshotHandler : MonoBehaviour
{
    private static ScreenshotHandler instance;
    private Camera cam;
    public RectTransform CaptureArea;
    private GameObject[] disableOnScreenshot;

    private bool takeScreenshotOnNextFrame;

    private byte[] currentImage;

    private RawImage scannedImage;
    public GameObject scanningPanel;
    


    private void Awake()
    {
        instance = this;
        cam = gameObject.GetComponent<Camera>();
        disableOnScreenshot = GameObject.FindGameObjectsWithTag("DisableOnScreenshot");
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

            var corners = new Vector3[4];
            CaptureArea.GetWorldCorners(corners);
            var bl = RectTransformUtility.WorldToScreenPoint(cam, corners[0]);
            var tl = RectTransformUtility.WorldToScreenPoint(cam, corners[1]);
            var tr = RectTransformUtility.WorldToScreenPoint(cam, corners[2]);

            var height = tl.y - bl.y;
            var width = tr.x - bl.x;

            Texture2D renderResult = new Texture2D((int)width, (int)height, TextureFormat.ARGB32, false);
            Rect rect = new Rect(bl.x, bl.y, width, height);

            renderResult.ReadPixels(rect, 0, 0);
            renderResult.Apply();

            scanningPanel.SetActive(true);
            scannedImage = scanningPanel.GetComponentInChildren<RawImage>();
            scannedImage.texture = renderResult;

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

            foreach (GameObject g in disableOnScreenshot)
            {
                g.SetActive(true);
            }

            // call the API to get plant data from the screenshot
            GameObject.FindObjectOfType<GetPlantData>().GetPlantInfo(currentImage);
        }
    }

    private void TakeScreenshot(int width, int height)
    {
        foreach (GameObject g in disableOnScreenshot)
        {
            g.SetActive(false);
        }
        cam.targetTexture = RenderTexture.GetTemporary((int)CaptureArea.rect.width, (int)CaptureArea.rect.height, 16);
        takeScreenshotOnNextFrame = true;
    }

    public static void TakeScreenshot_Static(int width, int height)
    {
        instance.TakeScreenshot(width, height);
    }

}
