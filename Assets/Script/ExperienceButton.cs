using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExperienceButton : MonoBehaviour
{
    private LevelSystem levelSystem;
    // Start is called before the first frame update
    void Start()
    {
        levelSystem = GameObject.FindObjectOfType<LevelSystem>();
        levelSystem.UpdateVisual();
    }

    public void AddExperience(int experienceToAdd)
    {
        levelSystem.AddExperience(experienceToAdd);
    }
}
