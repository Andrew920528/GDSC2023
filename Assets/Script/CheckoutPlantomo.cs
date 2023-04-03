using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CheckoutPlantomo : MonoBehaviour
{
    [SerializeField]
    private int plantInfoScene = 6;
    [SerializeField]
    public int cameraScene = 7;


    public void Checkout(string capturedPlantomo = null)
    {
        if (capturedPlantomo != null)
        {
            StaticData.SelectedPlantomo = StaticData.plantomoDict[capturedPlantomo];
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync(plantInfoScene);
            });
        } else
        {
            gameObject.transform.Find("Text").GetComponent<TMP_Text>().text = "Back to Camera";
            gameObject.GetComponent<Button>().onClick.AddListener(() =>
            {
                SceneManager.LoadSceneAsync(cameraScene);
            });
        }
        
    }
}
