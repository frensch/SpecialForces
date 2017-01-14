using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text text;
    private float score = 0;
    
    void Start()
    {
        text.text = "Score: " + score;
    }
    public void AddPoints(float value)
    {
        score += value;
        text.text = "Score: " + score;
    }
}
