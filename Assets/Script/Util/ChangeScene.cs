using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChangeScene : MonoBehaviour
{
    public GameObject LoadingScreen;
    public GameObject LoadingBarFill;

    //public void MoveToScene(int sceneID)
    //{
    //    SceneManager.LoadScene(sceneID);   
    //}

    ////public void ReturnOneScene()
    ////{
    ////    SceneManager.LoadScene();
    ////}
    //
    private List<int> sceneHistory = new List<int>();


    private void Start()
    {
        LoadingScreen = GameObject.FindGameObjectWithTag("LoadingScreen");
        LoadingBarFill = GameObject.FindGameObjectWithTag("LoadingBar");
        LoadingScreen.SetActive(false);
        LoadingBarFill.SetActive(false);
    }

    public void MoveToScene(int sceneId)
    {
        sceneHistory.Add(sceneId);
        StartCoroutine(LoadSceneAsync(sceneId));
    }

    IEnumerator LoadSceneAsync(int sceneId)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneId);

        if (LoadingScreen != null)
        {
            LoadingScreen.SetActive(true);
            LoadingBarFill.SetActive(true);

        }


        while (!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            LoadingBarFill.GetComponent<Image>().fillAmount = progressValue;

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
}
