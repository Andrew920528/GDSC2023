using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mapbox.Utils;
using TMPro;
using System;

public class QuestWindow : MonoBehaviour
{

    [SerializeField] private GameObject goalPrefab;

    [SerializeField] private float verticalOffset;
    [SerializeField] private float verticalSpace;

    [SerializeField] private int walkingGoalScene;
    [SerializeField] private int scanningGoalScene;

    private GameManager gameManager;
    private QuestManager questManager;
    private List<Quest> currentQuests;


    public void Initialize(Quest quest)
    {
        gameManager = GameObject.FindObjectOfType<GameManager>();
        questManager = gameManager.GetComponent<QuestManager>();
        currentQuests = questManager.CurrentQuests;


        foreach (var goal in quest.Goals) if (currentQuests.Contains(quest))
            {
            GameObject goalObj = Instantiate(goalPrefab, new Vector3(0, 0, 0), Quaternion.identity, gameObject.transform);
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
                
            } else
            {
                countObj.GetComponent<TMP_Text>().text = Math.Round(goal.CurrentAmount, 2) + "/" + goal.RequiredAmount;
                progressBarFill.GetComponent<Image>().fillAmount = (float) (goal.CurrentAmount / goal.RequiredAmount);
                goalObj.GetComponent<Button>().onClick.RemoveAllListeners();
                goalObj.GetComponent<Button>().onClick.AddListener(() => OnQuestClick(quest, goalObj, false));

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
            } else if (questType == "ScanningGoal")
            {
                gameManager.GetComponent<ChangeScene>().MoveToScene(scanningGoalScene);
            }
        // If the quest is completed, then claim the rewards and hide the quest.
        } else
        {
            // To do: claim the rewards

            // get rid of the goal object
            questManager.RemoveQuest(quest);
        }
    }


}
