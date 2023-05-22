using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
//using UnityEngine.UIElements;
//using System;
//using Unity.Mathematics;

public class GameManager : MonoBehaviour {
    enum GameState { Playing, QuestionsOver };

    // public for debug only
    [SerializeField] GameState currentState = GameState.Playing;

    [SerializeField] Loader loader;
    List<Question> questions;
    int currentQuestion = 0;
    int correctAnswer = 1;

    [SerializeField] GameObject questionPanel;
    [SerializeField] GameObject FinalReviewPanel;
    [SerializeField] GameObject GameOverPanel;

    TextMeshProUGUI prompt;
    TextMeshProUGUI[] answers;
    [SerializeField] TextMeshProUGUI reviewText;
    [SerializeField] TextMeshProUGUI finalReviewText;
    Button[] buttons;

    Material material;

    [SerializeField] Color defaultAnswerColor;
    [SerializeField] Color highlightAnswerColor;
    [SerializeField] Color wrongAnswerColor;
    public BackgroundColor BackgroundColor;
    public ReviewPanelOpener reviewPanelOpener;
    public QuestionPanelOpener questionPanelOpener;

    public ParticleSystem[] CorrectParticles;
    public ParticleSystem[] WrongParticles;
    public ParticleSystem[] FinalWinParticles;

    public AudioClip[] winSounds;
    public AudioClip[] loseSounds;
    public AudioClip finalwin;

    public AudioSource audioSource;

    public GameObject vio;
    private Animator anim;

    public void StartNewGame() {
        currentQuestion = 0;
        currentState = GameState.Playing;
        questions = loader.Load();
        questions = ShuffleQuestions(questions);
        questionPanelOpener.OpenQuestionPanel();
        PresentQuestion();
    }

    public void PresentQuestion() {
        //change bg color:
        BackgroundColor.ColorChange();
        //change buttons:
        ResetButtonColor();

        foreach (var b in buttons) {
            b.interactable = true;
        }
        var q = questions[currentQuestion];
        correctAnswer = UnityEngine.Random.Range(0, 3);
        // "This question text"   "<sprite>"   "box is here"

        var start = q.start;
        int lastspace = start.LastIndexOf(" ");
        if (lastspace == -1) {
            print("no space");
        }
        start = start.Insert(lastspace + 1, "<nobr>");

        var end = q.end;
        int firstspace = end.IndexOf(" ");
        if (firstspace == -1) {
            end += "</nobr>";
        } else {
            end = end.Insert(firstspace, "</nobr>");
        }
        // "This question <nobr>text <sprite> box</nobr> is here"

        prompt.text = start + " <sprite name=\"Bubble\"> " + end;
        //keep for reference:
        //prompt.text = q.start + "__" + q.end;
        //prompt.text = q.start + "U00B0000" + q.end;

        var wrong = new string[] { q.wrong1, q.wrong2, q.wrong3 };

        for (int i = 0; i < answers.Length; i++) {
            string s = i == correctAnswer ? q.correct : wrong[i];
            if (i > correctAnswer) {
                s = wrong[i - 1];
            }
            answers[i].text = s;
        }
    }

    public void ButtonPressed(int index) {
        anim.enabled = false;
        foreach (var b in buttons) {
            b.interactable = false;
        }
        if (index == correctAnswer) {
            CorrectAnswer();
        } else {
            WrongAnswer();
        }

    }
    void WrongAnswer() {
        anim.enabled = true;
        anim.SetTrigger("TrLose");

        int l = Random.Range(0, loseSounds.Length);
        audioSource.PlayOneShot(loseSounds[l]);

        answers[correctAnswer].color = wrongAnswerColor;

        AfterAnswer();
    }
    void CorrectAnswer() {

        anim.enabled = true;
        anim.SetTrigger("TrCorrect");

        answers[correctAnswer].color = highlightAnswerColor;

        int i = Random.Range(0, CorrectParticles.Length);
        CorrectParticles[i].Play();

        int w = Random.Range(0, winSounds.Length);
        audioSource.PlayOneShot(winSounds[w]);

        AfterAnswer();
    }

    void AfterAnswer() {
        Invoke("NextQuestion", 2f);
    }

    void OnValidate() {
        FindTextComponents();
        ResetButtonColor();
    }
    void ResetButtonColor() {
        foreach (var text in answers) {
            text.color = defaultAnswerColor;
        }
    }

    public void NextQuestion() {
        currentQuestion++;
        //print("now in question " + currentQuestion + " out of " + questions.Count);
        if (currentQuestion < questions.Count) {
            PresentQuestion();
        } else {
            currentState = GameState.QuestionsOver;
            ReviewAnswers();
        }
    }

    void Start() {
        anim = vio.GetComponent<Animator>();
        //IdleAnimRandom();

        buttons = questionPanel.GetComponentsInChildren<Button>();

        FindTextComponents();

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

        GameObject.Find("Vio").transform.localScale = new Vector3(0, 0, 0);

        if (currentState == GameState.QuestionsOver) {
            reviewPanelOpener.OpenFinalReviewPanel();
            finalReviewText.text = ReadReviewAnswers();
            audioSource.PlayOneShot(finalwin, 0.7F);
            int i = Random.Range(0, FinalWinParticles.Length);
            FinalWinParticles[i].Play();
        } else {
            reviewPanelOpener.OpenReviewPanel();
            reviewText.text = ReadReviewAnswers();
        }
    }

    public string ReadReviewAnswers() {
        string s = "";
        for (int i = 0; i < currentQuestion; i++) {
            Question q = questions[i];
            s += "• " + q.start + "<color=#56C81B>" + q.correct + "</color>" + q.end + "\n";
        }
        //reviewText.text = s;
        return s;
    }

    public void GoBackToGame() {
        if (currentState == GameState.Playing) {
            reviewPanelOpener.BackToQuestionPanel();

        } else if (currentState == GameState.QuestionsOver) {
            FinalReviewPanel.SetActive(false);
            GameOverPanel.SetActive(true);
            GameObject.Find("Vio").transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);

        } else {
            Debug.LogError("unknown state");
        }
    }

    List<Question> ShuffleQuestions(List<Question> original) {
        var questions = new List<Question>(original);
        for (int i = 0; i < questions.Count - 1; i++) {
            int j = Random.Range(i, questions.Count);
            var temp = questions[j];
            questions[j] = questions[i];
            questions[i] = temp;
        }
        return questions;
    }
}