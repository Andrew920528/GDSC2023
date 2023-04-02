using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Firestore;

[FirestoreData]
public class QuizQuestion
{
    // Types of questions:
    // 1. Scientific Name
    // 2. Watering period
    // 3. Sunlight
    // 4. Soil
    // 5. Pick right image
    [FirestoreProperty]
    public List<string> AnswerChoices { get; set; }
    [FirestoreProperty]
    public string Question { get; set; }
    [FirestoreProperty]
    public int CorrectAnswerIndex { get; set; }

    public QuizQuestion(string question, List<string> answerchoices, int correctAnswerIndex)
    {
        Question = question;
        AnswerChoices = answerchoices;
        CorrectAnswerIndex = correctAnswerIndex;
    }

}