using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhonebookBad : MonoBehaviour {
    public List<string> names;
    public List<string> numbers;

    void Start() {
        for (int i = 0; i < names.Count; i++) {
            print("Name: " + names[i] + " Number: " + numbers[i]);
        }
    }
}
