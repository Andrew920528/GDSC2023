using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;


// Class for keeping track of variables across scenes 
public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    private Root plantInfo;
    private byte[] plantImage;
    private Image resultImage;

    private DataManager dataManager;
    private QuestManager questManager;

    [SerializeField]
    private float saveWaitTime;

    [SerializeField]
    private int mapSceneId = 2;


    public void Awake()
    {
     
        dataManager = GetComponent<DataManager>();

        dataManager.Load();

        questManager = GetComponent<QuestManager>();

        StaticData.plantomoInventory = dataManager.GetGameData().plantomoInventory;

        // loads the saved plantomos into inventory
  
    }

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


    public Image ResultImage
    {
        get
        {
            return resultImage;
        }
        set
        {
            resultImage = value;
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

        // if it's the map scene, initialize the quests
        if (scene.buildIndex == mapSceneId)
        {
            questManager.Initialize();
        }

    }


    IEnumerator saveGameState(float saveWaitTime)
    {
        dataManager.Save();
        yield return new WaitForSecondsRealtime(saveWaitTime);
        StartCoroutine(saveGameState(saveWaitTime));
    }
}
