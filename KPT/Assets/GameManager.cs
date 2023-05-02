using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static System.Net.Mime.MediaTypeNames;
using System;

public class GameManager : MonoBehaviour
{
    //enum GameState { Menu, Questions, Review };
    //GameState current = GameState.Menu;

    [SerializeField] Loader loader;
    List<Question> questions;
    int currentQuestion = 0;
    int correctAnswer = 1;

    [SerializeField] GameObject questionPanel;
    TextMeshProUGUI prompt;
    TextMeshProUGUI[] answers;
    [SerializeField] TextMeshProUGUI reviewText;

    Button[] buttons;

    Material material;
    [SerializeField] Color defaultAnswerColor;
    [SerializeField] Color highlightAnswerColor;
    public BackgroundColor BackgroundColor;
    public ReviewPanelOpener reviewPanelOpener;

    public void PresentQuestion()
    {
        //current = GameState.Questions;
        BackgroundColor.ColorChange();
        ResetButtonColor();
        foreach (var b in buttons)
        {
            b.interactable = true;
        }
        var q = questions[currentQuestion];
        correctAnswer = UnityEngine.Random.Range(0, 3);
        prompt.text = q.start + " <sprite name=\"Bubble\"> " + q.end;
        //keep for reference:
        //prompt.text = q.start + "__" + q.end;
        //prompt.text = q.start + "U00B0000" + q.end;

        var wrong = new string[] { q.wrong1, q.wrong2, q.wrong3 };

        for (int i = 0; i < answers.Length; i++)
        {
            string s = i == correctAnswer ? q.correct : wrong[i];
            if (i > correctAnswer)
            {
                s = wrong[i - 1];
            }
            answers[i].text = s;
        }
        // run anims / ...
    }

    public void ButtonPressed(int index) {
        foreach (var b in buttons) {
            b.interactable = false;
        } if (index == correctAnswer){
            CorrectAnswer();
        } else {
            WrongAnswer();
        }
    }

    void WrongAnswer() {
        print("nope!");
        //anim
        AfterAnswer();
    }
    void CorrectAnswer() {
        print("yep!");
        answers[correctAnswer].color = highlightAnswerColor;
        AfterAnswer();
        // run anims / ...
    }

    void AfterAnswer() {
        // run anims / ...
        Invoke("NextQuestion", 2f);
    }

    void OnValidate() {
        FindTextComponents();
        ResetButtonColor();
    }
    void ResetButtonColor() {
        foreach (var text in answers)
        {
            text.color = defaultAnswerColor;
        }
    }

    public void NextQuestion() {
        currentQuestion++;
        // check & handle if no more questions!
        //Index was out of range - ReviewPanel
        PresentQuestion();
    }
    void Start() {
        questions = loader.Load();
        buttons = questionPanel.GetComponentsInChildren<Button>();

        //currentQuestion = Random.Range(0, ?.Length);
        //currentQuestion = Random.Range(0, questions.Length);
        //randomize
        FindTextComponents();

        PresentQuestion();
        material = GetComponent<Material>();
    }

    private void FindTextComponents() {
        answers = new TextMeshProUGUI[3];
        answers[0] = questionPanel.transform.Find("Answer0").GetComponentInChildren<TextMeshProUGUI>();
        answers[1] = questionPanel.transform.Find("Answer1").GetComponentInChildren<TextMeshProUGUI>();
        answers[2] = questionPanel.transform.Find("Answer2").GetComponentInChildren<TextMeshProUGUI>();
        prompt = questionPanel.transform.Find("Prompt").GetComponent<TextMeshProUGUI>();
        //reviewText = GameObject.Find("ReviewTexts").GetComponent<TextMeshProUGUI>();
    }

    public void ReviewAnswers() {
        //current = GameState.Review;
        reviewPanelOpener.OpenReviewPanel();
        string s = "";
        for (int i = 0; i < currentQuestion; i++) {
            Question q = questions[i];
            s += q.start + "<color=#56C81B>" + q.correct + "</color>" + q.end + "\n";
        }
        reviewText.text = s;
    }
    void GoBackToGame() {
        reviewPanelOpener.BackToQuestionPanel();
    }
}