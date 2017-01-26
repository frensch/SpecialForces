using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public int level = 1;
    public ScoreManager scoreManager;

    private float dificultyIncrease = 0.3f;
    private FindSpawnSpots spawn;
    private float gameTime = 0;
    private float restTime = 0;
    private float gameDuration = 60;
    private float restDuration = 30;
    private bool startRest = true;

    void SetLevel(int num) {
        level = num;
        switch(level)
        {
            case 1:
                dificultyIncrease = 0.3f;
                break;
            case 2:
                dificultyIncrease = 0.4f;
                break;
            case 3:
                dificultyIncrease = 0.5f;
                break;
            case 4:
                dificultyIncrease = 0.6f;
                break;
            case 5:
                dificultyIncrease = 0.7f;
                break;
        }
        spawn.ResetTime();
        gameTime = Time.time + gameDuration;
    }
	// Use this for initialization
	void Start () {
        SetLevel(1);
        spawn = GetComponent<FindSpawnSpots>();
    }
	
	// Update is called once per frame
	void Update () {
        if (gameTime > Time.time) {
            if (level < 5) {
                if (startRest) {
                    startRest = false;
                    restTime = Time.time + restDuration;
                } else if(restTime < Time.time) {
                    scoreManager.SetMessage("Level " + level + " completed\nLevel " + (level + 1) + " in " + (restTime - Time.time) + " seconds\n");
                } else {
                    SetLevel(level + 1);
                    startRest = true;
                }               
            }
            else {
                scoreManager.FinishGame();
            }
        }
        else {
            scoreManager.SetClock(gameTime - Time.time);
            spawn.DecreaseSpawnTime(dificultyIncrease / (100 * 30));
        }
    }
}
