using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPanelOpener : MonoBehaviour
{
    public GameObject QuestionPanel;
    public GameObject ReviewPanel;
    public GameObject FinalReviewPanel;
    public GameObject GameOverPanel;
    //Animator anim;
    //private Button PauseBackButton;
    //public Sprite PauseIdle;
    //public Sprite PausePressed;

    public void OpenReviewPanel(){
        if (ReviewPanel != null){
            ReviewPanel.SetActive(true);
            QuestionPanel.SetActive(false);
        }
    }

    public void OpenFinalReviewPanel() {
        if (FinalReviewPanel != null) {
            ReviewPanel.SetActive(false);
            QuestionPanel.SetActive(false);
            FinalReviewPanel.SetActive(true);
            //ReviewPanel.SetActive(false);
        }
    }
    public void BackToQuestionPanel(){
        Debug.Log("Resume pressed");
        //if (QuestionPanel != null) {
        QuestionPanel.SetActive(true);
        ReviewPanel.SetActive(false);
        FinalReviewPanel.SetActive(false);
        GameOverPanel.SetActive(false);
        GameObject.Find("Vio").transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
    }
}