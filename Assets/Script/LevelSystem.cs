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
    [SerializeField]
    private int levelUpReward;

    private TextMeshProUGUI uiLevelText;
    private TextMeshProUGUI uiLevelProgressText;

    

    // Start is called before the first frame update

    private void Start()
    {
        if (instance != null)
        {
            Debug.Log("More than one instance of level system!");
            return;
        }

        instance = this;

        int levelFromData = StaticData.PlayerStats.Level;

        SetLevel(levelFromData);
        experience = StaticData.PlayerStats.Experience;
    }


    public void SetupLeveling()
    {
        int levelFromData = StaticData.PlayerStats.Level;

        SetLevel(levelFromData);
        experience = StaticData.PlayerStats.Experience;

        UpdateVisual();
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

        StaticData.PlayerStats.Level = level;
        StaticData.PlayerStats.Experience = experience;
    }

    public void SetLevel(int value)
    {
        int coinsPerLevel = 100;
        Debug.Log("setting level");

        this.level = value;
        experience = experience - experienceToNextLevel;
        experienceToNextLevel = (int)(10f * (Mathf.Pow(level + 1, 2) - (5 * (level + 1)) + 8));
        levelUpReward = coinsPerLevel * level;
        StaticData.PlayerStats.Coins += levelUpReward;
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
