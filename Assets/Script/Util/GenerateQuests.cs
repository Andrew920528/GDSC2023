using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;
using UnityEngine.UI;
using System.Linq;


public class GenerateQuests : MonoBehaviour
{
    private Button getQuestsButton;
    // Start is called before the first frame update
    void Start()
    {
        getQuestsButton = gameObject.GetComponent<Button>();
        getQuestsButton.onClick.AddListener(() => GetRandomQuests());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void GetRandomQuests()
    {

    }
}
