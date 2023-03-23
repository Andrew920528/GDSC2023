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
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Checkout(string capturedPlantomo = null)
    {
        Debug.Log(capturedPlantomo);
        if (capturedPlantomo != null)
        {
            StaticData.SelectedPlantomo = capturedPlantomo;
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
