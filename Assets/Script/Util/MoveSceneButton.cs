using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MoveSceneButton : MonoBehaviour
{
    public int sceneId;
    ChangeScene gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.FindObjectOfType<GameManager>().GetComponent<ChangeScene>();
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => gameManager.MoveToScene(sceneId));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
