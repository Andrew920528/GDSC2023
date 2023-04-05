using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuizGenerator : MonoBehaviour
{
    public List<QuizQuestion> questions;
    public GameObject questionPrefab;
    public GameObject answerPrefab;
    private GameObject currentQuestionObject;
    public GameObject gameOverPrefab;
    public GameObject quizCompletePrefab;
    [SerializeField]
    private int startingLives = 3;
    private int livesLeft;
    private int score;
    public int verticalOffset;
    public int answerOffset = 100;

    private void Awake()
    {
        // TODO: Keep a list of questions for each plantomo, and keep the user's progress through them.
        questions = StaticData.questionList;
        UpdateVisuals();
    }

    public void StartQuiz()
    {
        livesLeft = startingLives;
        score = 0;
        GenerateQuestion(0);
    }

    private void GenerateQuestion(int index)
    {
        if (index >= questions.Count)
        {
            CompleteQuiz();
            return;
        }
        currentQuestionObject = Instantiate(questionPrefab, new Vector3(0, 0, 0), Quaternion.identity, transform);
        currentQuestionObject.transform.localPosition = new Vector3(0, verticalOffset, 0);
        currentQuestionObject.transform.localScale = new Vector3(2, 2, 1);

        QuizQuestion q = questions[index];
        currentQuestionObject.transform.Find("Question Text").GetComponent<TMP_Text>().
            SetText(q.Question);


        // Scale the Answers vertically if there are only 2 choices
        int verticalScale = q.AnswerChoices.Count == 2 ? 2 : 1;

        for (int optionIndex = 0; optionIndex < q.AnswerChoices.Count; ++optionIndex)
        {
            var option = q.AnswerChoices[optionIndex];
            GameObject ans = Instantiate(answerPrefab, new Vector3(0, 0, 0),
                Quaternion.identity, currentQuestionObject.transform.Find("Answer Holder"));

            float offsetX = optionIndex % 2 * answerOffset * 1.5f;
            float offsetY = optionIndex / 2 * -answerOffset;
            ans.transform.localPosition = new Vector3(offsetX, offsetY, 0);
            ans.transform.localScale = new Vector3(1, verticalScale, 1);

            // set text for answer choice
            ans.GetComponentInChildren<TMP_Text>().text = option;

            if (optionIndex == q.CorrectAnswerIndex)
            {
                ans.GetComponent<Button>().onClick.AddListener(() => OnRightAnswer(index));
            } else
            {
                ans.GetComponent<Button>().onClick.AddListener(() => OnWrongAnswer());
            }
        }
        
    }

    private void OnRightAnswer(int questionIndex)
    {
        Destroy(currentQuestionObject);

        int scoreMultiplier = 10;
        score += (questionIndex + 1) * scoreMultiplier;
        gameObject.transform.Find("Score Text").GetComponent<TMP_Text>().SetText("Score: " + score);

        GenerateQuestion(questionIndex + 1);

    }

    private void OnWrongAnswer()
    {
        livesLeft = livesLeft - 1;
        if (livesLeft == 0)
        {
            GameOver();
        }

        gameObject.transform.Find("Lives Holder").GetChild(livesLeft).gameObject.SetActive(false);
    }

    private void CompleteQuiz()
    {
        StaticData.PlayerStats.QuizCompleted++;
        StaticData.plantomoInventory[StaticData.SelectedPotIndex].QuizCompleted = true;
        GameObject quizCompleteObj = Instantiate(quizCompletePrefab, transform);
        quizCompleteObj.transform.GetComponentInChildren<Button>().onClick.AddListener(
            () => {
                ClaimRewards(); 
                }
            );
        
    }

    private void GameOver()
    {
        Destroy(currentQuestionObject);
        Instantiate(gameOverPrefab, transform);
    }

    private void ClaimRewards()
    {
        int coinsToGain = livesLeft * score;
        StaticData.PlayerStats.Coins += coinsToGain;

        UpdateVisuals();
    }

    private void UpdateVisuals()
    {
        if (StaticData.SelectedPotIndex != -1)
        {
            string levelField = "lvl. " + StaticData.plantomoInventory[StaticData.SelectedPotIndex].Level;
            GameObject.FindGameObjectWithTag("PlantomoLevel").GetComponent<TMP_Text>().SetText(levelField);
        }
    }
}
