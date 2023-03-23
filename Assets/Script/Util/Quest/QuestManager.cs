using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuestManager : MonoBehaviour
{
    [SerializeField] private GameObject questHolder;
    private GameObject questPage;
    private DataManager dataManager;

    public List<Quest> currentQuests;

    private GameManager gameManager;
    private QuestManager questManager;

    [SerializeField] private GameObject goalPrefab;

    [SerializeField] private float verticalOffset;
    [SerializeField] private float verticalSpace;

    [SerializeField] private int walkingGoalScene;
    [SerializeField] private int scanningGoalScene;

    private void Awake()
    {

        gameManager = GameObject.FindObjectOfType<GameManager>();
        
        dataManager = GameObject.FindObjectOfType<DataManager>();

        dataManager.Load();


        for (int i = 0; i < dataManager.GetGameData().questTracker.Count; ++i)
        {
            currentQuests[i].Goals[0].CurrentAmount = dataManager.GetGameData().questTracker[i].currentAmount;
            currentQuests[i].Goals[0].RequiredAmount = dataManager.GetGameData().questTracker[i].requiredAmount;
            currentQuests[i].Goals[0].Completed = dataManager.GetGameData().questTracker[i].completed;
        }

        // Only initialize incomplete quests
        foreach (var quest in currentQuests) // if (!quest.Goals[0].Completed)
        {
            if (quest.Goals[0].CurrentAmount >= quest.Goals[0].RequiredAmount)
            {
                quest.Goals[0].Complete();
            }

            quest.Initialize();
            quest.QuestCompleted.AddListener(OnQuestCompleted);
        }


        dataManager.SetQuests(currentQuests);
        dataManager.Save();
    }

    public void Walk(double walkedDistance)
    {
        EventManager.Instance.QueueEvent(new GameEvent.WalkingGameEvent(walkedDistance));
    }

    private void OnQuestCompleted(Quest quest)
    {
        //questHolder.transform.GetChild(currentQuests.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
        Debug.Log("Quest Complete!");
    }

    public void RemoveQuest(Quest quest)
    {
        Destroy(questHolder.transform.GetChild(currentQuests.IndexOf(quest)).gameObject);
        currentQuests.Remove(quest);

        // Done with all quests
        if (currentQuests.Count == 0)
        {
            questPage = GameObject.Find("Quest Page");
            questPage.transform.GetChild(0).gameObject.SetActive(true);
        }
    }

    public void Initialize()
    {
        foreach (Quest quest in currentQuests)
        {
            foreach (var goal in quest.Goals) if (currentQuests.Contains(quest))
                {
                    GameObject goalObj = Instantiate(
                        goalPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity, Resources.FindObjectsOfTypeAll<GenerateQuests>()[0].gameObject.transform);
                    goalObj.transform.Find("Description").GetComponent<TMP_Text>().text = goal.GetDescription();

                    goalObj.transform.localPosition = new Vector3(0, verticalOffset - verticalSpace * currentQuests.IndexOf(quest), 0);


                    GameObject countObj = goalObj.transform.Find("Progress Count").gameObject;
                    GameObject progressBarFill = goalObj.transform.Find("Progress Bar Fill").gameObject;

                    goalObj.AddComponent<Button>();

                    if (goal.Completed)
                    {
                        //countObj.SetActive(false);
                        goalObj.transform.Find("Checkmark").gameObject.SetActive(true);
                        countObj.GetComponent<TMP_Text>().text = goal.RequiredAmount + "/" + goal.RequiredAmount;
                        progressBarFill.GetComponent<Image>().fillAmount = 1;
                        goalObj.GetComponent<Button>().onClick.RemoveAllListeners();
                        goalObj.GetComponent<Button>().onClick.AddListener(() => OnQuestClick(quest, goalObj, true));

                    }
                    else
                    {
                        countObj.GetComponent<TMP_Text>().text = Math.Round(goal.CurrentAmount, 2) + "/" + goal.RequiredAmount;
                        progressBarFill.GetComponent<Image>().fillAmount = (float)(goal.CurrentAmount / goal.RequiredAmount);
                        goalObj.GetComponent<Button>().onClick.RemoveAllListeners();
                        goalObj.GetComponent<Button>().onClick.AddListener(() => OnQuestClick(quest, goalObj, false));

                    }
                }
        }

        
    }

    public void OnQuestClick(Quest quest, GameObject goalObj, bool completed)
    {
        // If quest is not completed, move the user to the appropriate page
        // e.g. Map for walking, Camera for scanning
        if (!completed)
        {
            string questType = quest.Goals[0].GetType().ToString();
            if (questType == "WalkingGoal")
            {
                gameManager.GetComponent<ChangeScene>().MoveToScene(walkingGoalScene);
            }
            else if (questType == "ScanningGoal")
            {
                gameManager.GetComponent<ChangeScene>().MoveToScene(scanningGoalScene);
            }
            // If the quest is completed, then claim the rewards and hide the quest.
        }
        else
        {
            // To do: claim the rewards

            // get rid of the goal object
            questManager.RemoveQuest(quest);
        }
    }
}
