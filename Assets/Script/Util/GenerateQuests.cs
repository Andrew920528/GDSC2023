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
    private QuestManager questManager;
    // Start is called before the first frame update
    void Start()
    {
        gameObject.AddComponent<Button>();
        gameObject.GetComponent<Button>().onClick.AddListener(() => ShowQuests());
        questManager = GameObject.FindObjectOfType<QuestManager>(); 
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowQuests()
    {
        foreach (var quest in questManager.currentQuests) // if (!quest.Goals[0].Completed)
        {
            if (quest.Goals[0].CurrentAmount >= quest.Goals[0].RequiredAmount)
            {
                quest.Goals[0].Complete();
            }
            quest.Initialize();
        }

    }
}
