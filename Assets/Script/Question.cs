using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "QuestionScriptableObject", menuName = "Questions/question")]
public class Question : ScriptableObject
{
    // Types of questions:
    // 1. Scientific Name
    // 2. Watering period
    // 3. Sunlight
    // 4. Soil
    // 5. Pick right image
    public List<string> answerChoices;
    public QuestionType questionType;
    public int correctAnswerIndex;

}