using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text text;
    private float score = 0;
    private float clock = 0;
    
    void Start()
    {
        text.text = "Score: " + score;
    }
    public void FinishGame()
    {
        text.text = "Game Over\nScore: " + score;
    }
    public void SetMessage(string str)
    {
        text.text = str;
    }
    public void SetClock(float value)
    {
        clock = value;
        text.text = "Time: " + clock + "\nScore: " + score;
    }
    public void AddPoints(float value)
    {
        score += value;
        text.text = "Time: " + clock + "\nScore: " + score;
    }
}
