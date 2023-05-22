using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanelOpener : MonoBehaviour {
    public GameObject QuestionPanel;
    public GameObject StartPanel;
    //public GameObject GameOverPanel;

    public void OpenQuestionPanel() {
        
        if (QuestionPanel != null) {
            QuestionPanel.SetActive(true);
            //GameOverPanel.SetActive(false);
            if (StartPanel != null) {
                StartPanel.SetActive(false);
            }
        }
    }
}
