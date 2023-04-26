using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReviewPanelOpener : MonoBehaviour {
    public GameObject QuestionPanel;
    public GameObject ReviewPanel;
    //private Button PauseBackButton;
    //public Sprite PauseIdle;
    //public Sprite PausePressed;

    public void OpenReviewPanel() {
        if (ReviewPanel != null) {
            ReviewPanel.SetActive(true);
            QuestionPanel.SetActive(false);
        }
    }
    public void BackToQuestionPanel() {
            Debug.Log("Resume pressed");
        //if (QuestionPanel != null) {
            QuestionPanel.SetActive(true);
            ReviewPanel.SetActive(false);
        //}
       // DeactivatePause();
    }

    //public void DeactivatePause() {
    //PauseBackButton = GetComponent<Button>();
    //PauseBackButton.image.overrideSprite = PauseIdle;
    //}
}