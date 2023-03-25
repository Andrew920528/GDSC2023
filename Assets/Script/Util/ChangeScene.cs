using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public GameObject loadingScreen;
    private Image loadingBarFill;
    
    
    private List<int> sceneHistory = new List<int>();
    private Canvas canvas;

    private void Start()
    {
        
        canvas = GetComponent<Canvas>();
        if (canvas != null)
        {
            canvas.sortingOrder = 0;
        }
        

        

        if (GameObject.FindGameObjectWithTag("LoadingScreen") == null)
        {
            
            this.loadingScreen = Instantiate(loadingScreen, new Vector3(0, 0, 0), Quaternion.identity);
            
        } else
        {
            this.loadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen");
        }
        RectTransform loadScreenRect = loadingScreen.GetComponent<RectTransform>();
        loadingScreen.transform.SetParent(transform);
        loadingScreen.transform.localPosition = new Vector3(0, 0, 0);
        loadingScreen.transform.localScale = new Vector3(1, 1, 1);

        RectTransformExtensions.SetLRTB(loadScreenRect, 0, 0, 0, 0);


        loadingScreen.SetActive(false);
        for (int i = 0; i < loadingScreen.transform.childCount; ++i)
        {
            Transform currentItem = loadingScreen.transform.GetChild(i);
            if (currentItem.CompareTag("LoadingBar"))
            {
                this.loadingBarFill = currentItem.GetComponent<Image>();
                this.loadingBarFill.fillAmount = 0;
                break;
            }
            else
            {
                Debug.Log("Loading bar not found");
            }
        }

        
    }

    public void MoveToScene(int sceneId)
    {
        
        sceneHistory.Add(sceneId);
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    public void MoveToSceneWithoutLoad(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }

    
    IEnumerator LoadSceneAsync(int sceneId)
    {

        Timer minTimer = new Timer(0.5f);

        loadingScreen.SetActive(true);
        canvas.sortingOrder = 50;
        while ( minTimer.GetCurrentTime() > 0)
        {
            
            minTimer.CountDown();
            loadingBarFill.fillAmount = 0.3f * (1 - 2*minTimer.GetCurrentTime());

            yield return null;
        }

        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);
        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);
            minTimer.CountDown();
            loadingBarFill.fillAmount = 0.3f + progressValue;

            yield return null;
        }
        
    }

    public void PreviousScene()
    {
        Debug.Log(sceneHistory.Count);
        if (sceneHistory.Count >= 2)  //Checking that we have actually switched scenes enough to go back to a previous scene
        {
            sceneHistory.RemoveAt(sceneHistory.Count - 1);
            StartCoroutine(LoadSceneAsync(sceneHistory[sceneHistory.Count - 1]));
        }

    }



    // Util class
    public static class RectTransformExtensions
    {
        public static void SetLRTB(RectTransform rt, float left, float right, float top, float bottom)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }

        public static void SetLeft(RectTransform rt, float left)
        {
            rt.offsetMin = new Vector2(left, rt.offsetMin.y);
        }

        public static void SetRight(RectTransform rt, float right)
        {
            rt.offsetMax = new Vector2(-right, rt.offsetMax.y);
        }

        public static void SetTop(RectTransform rt, float top)
        {
            rt.offsetMax = new Vector2(rt.offsetMax.x, -top);
        }

        public static void SetBottom(RectTransform rt, float bottom)
        {
            rt.offsetMin = new Vector2(rt.offsetMin.x, bottom);
        }

        public static void getWidth(RectTransform rt)
        {

        }
    }

}
