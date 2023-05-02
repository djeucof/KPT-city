using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestionPanelOpener : MonoBehaviour {
    public GameObject QuestionPanel;
    public GameObject StartPanel;

    public void OpenQuestionPanel() {
        
        if (QuestionPanel != null) {
            QuestionPanel.SetActive(true);
            if (StartPanel != null) {
                StartPanel.SetActive(false);
            }
        }
    }
}
