using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckoutPlantomo : MonoBehaviour
{
    public int plantInfoScene = 6;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Checkout()
    {
        Debug.Log(StaticData.SelectedPlantomo);
        SceneManager.LoadScene(plantInfoScene);
    }
}
