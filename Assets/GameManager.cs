using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


// Class for keeping track of variables across scenes 
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Root plantInfo;
    private byte[] plantImage;

    public Root PlantInfo
    {
        get
        {
            return plantInfo;
        }
        set
        {
            plantInfo = value;
        }
    }

    public byte[] PlantImage
    {
        get
        {
            return plantImage;
        }
        set
        {
            plantImage = value;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        Debug.Log("On Enable Called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }



    // Update is called once per frame
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("Scene loaded:" + scene.name);

    }

    
    

}
