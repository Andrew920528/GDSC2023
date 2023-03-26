using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "QuestionScriptableObject", menuName = "Questions/questionType")]
public class QuestionType : ScriptableObject
{
    // Types of questions:
    // 1. Scientific Name
    // 2. Watering period
    // 3. Sunlight
    // 4. Soil
    // 5. Pick right image
    public int questionTypeIndex;
    public List<string> questionTypeList = new List<string>
        {
            "What is the Scientific Name of this Plantomo?",
            "What is the Watering Period of this Plantomo?",
            "What is the right amount of Sunlight for this Plantomo?",
            "What type of soil is suitable for this Plantomo?",
            "Which of these images represent this Plantomo?",
            "SDG",
        };

}