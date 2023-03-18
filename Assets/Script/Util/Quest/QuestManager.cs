using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questHolder;
    private GameObject questPage;
    private DataManager dataManager;

    public List<Quest> CurrentQuests;

    private void Awake()
    {
        
        dataManager = GameObject.FindObjectOfType<DataManager>();

        dataManager.Load();


        for (int i = 0; i < dataManager.GetGameData().questTracker.Count; ++i)
        {
            CurrentQuests[i].Goals[0].CurrentAmount = dataManager.GetGameData().questTracker[i].currentAmount;
            CurrentQuests[i].Goals[0].RequiredAmount = dataManager.GetGameData().questTracker[i].requiredAmount;
            CurrentQuests[i].Goals[0].Completed = dataManager.GetGameData().questTracker[i].completed;
        }

        // Only initialize incomplete quests
        foreach (var quest in CurrentQuests) // if (!quest.Goals[0].Completed)
        {
            if (quest.Goals[0].CurrentAmount >= quest.Goals[0].RequiredAmount)
            {
                quest.Goals[0].Complete();
            }
            if (quest.Completed == false)
            {
                quest.Initialize();
                quest.QuestCompleted.AddListener(OnQuestCompleted);
            }
            
            
            questHolder.GetComponent<QuestWindow>().Initialize(quest);
        }
        questHolder.SetActive(true);


        dataManager.SetQuests(CurrentQuests);
        dataManager.Save();
    }

    public void Walk(double walkedDistance)
    {
        EventManager.Instance.QueueEvent(new GameEvent.WalkingGameEvent(walkedDistance));
    }

    private void OnQuestCompleted(Quest quest)
    {
        questHolder.transform.GetChild(CurrentQuests.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
    }

    public void RemoveQuest(Quest quest)
    {
        Destroy(questHolder.transform.GetChild(CurrentQuests.IndexOf(quest)).gameObject);
        CurrentQuests.Remove(quest);

        // Done with all quests
        if (CurrentQuests.Count == 0)
        {
            questPage = GameObject.Find("Quest Page");
            questPage.transform.GetChild(0).gameObject.SetActive(true);
        }
    }
}
