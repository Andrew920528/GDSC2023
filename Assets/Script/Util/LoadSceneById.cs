using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadSceneById : MonoBehaviour
{
    public int sceneId;
    ChangeScene gameManager;
    // Start is called before the first frame update
    void Start()
    {
        this.gameObject.GetComponent<Button>().onClick.AddListener(() => SceneManager.LoadScene(sceneId));
    }

    // Update is called once per frame
    void Update()
    {

    }
}
