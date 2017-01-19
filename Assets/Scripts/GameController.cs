using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public int level = 1;
    
    private float dificultyIncrease = 0.3f;
    private FindSpawnSpots spawn;
    

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
    }
	// Use this for initialization
	void Start () {
        SetLevel(1);
        spawn = GetComponent<FindSpawnSpots>();
    }
	
	// Update is called once per frame
	void Update () {
        spawn.DecreaseSpawnTime(dificultyIncrease/(100*30));
    }
}
