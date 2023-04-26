using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Loader : MonoBehaviour {
    private List<Question> questions;

    public List<Question> Load() {
        if (questions == null) {
            questions = new List<Question>();
            Debug.Log("Loading");
            LoadData();
        }
        return questions;
    }

    public void LoadData() {

        var data = Resources.Load<TextAsset>("Data");

        string[] rowData = data.text.Split('\n');

        for (int i = 0; i < rowData.Length; i++) {

            Question q = new Question();

            var columnData = rowData[i].Split(',');

            for (int k = 0; k < columnData.Length; k++) {
                if (k == 0) q.start = columnData[k];
                if (k == 1) q.correct = columnData[k];
                if (k == 2) q.wrong1 = columnData[k];
                if (k == 3) q.wrong2 = columnData[k];
                if (k == 6) q.end = columnData[k];
            }
            questions.Add(q);
        }
    }
}
