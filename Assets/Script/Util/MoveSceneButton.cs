using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveSceneButton : MonoBehaviour
{
    public int sceneId;
    public float wait;
    ChangeScene gameManager;
    Button button;
    
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<ChangeScene>();
        button = GetComponent<Button>();
        button.onClick.AddListener(() => gameManager.MoveToScene(sceneId));
        
    }

    
    void Update()
    {

    }



}
