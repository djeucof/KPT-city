using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Question {
    //public string name;
    //public string number;

    public string start;
    public string correct;
    public string wrong1;
    public string wrong2;
    public string wrong3;
    public string end;
            
    public string GetQuestion() {
        return start + " " + correct + " " + wrong1 + " " + wrong2 + " " + wrong3 + " " + end;
    }
    
    public Question(string newStart, string newCorrect, string newWrong1, string newWrong2, string newWrong3, string newEnd) {
        //name = newName;
        //number = newNumber;

        start = newStart;
        correct = newCorrect;
        wrong1 = newWrong1;
        wrong2 = newWrong2;
        wrong3 = newWrong3;
        end = newEnd;
    }
    public override string ToString() {
        //return "Name: " + name + " Number: " + number;
        return start + " " + correct + " " + wrong1 + " " + wrong2 + " " + wrong3 + " " + end;
    }
}

public class Testing : MonoBehaviour {
    public List<Question> task;

    void Start() {
        foreach (var entry in task) {
            print(entry.start + " " + entry.correct + " " + entry.wrong1 + " " + entry.end);
            //  print(entry);
        }



        //for (int i = 0; i < book.Count; i++) {
        //    print("Name: " + book[i].name + " Number: " + book[i].number);
        //}
    }

    private void Update() {
        if (Input.GetKeyDown(KeyCode.Space)) {
            Question entry;
            entry.start = "Menne ra";
            entry.correct = "nn";
            entry.wrong1 = "nt";
            entry.wrong2 = "nk";
            entry.wrong3 = "nd";
            entry.end = "alle!";

            task.Add(entry);

            task.Add(new Question("Menne ra", "nn", "nt", "nk", "nd", "alle!"));

        }
    }
}


