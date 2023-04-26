using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundColor : MonoBehaviour {
    public Color[] colors;
    //float smoothness = 0.02f;
    [SerializeField] private Camera cameraRef;
    [SerializeField] private float time;
    private float currentTime;
    private int colorIndex;
    Color lastColor;

    public void Awake() {
        cameraRef = Camera.main;
        //Camera.main.backgroundColor = colors[Random.Range(0, colors.Length)];
        ColorChange();
    }
    public void ColorChange() {
        lastColor = cameraRef.backgroundColor;
        colorIndex = Random.Range(0, colors.Length);
        currentTime = 0;
    }
    public void Update() {
        currentTime += Time.deltaTime;
        //  colorChangeSpeed * Time.deltaTime
        float t = currentTime / time;
        cameraRef.backgroundColor = Color.Lerp(lastColor, colors[colorIndex], t);
    }
}
