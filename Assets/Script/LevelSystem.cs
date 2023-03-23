using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class LevelSystem : MonoBehaviour
{
    public static LevelSystem instance;
    [SerializeField]
    private int level;
    [SerializeField]
    private int experience;
    [SerializeField]
    private int experienceToNextLevel = 0;

    private DataManager dataManager;

    private TextMeshProUGUI uiLevelText;
    private TextMeshProUGUI uiLevelProgressText;

    

    // Start is called before the first frame update

    private void Awake()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of level system!");
            return;
        }

        instance = this;

        dataManager = GetComponent<DataManager>();
        int levelFromData = dataManager.GetGameData().level;

        SetLevel(levelFromData);
        experience = dataManager.GetGameData().currentExperience;
    }

    public void AddExperience(int experienceToAdd)
    {
        Debug.Log("adding experience");
        experience += experienceToAdd;

        if (experience >= experienceToNextLevel)
        {
            SetLevel(level + 1);
        }

        UpdateVisual();

        dataManager.SetLevel(level);
        dataManager.SetExperience(experience);
        dataManager.Save();
       
    }

    public void SetLevel(int value)
    {
        Debug.Log("setting level");
        this.level = value;
        experience = experience - experienceToNextLevel;
        experienceToNextLevel = (int)(10f * (Mathf.Pow(level + 1, 2) - (5 * (level + 1)) + 8));
    }

    public void UpdateVisual()
    {
        Debug.Log("updating level visual");
        GameObject uiLevel = GameObject.FindGameObjectWithTag("LevelUI");
        if (uiLevel != null)
        {
            uiLevelText = uiLevel.transform.Find("LevelText").GetComponent<TextMeshProUGUI>();
            uiLevelProgressText = uiLevel.transform.Find("LevelProgressText").GetComponent<TextMeshProUGUI>();
            uiLevelText.SetText(level.ToString("0"));
            uiLevelProgressText.SetText(experience + "/" + experienceToNextLevel);

            uiLevel.transform.Find("LevelImageFill").GetComponent<Image>().fillAmount = (float) experience / experienceToNextLevel;
        }
    }
}
