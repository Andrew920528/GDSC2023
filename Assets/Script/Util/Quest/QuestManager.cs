using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
public class QuestManager : MonoBehaviour
{
    private GameObject questHolder;
    private GameObject questPage;
    private const int mapSceneId = 2;
    public List<Quest> currentQuests;


    [SerializeField] private GameObject goalPrefab;

    [SerializeField] private int walkingGoalScene;
    [SerializeField] private int scanningGoalScene;

    private void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;

        for (int i = 0; i < StaticData.QuestTracker.Count; ++i)
        {
            QuestData questData = StaticData.QuestTracker[i];
            currentQuests[i].Goals[0].CurrentAmount = questData.currentAmount;
            currentQuests[i].Goals[0].RequiredAmount = questData.requiredAmount;
            currentQuests[i].Goals[0].Completed = currentQuests[i].Goals[0].CurrentAmount >= currentQuests[i].Goals[0].RequiredAmount;
        }

        // Only initialize incomplete quests
        foreach (var quest in currentQuests) if (!quest.Goals[0].Completed)
        {
            if (quest.Goals[0].CurrentAmount >= quest.Goals[0].RequiredAmount)
            {
                quest.Goals[0].Complete();
            }

            quest.Initialize();
            quest.QuestCompleted.AddListener(OnQuestCompleted);
        }
    }

    public void Walk(double walkedDistance)
    {
        EventManager.Instance.QueueEvent(new GameEvent.WalkingGameEvent(walkedDistance));
    }

    private void OnQuestCompleted(Quest quest)
    {
        // TODO
        //questHolder.transform.GetChild(currentQuests.IndexOf(quest)).Find("Checkmark").gameObject.SetActive(true);
        Debug.Log("Quest Complete!");
    }

    public void RemoveQuest(Quest quest)
    {
        // Destroy(questHolder.transform.GetChild(currentQuests.IndexOf(quest)).gameObject);
        questHolder = GameObject.FindGameObjectWithTag("QuestHolder");
        Destroy(questHolder.transform.GetChild(currentQuests.IndexOf(quest)).gameObject);
        // questHolder.transform.GetChild(currentQuests.IndexOf(quest)).gameObject.SetActive(false);
        currentQuests.Remove(quest);
        
    }

    public void Initialize()
    {
        const float verticalOffset = 225f;
        const float verticalSpace = 225f;
        for (int i = 0; i < currentQuests.Count; ++i)
        {
            Quest quest = currentQuests[i];
            foreach (var goal in quest.Goals)
                {
                    GameObject goalObj = Instantiate(
                        goalPrefab, new Vector3(0, 0, 0),
                        Quaternion.identity, Resources.FindObjectsOfTypeAll<GenerateQuests>()[0].gameObject.transform);
                    goalObj.transform.Find("Description").GetComponent<TMP_Text>().text = goal.GetDescription();

                    goalObj.transform.localPosition = new Vector3(0, verticalOffset - verticalSpace * i, 0);


                    GameObject countObj = goalObj.transform.Find("Progress Count").gameObject;
                    GameObject progressBarFill = goalObj.transform.Find("Progress Bar Fill").gameObject;

                    goalObj.AddComponent<Button>();
                    
                    if (goal.Completed)
                    {
                       
                        goalObj.transform.Find("Checkmark").gameObject.SetActive(true);
                        countObj.GetComponent<TMP_Text>().text = goal.RequiredAmount + "/" + goal.RequiredAmount;
                        progressBarFill.GetComponent<Image>().fillAmount = 1;
                        goalObj.GetComponent<Button>().onClick.RemoveAllListeners();
                        goalObj.GetComponent<Button>().onClick.AddListener(() => OnQuestClick(quest, goalObj, true));

                    }
                    else
                    {
                        Debug.Log("current progress: " + goal.CurrentAmount);
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
        GameObject canvas = GameObject.FindGameObjectWithTag("Canvas");
        ChangeScene c = canvas.GetComponent<ChangeScene>();
        GameObject questcanvas = GameObject.FindGameObjectWithTag("QuestCanvas");

        if (!quest.Completed)
        {
            questcanvas.SetActive(false);
            string questType = quest.Goals[0].GetType().ToString();

            if (questType == "WalkingGoal" || questType == "LocationGoal")
            {
                
                c.MoveToScene(walkingGoalScene);
            }
            else if (questType == "ScanningGoal")
            {
                c.MoveToScene(scanningGoalScene);
            }
            // If the quest is completed, then claim the rewards and hide the quest.
        }
        else
        {
            // TODO: claim the rewards
            // this.OnQuestCompleted(quest);
            // get rid of the goal object
            GetComponent<LevelSystem>().AddExperience(quest.Reward.XP);
            StaticData.PlayerStats.QuestsCompleted++;


            this.RemoveQuest(quest);

        }

        
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // if it's the map scene, initialize the quests
        if (scene.buildIndex == mapSceneId)
        {
            Initialize();
            
        }

    }
}
